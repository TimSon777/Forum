using AutoMapper;
using LiWiMus.Core.LikedPlaylists;
using LiWiMus.Core.LikedPlaylists.Specifications;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Playlists.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Music.Controllers;

[Area(AreasConstants.Music)]
public class PlaylistController : Controller
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IRepository<Playlist> _playlistRepository;
    private readonly IRepository<LikedPlaylist> _likedPlaylists;
    private readonly IMapper _mapper;
    private readonly PullUrls _pullUrls;

    public PlaylistController(IAuthorizationService authorizationService, 
        IRepository<Playlist> playlistRepository, IMapper mapper, IRepository<LikedPlaylist> likedPlaylists, IOptions<PullUrls> options)
    {
        _authorizationService = authorizationService;
        _playlistRepository = playlistRepository;
        _mapper = mapper;
        _likedPlaylists = likedPlaylists;
        _pullUrls = options.Value;
    }

    private async Task<IActionResult> GetPlaylistAsync(int playlistId, bool inJson)
    {
        var playlist = await _playlistRepository.GetPlaylistDetailedAsync(playlistId);
        
        if (playlist is null)
        {
            return NotFound();
        }

        var isOwner = await _authorizationService.AuthorizeAsync(User, playlist, "SameAuthorPolicy")
            is { Succeeded: true };
        if (!isOwner && !playlist.IsPublic)
        {
            return Forbid();
        }

        var playlistVm = _mapper.Map<PlaylistViewModel>(playlist);
        playlistVm.IsOwner = isOwner;
        playlistVm.CountSubscribers = await _likedPlaylists.CountSubscribersAsync(playlist.Id);
        playlistVm.PrefixFiles = _pullUrls.FileServer;
        
        if (inJson)
        {
            return Json(playlistVm);
        }
        
        return View("Index", playlistVm);
    }
    [HttpGet]
    public async Task<IActionResult> Index(int playlistId)
    {
        return await GetPlaylistAsync(playlistId, false);
    }

    [HttpGet]
    public async Task<IActionResult> Json(int playlistId)
    {
        return await GetPlaylistAsync(playlistId, true);
    }
}