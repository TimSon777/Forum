using AutoMapper;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Tracks.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.Music.Controllers;

[Area(AreasConstants.Music)]
public class TrackController : Controller
{
    private readonly IRepository<Track> _trackRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<Core.Users.User> _userRepository;

    public TrackController(IRepository<Track> trackRepository, IMapper mapper, IRepository<Core.Users.User> userRepository)
    {
        _trackRepository = trackRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int trackId)
    {
        var track = await _trackRepository.GetDetailedAsync(trackId);

        if (track is null)
        {
            return NotFound();
        }

        var trackVm = _mapper.Map<TrackViewModel>(track);
        trackVm.IsUserSubscribed = await _userRepository.IsUserSubscribeAsync(User.GetId()!.Value, trackId);
        return View(trackVm);
    }
}