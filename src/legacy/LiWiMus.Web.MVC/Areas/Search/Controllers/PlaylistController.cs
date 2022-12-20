using AutoMapper;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Playlists.Specifications;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Search.ViewModels;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;
using LiWiMus.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Search.Controllers;

[Area(AreasConstants.Search)]
public class PlaylistController : Controller
{
    private readonly IRepository<Playlist> _playlistRepository;
    private readonly IMapper _mapper;
    private readonly IOptions<PullUrls> _options;

    public PlaylistController(IRepository<Playlist> playlistRepository, 
        IMapper mapper, IOptions<PullUrls> options)
    {
        _playlistRepository = playlistRepository;
        _mapper = mapper;
        _options = options;
    }
    
    private async Task<IEnumerable<PlaylistForListViewModel>> GetPlaylistsAsync(SearchViewModel searchVm)
    {
        var pagination = _mapper.Map<Pagination>(searchVm);
        var playlists = await _playlistRepository.PaginateWithTitleAsync(pagination);
        var playlistVms = _mapper.Map<List<Playlist>, List<PlaylistForListViewModel>>(playlists);
        
        foreach (var vm in playlistVms)
        {
            vm.IsOwner = User.Identity?.Name == vm.Owner.UserName;
        }

        return playlistVms;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["fileServer"] = _options.Value.FileServer;
        var playlists = await GetPlaylistsAsync(SearchViewModel.Default);
        return View(playlists);
    }

    [HttpGet]
    public async Task<IActionResult> ShowMore(SearchViewModel searchVm)
    {
        var playlists = await GetPlaylistsAsync(searchVm);
        return PartialView("PlaylistsPartial", playlists);
    }
}