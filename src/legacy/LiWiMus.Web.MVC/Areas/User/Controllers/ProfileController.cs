using AutoMapper;
using FormHelper;
using LiWiMus.Core.Interfaces;
using LiWiMus.Core.Interfaces.Files;
using LiWiMus.Core.Users.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.User.ViewModels;
using LiWiMus.Web.Shared;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LiWiMus.Web.MVC.Areas.User.Controllers;

[Area("User")]
[Route("[area]/[controller]")]
public class ProfileController : Controller
{
    private readonly IAvatarService _avatarService;
    private readonly IMapper _mapper;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IRepository<Core.Users.User> _userRepository;
    private readonly IFileService _fileService;
    private readonly IOptions<PullUrls> _pullUrls;

    public ProfileController(UserManager<Core.Users.User> userManager,
        IMapper mapper, IAvatarService avatarService,
        IRepository<Core.Users.User> userRepository, IFileService fileService,
        IOptions<PullUrls> pullUrls)
    {
        _userManager = userManager;
        _mapper = mapper;
        _avatarService = avatarService;
        _userRepository = userRepository;
        _fileService = fileService;
        _pullUrls = pullUrls;
    }

    [HttpGet("{userName?}")]
    [AllowAnonymous]
    public async Task<IActionResult> Index(string userName)
    {
        ModelState.Clear();
        var currentUser = await _userManager.GetUserAsync(User);

        var user = string.IsNullOrEmpty(userName)
            ? currentUser
            : await _userManager.FindByNameAsync(userName);


        if (user is null)
        {
            return NotFound();
        }

        var profile = _mapper.Map<ProfileViewModel>(user);
        profile.IsAccountOwner = currentUser == user;
        var isFollower = await _userRepository.IsUserFollowAsync(currentUser.Id, user.Id);

        if (isFollower != null)
        {
            profile.IsSubscribed = isFollower.Value;
        }

        return View(profile);
    }

    [HttpGet("[action]/{userName?}")]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Profile", new {area = "User"});
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "User not found");
        }

        return View();
    }

    [HttpPost("[action]")]
    [FormValidator]
    public async Task<IActionResult> UpdateAsync(ProfileViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);

        _mapper.Map(model, user);
        
        if (user.Email != model.Email)
        {
            user.EmailConfirmed = false;
        }
        
        if (model.Avatar is not null)
        {
            var fileResult = await _fileService.Save(model.Avatar.ToStreamPart());
            if (!fileResult.IsSuccessStatusCode || fileResult.Content is null)
            {
                return FormResult.CreateErrorResult("Bad photo");
            }

            if (user.AvatarLocation is not null)
            {
                await _fileService.Remove(user.AvatarLocation[1..]);
            }

            user.AvatarLocation = fileResult.Content.Location;
        }

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded
            ? FormResult.CreateSuccessResult("Ok")
            : FormResult.CreateErrorResult(result.Errors.FirstOrDefault()?.Description ?? "Some error");
    }

    [HttpPost("[action]")]
    [FormValidator]
    public async Task<IActionResult> ChangeAvatarToRandom()
    {
        var user = await _userManager.GetUserAsync(User);
        await _avatarService.SetRandomAvatarAsync(user);
        await _userManager.UpdateAsync(user);
        return FormResult.CreateSuccessResultWithObject(
            new {AvatarLocation = _pullUrls.Value.FileServer + user.AvatarLocation},
            "Ok");
    }
}