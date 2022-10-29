namespace Queue.Listener.Forum.Database.Repositories.Abstractions;

public interface IForumRepository
{
    Task SaveMessageAsync(string userName, string text, Guid? fileKey);
}