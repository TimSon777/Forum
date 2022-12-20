using AutoMapper;
using LiWiMus.Core.Chats.Enums;
using LiWiMus.Core.Messages;
using LiWiMus.Core.Messages.Specifications;
using LiWiMus.Core.OnlineConsultants;
using LiWiMus.Core.OnlineConsultants.Specifications;
using LiWiMus.Core.Roles;
using LiWiMus.Core.Users.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.MVC.Areas.User.ViewModels;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.User.Controllers;

[Area("User")]
public class SupportChatController : Controller
{
    private readonly IMapper _mapper;
    private readonly IRepository<OnlineConsultant> _repositoryConsultant;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IRepository<Core.Users.User> _userRepository;
    private readonly IRepository<Message> _messageRepository;

    public SupportChatController(IRepository<OnlineConsultant> repositoryConsultant,
                                 UserManager<Core.Users.User> userManager,
                                 IMapper mapper, IRepository<Core.Users.User> userRepository, IRepository<Message> messageRepository)
    {
        _repositoryConsultant = repositoryConsultant;
        _userManager = userManager;
        _mapper = mapper;
        _userRepository = userRepository;
        _messageRepository = messageRepository;
    }

    [HttpGet]
    [Authorize(DefaultSystemPermissions.Chat.Answer.Name)]
    public IActionResult Chats()
    {
        return View();
    }

    [HttpGet]
    [Authorize(DefaultSystemPermissions.Chat.Answer.Name)]
    public async Task<IActionResult> GetTextingUsersChats()
    {
        var user = await _userManager.GetUserAsync(User);

        var onlineConsultant = await _repositoryConsultant
            .GetBySpecAsync(new ConsultantByUser(user));

        var userNames = onlineConsultant?.Chats
                                        .Where(ch => ch.Status == ChatStatus.Opened)
                                        .Select(ch => ch.User.UserName);

        return Json(userNames);
    }

    [HttpGet]
    //[Authorize(Roles = "Consultant")]
    public async Task<IActionResult> ChatForConsultant(string userName)
    {
        var user = await _userManager.GetUserAsync(User);
        var consultant = await _repositoryConsultant.GetBySpecAsync(new ConsultantByUser(user));

        var chat = consultant?.Chats.FirstOrDefault(ch => ch.User.UserName == userName && ch.Status == ChatStatus.Opened);

        if (chat is null)
        {
            return BadRequest();
        }

        var chatVm = _mapper.Map<ChatViewModel>(chat);

        return PartialView("~/Areas/User/Views/Shared/ChatConsultantPartial.cshtml", 
            (chatVm, user.UserName));
    }
    
    [HttpGet]
    public async Task<IActionResult> ChatForUser()
    {
        var id = User.GetId();
        var user = await _userRepository.GetBySpecAsync(new UserWithChatsByIdSpec(id!.Value));
        var chat = user!.UserChats.FirstOrDefault(c => c.Status == ChatStatus.Opened);

        if (chat is null)
        {
            return BadRequest("Chat is not found");
        }

        var chatVm = _mapper.Map<ChatViewModel>(chat);

        return PartialView("~/Areas/User/Views/Shared/ChatUserPartial.cshtml", 
            (chatVm, user.UserName));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var id = User.GetId();
        var user = await _userRepository.GetBySpecAsync(new UserWithChatsByIdSpec(id!.Value));
        var chat = user!.UserChats.FirstOrDefault(c => c.Status == ChatStatus.Opened);
        var chatVm = _mapper.Map<ChatViewModel>(chat);
        return View(chatVm);
    }

    [HttpGet]
    public async Task<IActionResult> MessageForUser(int messageId, bool isOwner)
    {
        return await GetMessageResultAsync(messageId, true, isOwner);
    }
    
    [HttpGet]
    //[Authorize(DefaultPermissions.Chat.Answer)]
    public async Task<IActionResult> MessageForConsultant(int messageId, bool isOwner)
    {
        return await GetMessageResultAsync(messageId, false, isOwner);
    }

    private async Task<IActionResult> GetMessageResultAsync(int messageId, bool isForUser, bool isOwner)
    {
        var message = await _messageRepository.GetBySpecAsync(new MessageWithSenderByIdSpec(messageId));
        
        if (message is null)
        {
            return BadRequest("Message is not found");
        }

        var messageVm = _mapper.Map<MessageViewModel>(message);

        return isForUser
            ? PartialView("~/Areas/User/Views/Shared/MessageConsultantPartial.cshtml", (messageVm, isOwner))
            : PartialView("~/Areas/User/Views/Shared/MessageUserPartial.cshtml", (messageVm, isOwner));
    }
}