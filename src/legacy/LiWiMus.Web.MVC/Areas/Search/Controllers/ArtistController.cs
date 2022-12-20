using AutoMapper;
using LiWiMus.Core.Artists.Specifications;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Search.ViewModels;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;
using LiWiMus.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Search.Controllers;

[Area(AreasConstants.Search)]
public class ArtistController : Controller
{
    private readonly IRepository<Core.Artists.Artist> _artistRepository;
    private readonly IMapper _mapper;
    private readonly IOptions<PullUrls> _options;

    public ArtistController(IRepository<Core.Artists.Artist> artistRepository, 
        IMapper mapper, IOptions<PullUrls> options)
    {
        _artistRepository = artistRepository;
        _mapper = mapper;
        _options = options;
    }
    
    private async Task<IEnumerable<ArtistForListViewModel>> GetArtistsAsync(SearchViewModel searchVm)
    {
        var pagination = _mapper.Map<Pagination>(searchVm);
        var artists = await _artistRepository.PaginateAsync(pagination);
        return _mapper.Map<List<Core.Artists.Artist>, List<ArtistForListViewModel>>(artists);
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["fileServer"] = _options.Value.FileServer;
        var artists = await GetArtistsAsync(SearchViewModel.Default);
        return View(artists);
    }

    [HttpGet]
    public async Task<IActionResult> ShowMore(SearchViewModel searchVm)
    {
        var artists = await GetArtistsAsync(searchVm);
        return PartialView("ArtistsPartial", artists);
    }
}