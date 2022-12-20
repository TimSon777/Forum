using AutoMapper;
using LiWiMus.Core.Genres;
using LiWiMus.Core.Genres.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Music.Controllers;

[Area(AreasConstants.Music)]
public class GenreController : Controller
{
    private readonly IRepository<Genre> _genreRepository;
    private readonly IMapper _mapper;
    private readonly IOptions<PullUrls> _options;

    public GenreController(IRepository<Genre> genreRepository, IMapper mapper, IOptions<PullUrls> options)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
        _options = options;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int genreId)
    {
        ViewData["fileServer"] = _options.Value.FileServer;
        
        var genre = await _genreRepository.GenreWithPopularSongsAsync(genreId);

        if (genre is null)
        {
            return NotFound();
        }

        var genreVm = _mapper.Map<GenreViewModel>(genre);
        
        return View(genreVm);
    }
}