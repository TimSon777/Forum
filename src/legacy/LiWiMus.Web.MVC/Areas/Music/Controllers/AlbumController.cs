using AutoMapper;
using FormHelper;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Albums.Specifications;
using LiWiMus.Core.LikedAlbums;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.Music.Controllers;

[Area(AreasConstants.Music)]
public class AlbumController : Controller
{
    private readonly IRepository<Album> _albumRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<LikedAlbum> _likedAlbumRepository;
    private readonly UserManager<Core.Users.User> _userManager;

    public AlbumController(IRepository<Album> albumRepository, IMapper mapper, IRepository<LikedAlbum> likedAlbumRepository, UserManager<Core.Users.User> userManager)
    {
        _albumRepository = albumRepository;
        _mapper = mapper;
        _likedAlbumRepository = likedAlbumRepository;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(int albumId)
    {
        var album = await _albumRepository.GetDetailedAlbumAsync(albumId, true);
        
        if (album is null)
        {
            return NotFound();
        }

        var albumVm = _mapper.Map<AlbumViewModel>(album);
        albumVm.IsSubscribed = album.Subscribers.Any(s => s.User.UserName == User.Identity!.Name);
        return View(albumVm);
    }

    [HttpPost, FormValidator]
    public async Task<IActionResult> SubscribeOrUnsubscribe(int albumId)
    {
        var userId = User.GetId();
        var album = await _albumRepository.GetAlbumWithSubscribersAsync(albumId);
        
        if (album is null)
        {
            return NotFound();
        }

        var likedAlbum = album.Subscribers.FirstOrDefault(s => s.User.Id == userId);
        if (likedAlbum is not null)
        {
            album.Subscribers.Remove(likedAlbum);
            await _albumRepository.SaveChangesAsync();
        }
        else
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            
            await _likedAlbumRepository.AddAsync(new LikedAlbum
            {
                User = user,
                Album = album
            });
        }

        return FormResult.CreateSuccessResult("Ok");
    }
}