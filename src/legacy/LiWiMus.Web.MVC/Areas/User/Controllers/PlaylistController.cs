using AutoMapper;
using FormHelper;
using LiWiMus.Core.Interfaces.Files;
using LiWiMus.Core.LikedPlaylists;
using LiWiMus.Core.Plans;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Playlists.Specifications;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Tracks.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.MVC.Areas.User.ViewModels;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.User.Controllers;

[Area("User")]
public class PlaylistController : Controller
{
    private readonly IMapper _mapper;
    private readonly IRepository<Playlist> _playlistRepository;
    private readonly IRepository<LikedPlaylist> _likedPlaylistRepository;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IRepository<Track> _trackRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IFileService _fileService;

    public PlaylistController(IMapper mapper, IRepository<Playlist> playlistRepository,
                              UserManager<Core.Users.User> userManager,
                              IRepository<LikedPlaylist> likedPlaylistRepository, IRepository<Track> trackRepository,
                              IAuthorizationService authorizationService, IFileService fileService)
    {
        _mapper = mapper;
        _playlistRepository = playlistRepository;
        _userManager = userManager;
        _likedPlaylistRepository = likedPlaylistRepository;
        _trackRepository = trackRepository;
        _authorizationService = authorizationService;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int playlistId)
    {
        var playlist = await _playlistRepository.GetPlaylistDetailedAsync(playlistId);
        
        if (playlist is null)
        {
            return NotFound();
        }

        var playlistVm = _mapper.Map<PlaylistViewModel>(playlist);
        playlistVm.CountSubscribers = await _playlistRepository.GetCountSubscribersAsync(playlistId);
        playlistVm.IsSubscribed = await _playlistRepository.IsUserSubscribedAsync(User.GetId()!.Value, playlistId);
        return View(playlistVm);
    }

    [HttpPost]
    [Authorize(DefaultPermissions.Playlist.Private.Name)]
    public async Task<IActionResult> TogglePublicity(int playlistId)
    {
        var playlist = await _playlistRepository.GetPlaylistDetailedAsync(playlistId);

        if (playlist is null)
        {
            return NotFound();
        }
        
        if (await _authorizationService.AuthorizeAsync(User, playlist, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        playlist.IsPublic = !playlist.IsPublic;
        await _playlistRepository.UpdateAsync(playlist);

        return FormResult.CreateSuccessResult("Playlist is " + (playlist.IsPublic ? "public" : "private"));
    }

    [HttpPut]
    [FormValidator]
    public async Task<IActionResult> Update(UpdatePlaylistViewModel model)
    {
        var playlist = await _playlistRepository.GetPlaylistDetailedAsync(model.Id);
        if (playlist is null)
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, playlist, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        playlist.Name = model.Name;
        await _playlistRepository.UpdateAsync(playlist);

        return FormResult.CreateSuccessResult("Updated");
    }

    [HttpPost]
    [FormValidator]
    [Authorize(DefaultPermissions.Playlist.Cover.Name)]
    public async Task<IActionResult> UpdatePhoto(UpdatePlaylistPhotoViewModel model)
    {
        var playlist = await _playlistRepository.GetPlaylistDetailedAsync(model.Id);
        if (playlist is null)
        {
            return NotFound();
        }

        if (await _authorizationService.AuthorizeAsync(User, playlist, "SameAuthorPolicy") is {Succeeded: false})
        {
            return Forbid();
        }

        var fileResult = await _fileService.Save(model.Photo.ToStreamPart());
        if (!fileResult.IsSuccessStatusCode || fileResult.Content is null)
        {
            return FormResult.CreateErrorResult("Bad photo");
        }

        if (playlist.PhotoLocation is not null)
        {
            var r = await _fileService.Remove(playlist.PhotoLocation[1..]);
        }

        playlist.PhotoLocation = fileResult.Content.Location;

        await _playlistRepository.UpdateAsync(playlist);

        return FormResult.CreateSuccessResult("Updated");
    }

    [HttpGet]
    public async Task<IActionResult> Search(SearchForPlaylistViewModel model)
    {
        var tracks = await _trackRepository.SearchToPlaylistAsync(model.Title, model.PlaylistId);
        var trackVms = _mapper.Map<List<TrackForListViewModel>>(tracks);
        
        var result = new TracksPlaylistViewModel
        {
            Tracks = trackVms,
            PlaylistId = model.PlaylistId
        };
        return PartialView("TracksSearchPartial", result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost, FormValidator]
    public async Task<IActionResult> Create(CreatePlaylistViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return FormResult.CreateErrorResultWithObject(vm);
        }
        
        var user = await _userManager.GetUserAsync(User);

        var mappedPlaylist = _mapper.Map<Playlist>(vm);
        mappedPlaylist.Owner = user;
        var playlist = await _playlistRepository.AddAsync(mappedPlaylist);
        return FormResult.CreateSuccessResult("Ok", $"/User/Playlist/Index?playlistId={playlist.Id}");
    }

    [HttpPost, FormValidator]
    public async Task<IActionResult> SubscribeOrUnsubscribe(int playlistId)
    {
        var playlist = await _playlistRepository.WithSubscribersAndOwner(playlistId);
        
        if (playlist is null)
        {
            return FormResult.CreateErrorResult("Playlist is not found.");
        }

        var user = await _userManager.GetUserAsync(User);

        if (playlist.Owner == user)
        {
            return FormResult.CreateErrorResult("It is your playlist.");
        }

        var likedPlaylist = playlist.Subscribers.FirstOrDefault(likedPlaylist => likedPlaylist.User == user);
        if (likedPlaylist is not null)
        {
            playlist.Subscribers.Remove(likedPlaylist);
            await _playlistRepository.SaveChangesAsync();
        }
        else
        {
            var lp = new LikedPlaylist
            {
                Playlist = playlist,
                User = user
            };

            await _likedPlaylistRepository.AddAsync(lp);
        }

        return FormResult.CreateSuccessResult("Ok");
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> AddOrRemoveTrack(TrackPlaylistViewModel vm)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user is null)
        {
            return Forbid();
        }

        var track = await _trackRepository.GetByIdAsync(vm.TrackId);
        
        if (track is null)
        {
            return FormResult.CreateErrorResult("Track was not found");
        }
        
        var playlist = await _playlistRepository.WithTracksAsync(vm.PlaylistId);

        if (playlist is null)
        {
            return FormResult.CreateErrorResult("Playlist was not found");
        }

        var trackInPlaylist = playlist.Tracks.FirstOrDefault(x => x == track);

        if (trackInPlaylist is not null)
        {
            playlist.Tracks.Remove(track);
            await _playlistRepository.SaveChangesAsync();
            return FormResult.CreateSuccessResult("Ok");
        }
        
        playlist.Tracks.Add(track);
        await _playlistRepository.SaveChangesAsync();
        return FormResult.CreateSuccessResult("Ok");
    }
}