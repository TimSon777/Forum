using FormHelper;
using LiWiMus.Core.FollowingUsers;
using LiWiMus.Core.LikedSongs;
using LiWiMus.Core.Shared;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Tracks.Specifications;
using LiWiMus.Core.Users.Interfaces;
using LiWiMus.Core.Users.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.User.Controllers;

[Area(AreasConstants.User)]
public class FollowingController : Controller
{
    private readonly IRepository<Core.Users.User> _userRepository;
    private readonly IRepository<Track> _trackRepository;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IUserService _userService;

    public FollowingController(IRepository<Core.Users.User> userRepository, UserManager<Core.Users.User> userManager, IRepository<Track> trackRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _trackRepository = trackRepository;
        _userService = userService;
    }

    [HttpPost, FormValidator]
    public async Task<JsonResult> SubscribeOrUnsubscribeUser(string username)
    {
        var userId = User.GetId();
        
        if (userId is null)
        {
            return FormResult.CreateErrorResult("You bad");
        }
        
        var currentUser = await _userRepository.WithFollowingsAsync(userId.Value);

        if (currentUser is null)
        {
            return FormResult.CreateErrorResult("You bad");
        }
        
        var otherUser = await _userManager.FindByNameAsync(username);

        if (otherUser is null)
        {
            return FormResult.CreateErrorResult("You bad");
        }

        if (otherUser == currentUser)
        {
            return FormResult.CreateWarningResult("It is your account");
        }

        var subscription = await _userService.SubscribeOrUnsubscribeAsync(currentUser, otherUser);
        return subscription switch
        {
            Subscription.Subscribed => FormResult.CreateSuccessResult("Sub"),
            Subscription.Unsubscribed => FormResult.CreateSuccessResult("Unsub"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpPost, FormValidator]
    public async Task<IActionResult> SubscribeOrUnsubscribeTrack(int trackId)
    {
        var track = await _trackRepository.WithSubscribersAsync(trackId);
        
        if (track is null)
        {
            return FormResult.CreateErrorResult("Track was not found");
        }

        var userId = User.GetId();
        
        var likedSong = track.Subscribers.FirstOrDefault(song => song.User.Id == userId);

        string okMessage;
        if (likedSong is null)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
            {
                return FormResult.CreateErrorResult("You aren't login");
            }
            
            track.Subscribers.Add(new LikedSong
            {
                User = user
            });
            okMessage = "You just subscribed";
        }
        else
        {
            track.Subscribers.Remove(likedSong);
            okMessage = "You just unsubscribed";
        }

        await _trackRepository.SaveChangesAsync();
        return FormResult.CreateSuccessResult(okMessage);
    }
}