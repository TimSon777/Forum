using AutoMapper;
using LiWiMus.Core.Genres;
using LiWiMus.Core.Genres.Specifications;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Search.ViewModels;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;
using LiWiMus.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Search.Controllers;

[Area(AreasConstants.Search)]
public class GenreController : Controller
{
    private readonly IRepository<Genre> _genreRepository;
    private readonly IMapper _mapper;
    private readonly IOptions<PullUrls> _options;

    public GenreController(IRepository<Genre> genreRepository, 
        IMapper mapper, IOptions<PullUrls> options)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
        _options = options;
    }
    
    private async Task<IEnumerable<GenreForListViewModel>> GetGenresAsync(SearchViewModel searchVm)
    {
        var pagination = _mapper.Map<Pagination>(searchVm);
        var genres = await _genreRepository.PaginateAsync(pagination);
        return _mapper.Map<List<Genre>, List<GenreForListViewModel>>(genres);
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["fileServer"] = _options.Value.FileServer;
        var genres = await GetGenresAsync(SearchViewModel.Default);
        return View(genres);
    }
    
    [HttpGet]
    public async Task<IActionResult> ShowMore(SearchViewModel searchVm)
    {
        var genres = await GetGenresAsync(searchVm);
        return PartialView("GenresPartial", genres);
    }
}