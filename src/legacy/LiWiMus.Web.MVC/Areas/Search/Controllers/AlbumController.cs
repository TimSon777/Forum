using AutoMapper;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Albums.Specifications;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Search.ViewModels;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;
using LiWiMus.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Search.Controllers;

[Area(AreasConstants.Search)]
public class AlbumController : Controller
{
    private readonly IOptions<PullUrls> _options;
    private readonly IRepository<Album> _albumRepository;
    private readonly IMapper _mapper;

    public AlbumController(IRepository<Album> albumRepository, 
        IMapper mapper, IOptions<PullUrls> options)
    {
        _albumRepository = albumRepository;
        _mapper = mapper;
        _options = options;
    }
    
    private async Task<IEnumerable<AlbumForListViewModel>> GetAlbumsAsync(SearchViewModel searchVm)
    {
        var pagination = _mapper.Map<Pagination>(searchVm);
        var albums = await _albumRepository.PaginateWithTitleAsync(pagination);
        return _mapper.Map<List<Album>, List<AlbumForListViewModel>>(albums);
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["fileServer"] = _options.Value.FileServer;
        var albums = await GetAlbumsAsync(SearchViewModel.Default);
        return View(albums);
    }

    [HttpGet]
    public async Task<IActionResult> ShowMore(SearchViewModel searchVm)
    {
        var albums = await GetAlbumsAsync(searchVm);
        return PartialView("AlbumsPartial", albums);
    }
}