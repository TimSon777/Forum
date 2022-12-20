namespace LiWiMus.Infrastructure.Data.Seeders;

public interface ISeeder
{
    Task SeedAsync(EnvironmentType environmentType);
    int Priority { get; }
}