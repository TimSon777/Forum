using AutoMapper;
using LiWiMus.Core.Artists.Specifications;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Playlists.Specifications;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Tracks.Specifications;
using LiWiMus.Core.Users.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.MVC.Areas.User.ViewModels;
using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.User.Controllers;

[Area(AreasConstants.User)]
public class MediaLibrary : Controller
{
    private readonly IRepository<Core.Users.User> _userRepository;
    private readonly IRepository<Playlist> _playlistRepository;
    private readonly IRepository<Core.Artists.Artist> _artistRepository;
    private readonly IRepository<Track> _trackRepository;
    private readonly IMapper _mapper;

    public MediaLibrary(IRepository<Core.Users.User> userRepository, IRepository<Playlist> playlistRepository, 
        IRepository<Core.Artists.Artist> artistRepository, IRepository<Track> trackRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _playlistRepository = playlistRepository;
        _artistRepository = artistRepository;
        _trackRepository = trackRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Playlists()
    {
        var userId = User.GetId()!.Value;
        var playlists = await _playlistRepository.ByUserIdAsync(userId);
        var playlistVms = _mapper.Map<List<PlaylistGeneralInfoViewModel>>(playlists);

        var playlistsSubscribed = await _playlistRepository.SubscribedAsync(userId);
        var playlistSubscribedVms = _mapper.Map<List<PlaylistGeneralInfoViewModel>>(playlistsSubscribed);

        var vm = new MediaLibraryPlaylistsVewMode
        {
            Playlists = playlistVms,
            SubscribedPlaylists = playlistSubscribedVms
        };

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var userId = User.GetId()!.Value;
        var users = await _userRepository.FollowingAsync(userId);
        var userVms = _mapper.Map<List<UserGeneralInfoViewModel>>(users);
        return View(userVms);
    }

    [HttpGet]
    public async Task<IActionResult> Artists()
    {
        var userId = User.GetId()!.Value;
        var artists = await _artistRepository.SubscribedByUserIdAsync(userId);
        var artistVms = _mapper.Map<List<ArtistGeneralInfoViewModel>>(artists);
        return View(artistVms);
    }

    [HttpGet]
    public async Task<IActionResult> Tracks()
    {
        var userId = User.GetId()!.Value;
        var tracks = await _trackRepository.SubscribedAsync(userId);
        var trackVms = _mapper.Map<List<TrackViewModel>>(tracks);
        return View(trackVms);
    }
}