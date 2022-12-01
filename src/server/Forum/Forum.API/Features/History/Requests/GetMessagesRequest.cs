using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Features.History.Requests;

public sealed class GetMessagesRequest
{
    [FromRoute]
    public int CountMessages { get; set; }
}