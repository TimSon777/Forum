using FormHelper;
using LiWiMus.Core.LikedSongs;
using LiWiMus.Core.Plans;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Tracks.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.User.Controllers;

[Area("User")]
public class TrackController : Controller
{
    private readonly IRepository<Track> _trackRepository;
    private readonly IRepository<LikedSong> _likedSongRepository;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IOptions<PullUrls> _pullUrls;

    public TrackController(IRepository<Track> trackRepository, UserManager<Core.Users.User> userManager,
                           IRepository<LikedSong> likedSongRepository, IOptions<PullUrls> pullUrls)
    {
        _trackRepository = trackRepository;
        _userManager = userManager;
        _likedSongRepository = likedSongRepository;
        _pullUrls = pullUrls;
    }

    [HttpGet]
    [Authorize(DefaultPermissions.Track.Download.Name)]
    public async Task<IActionResult> Download(int trackId)
    {
        var track = await _trackRepository.GetBySpecAsync(new TrackWithArtistsByIdSpecification(trackId));

        if (track is null)
        {
            return BadRequest();
        }

        return Redirect(_pullUrls.Value.FileServer + track.FileLocation);
    }

    [HttpPost]
    [FormValidator]
    public async Task<IActionResult> Like(int trackId)
    {
        var user = await _userManager.GetUserAsync(User);
        var track = await _trackRepository.GetByIdAsync(trackId);

        if (track is null)
        {
            return FormResult.CreateErrorResult("Track is not found.");
        }

        if (user.LikedSongs.Any(s => s.Track == track))
        {
            return FormResult.CreateErrorResult("Track has already added.");
        }

        var ls = new LikedSong
        {
            Track = track,
            User = user
        };
        
        await _likedSongRepository.AddAsync(ls);

        return FormResult.CreateSuccessResult("Ok");
    }
}