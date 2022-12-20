using Ardalis.GuardClauses;

namespace LiWiMus.SharedKernel.Extensions;

public static class RandomExtensions
{
    public static bool TryProbability(this Random random, double percentage)
    {
        Guard.Against.OutOfRange(percentage, nameof(percentage), 0, 100);
        return random.NextDouble() * 100 < percentage;
    }
}