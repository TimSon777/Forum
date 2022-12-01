namespace Forum.Queue.Listener.Abstractions;

public interface IForumRepository
{
    Task SaveMessageAsync(string userName, string text, string? fileKey);
}