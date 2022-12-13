using Application.Abstractions;
using FluentValidation;
using MinimalApi.Endpoint;

namespace Forum.API.Features.Messages.History;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly IValidator<Request> _validator;
    private readonly IMessageRepository _messageRepository;

    public Endpoint(IValidator<Request> validator, IMessageRepository messageRepository)
    {
        _validator = validator;
        _messageRepository = messageRepository;
    }

    public async Task<IResult> HandleAsync([AsParameters]Request request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var messages = await _messageRepository.GetMessagesAsync(request.CountMessages);

        var result = messages.Select(Mapping.Map);
        return Results.Ok(result);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("history/{CountMessages}", HandleAsync);
    }
}