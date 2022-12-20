using AutoMapper;
using FormHelper;
using LiWiMus.Core.Artists.Specifications;
using LiWiMus.Core.LikedArtists;
using LiWiMus.Core.Users.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.Shared;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.Music.Controllers;

[Area(AreasConstants.Music)]
public class ArtistController : Controller
{
    private readonly IRepository<Core.Artists.Artist> _artistRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IRepository<Core.Users.User> _userRepository;
    private readonly IOptions<PullUrls> _options;

    public ArtistController(IRepository<Core.Artists.Artist> artistRepository, IMapper mapper, UserManager<Core.Users.User> userManager, IRepository<Core.Users.User> userRepository, IOptions<PullUrls> options)
    {
        _artistRepository = artistRepository;
        _mapper = mapper;
        _userManager = userManager;
        _userRepository = userRepository;
        _options = options;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(int artistId)
    {
        ViewData["fileServer"] = _options.Value.FileServer;
        var artist = await _artistRepository.WithAlbumsAndOwnersAsync(artistId);
    
        if (artist is null)
        {
            return NotFound();
        }

        var artistVm = _mapper.Map<ArtistViewModel>(artist);
        artistVm.IsSubscribed = await _userRepository.IsUserSubscribeArtistAsync(User.GetId()!.Value, artistId);
        
        return View(artistVm);
    }

    [HttpPost, FormValidator]
    public async Task<IActionResult> SubscribeOrUnsubscribe(int artistId)
    {
        var artist = await _artistRepository.WithSubscribers(artistId);
        
        if (artist is null)
        {
            return NotFound();
        }

        var userId = User.GetId();
        var likedArtist = artist.Subscribers.FirstOrDefault(x => x.User.Id == userId);

        if (likedArtist is null)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            artist.Subscribers.Add(new LikedArtist
            {
                User = user
            });
        }
        else
        {
            artist.Subscribers.Remove(likedArtist);
        }
        
        await _artistRepository.SaveChangesAsync();
        return FormResult.CreateSuccessResult("Ok");
    }
}