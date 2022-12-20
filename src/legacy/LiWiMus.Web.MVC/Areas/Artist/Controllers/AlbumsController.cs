using AutoMapper;
using FormHelper;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Albums.Specifications;
using LiWiMus.Core.Artists.Specifications;
using LiWiMus.Core.Interfaces.Files;
using LiWiMus.Core.Settings;
using LiWiMus.SharedKernel.Helpers;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;
using LiWiMus.Web.Shared;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Artist.Controllers;

[Area("Artist")]
[Route("Artist/{artistId:int}/[controller]")]
public class AlbumsController : Controller
{
    private readonly IRepository<Album> _albumsRepository;
    private readonly IRepository<Core.Artists.Artist> _artistRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly SharedSettings _settings;
    private readonly IFileService _fileService;
    private readonly IOptions<PullUrls> _pullUrls;

    public AlbumsController(IRepository<Album> albumsRepository,
                            IRepository<Core.Artists.Artist> artistRepository,
                            IAuthorizationService authorizationService, IMapper mapper,
                            IOptions<SharedSettings> settings, IFileService fileService, IOptions<PullUrls> pullUrls)
    {
        _albumsRepository = albumsRepository;
        _artistRepository = artistRepository;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _fileService = fileService;
        _pullUrls = pullUrls;
        _settings = settings.Value;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(int artistId)
    {
        var artist = await _artistRepository.GetBySpecAsync(new ArtistDetailedSpec(artistId));

        if (artist is null)
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, artist, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        return View(artist.Albums.ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int artistId, int id)
    {
        var album = await _albumsRepository.GetBySpecAsync(new DetailedAlbumByIdSpec(id));
        var artist = await _artistRepository.GetBySpecAsync(new ArtistWithOwnersByIdSpec(artistId));

        if (album is null || artist is null || album.Owners.All(a => a.Id != artistId))
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, artist, "SameAuthorPolicy") is {Succeeded: false} ||
            await _authorizationService.AuthorizeAsync(User, album, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        return View(album);
    }

    [HttpPut("{id:int}")]
    [FormValidator]
    public async Task<IActionResult> Update(int artistId, UpdateAlbumViewModel viewModel)
    {
        var album = await _albumsRepository.GetBySpecAsync(new DetailedAlbumByIdSpec(viewModel.Id));
        var artist = await _artistRepository.GetBySpecAsync(new ArtistWithOwnersByIdSpec(artistId));

        if (album is null || artist is null || album.Owners.All(a => a.Id != artistId))
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, artist, "SameAuthorPolicy") is {Succeeded: false} ||
            await _authorizationService.AuthorizeAsync(User, album, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        if (viewModel.ArtistsIds is not null)
        {
            if (viewModel.ArtistsIds.All(a => a != artistId))
            {
                return BadRequest();
            }

            var artists = await _artistRepository.ListAsync(new ArtistsByIdsSpec(viewModel.ArtistsIds));
            album.Owners = artists;
        }

        _mapper.Map(viewModel, album);
        if (viewModel.Cover is not null)
        {
            var fileResult = await _fileService.Save(viewModel.Cover.ToStreamPart());
            if (!fileResult.IsSuccessStatusCode || fileResult.Content is null)
            {
                return FormResult.CreateErrorResult("Bad photo");
            }

            var r = await _fileService.Remove(artist.PhotoLocation[1..]);
            album.CoverLocation = fileResult.Content.Location;
        }

        await _albumsRepository.UpdateAsync(album);
        return FormResult.CreateSuccessResultWithObject(
            new {CoverLocation = _pullUrls.Value.FileServer + album.CoverLocation},
            "Updated successfully");
    }

    [HttpGet("[action]")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(int artistId, CreateAlbumViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        var artist = await _artistRepository.GetBySpecAsync(new ArtistWithOwnersByIdSpec(artistId));

        if (artist is null)
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, artist, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        var fileResult = await _fileService.Save(viewModel.Cover.ToStreamPart());
        if (!fileResult.IsSuccessStatusCode || fileResult.Content is null)
        {
            ModelState.AddModelError(nameof(CreateArtistViewModel.Photo), "Bad photo");
            return View(viewModel);
        }

        var coverPath = fileResult.Content.Location;

        var artists = new List<Core.Artists.Artist> {artist};

        var album = new Album
        {
            Title = viewModel.Title,
            CoverLocation = coverPath,
            Owners = artists,
            PublishedAt = viewModel.PublishedAt
        };
        album = await _albumsRepository.AddAsync(album);

        return RedirectToAction("Details", "Albums", new {Area = "Artist", album.Id, artistId});
    }

    [HttpPost]
    [FormValidator]
    public async Task<IActionResult> Delete(int artistId, int id)
    {
        var album = await _albumsRepository.GetBySpecAsync(new DetailedAlbumByIdSpec(id));
        var artist = await _artistRepository.GetBySpecAsync(new ArtistWithOwnersByIdSpec(artistId));

        if (album is null || artist is null || album.Owners.All(a => a.Id != artistId))
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, artist, "SameAuthorPolicy") is {Succeeded: false} ||
            await _authorizationService.AuthorizeAsync(User, album, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        FileHelper.DeleteIfExists(Path.Combine(_settings.SharedDirectory, album.CoverLocation));
        await _albumsRepository.DeleteAsync(album);
        return FormResult.CreateSuccessResult("Removed successfully");
    }
}