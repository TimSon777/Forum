using System.Security.Claims;
using AutoMapper;
using FormHelper;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Artists.Specifications;
using LiWiMus.Core.Interfaces.Files;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;
using LiWiMus.Web.Shared;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Artist.Controllers;

[Area("Artist")]
[Route("[area]")]
public class HomeController : Controller
{
    private readonly IRepository<Core.Artists.Artist> _artistRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IFileService _fileService;
    private readonly IOptions<PullUrls> _pullUrls;

    public HomeController(UserManager<Core.Users.User> userManager,
                          IAuthorizationService authorizationService,
                          IMapper mapper, IRepository<Core.Artists.Artist> artistRepository, IFileService fileService,
                          IOptions<PullUrls> pullUrls)
    {
        _userManager = userManager;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _artistRepository = artistRepository;
        _fileService = fileService;
        _pullUrls = pullUrls;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString is null)
        {
            return Challenge();
        }

        var userId = int.Parse(userIdString);

        var artists = await _artistRepository.ListAsync(new ArtistsByUserIdSpec(userId));

        return View(artists);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Profile(int id)
    {
        var artist = await _artistRepository.GetBySpecAsync(new ArtistWithOwnersByIdSpec(id));

        if (artist is null)
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, artist, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        return View(artist);
    }

    [HttpPut("")]
    [FormValidator]
    public async Task<IActionResult> Update(UpdateArtistViewModel viewModel)
    {
        var artist = await _artistRepository.GetBySpecAsync(new ArtistWithOwnersByIdSpec(viewModel.Id));

        if (artist is null)
        {
            return FormResult.CreateErrorResult("No such artist");
        }

        if (await _authorizationService.AuthorizeAsync(User, artist, "SameAuthorPolicy") is {Succeeded: false})
        {
            return FormResult.CreateErrorResult("Access denied");
        }

        _mapper.Map(viewModel, artist);
        if (viewModel.Photo is not null)
        {
            var fileResult = await _fileService.Save(viewModel.Photo.ToStreamPart());
            if (!fileResult.IsSuccessStatusCode || fileResult.Content is null)
            {
                return FormResult.CreateErrorResult("Bad photo");
            }

            var r = await _fileService.Remove(artist.PhotoLocation[1..]);
            artist.PhotoLocation = fileResult.Content.Location;
        }

        await _artistRepository.UpdateAsync(artist);
        return FormResult.CreateSuccessResultWithObject(
            new {PhotoLocation = _pullUrls.Value.FileServer + artist.PhotoLocation}, "Updated successfully");
    }

    [HttpGet("[action]")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateArtistViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var fileResult = await _fileService.Save(viewModel.Photo.ToStreamPart());
        if (!fileResult.IsSuccessStatusCode || fileResult.Content is null)
        {
            ModelState.AddModelError(nameof(CreateArtistViewModel.Photo), "Bad photo");
            return View(viewModel);
        }

        var user = await _userManager.GetUserAsync(User);

        var artist = new Core.Artists.Artist
        {
            Name = viewModel.Name,
            About = viewModel.About,
            PhotoLocation = fileResult.Content.Location
        };
        artist.UserArtists = new List<UserArtist> {new() {User = user, Artist = artist}};
        artist = await _artistRepository.AddAsync(artist);

        return RedirectToAction("Profile", "Home", new {Area = "Artist", artist.Id});
    }
}