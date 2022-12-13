namespace Infrastructure.Abstractions;

public interface IFileMover
{
    Task<bool> MoveToPersistenceAsync(Guid fileKey);
}