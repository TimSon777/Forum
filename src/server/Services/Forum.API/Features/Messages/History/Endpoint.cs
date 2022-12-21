using Application.Abstractions;
using FluentValidation;
using MinimalApi.Endpoint;

namespace Forum.API.Features.Messages.History;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly IValidator<Request> _validator;
    private IMessageRepository MessageRepository { get; set; } = default!;

    public Endpoint(IValidator<Request> validator)
    {
        _validator = validator;
    }

    public async Task<IResult> HandleAsync(Request request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var messages = await MessageRepository.GetMessagesAsync(request.UserName, request.CountMessages);

        var result = messages.Select(Mapping.Map);
        return Results.Ok(result);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/history/{CountMessages}/{UserName}", async (IMessageRepository messageRepository, [AsParameters] Request request) =>
        {
            MessageRepository = messageRepository;
            return await HandleAsync(request);
        });
    }
}