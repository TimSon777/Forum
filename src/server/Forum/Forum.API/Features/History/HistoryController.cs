using FluentValidation;
using Forum.API.Features.History.Requests;
using Forum.API.Features.History.Responses;
using Forum.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Features.History;

[ApiController]
[Route("api/history")]
public sealed class HistoryController : ControllerBase
{
    private readonly IValidator<GetMessagesRequest> _validator;
    private readonly IMessageRepository _messageRepository;

    public HistoryController(IValidator<GetMessagesRequest> validator,
        IMessageRepository messageRepository)
    {
        _validator = validator;
        _messageRepository = messageRepository;
    }

    [HttpGet("{CountMessages:int}")]
    public async Task<ActionResult<GetMessageResponse[]>> GetMessagesAsync(GetMessagesRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest();
        }

        var messages = await _messageRepository.GetMessagesAsync(request.CountMessages);

        return messages.Select(Mapping.Map).ToArray();
    }
}