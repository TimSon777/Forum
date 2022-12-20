namespace LiWiMus.Core.Plans;

public static class DefaultPlans
{
    public static class Default
    {
        public const string Name = nameof(Default);
        public const string Description = "Default plan";
        public const int PricePerMonth = 0;
    }

    public static class Premium
    {
        public const string Name = nameof(Premium);
        public const string Description = "Premium plan";
        public const int PricePerMonth = 100;
    }

    public static IEnumerable<Plan> GetAll()
    {
        yield return new Plan
        {
            Name = Default.Name,
            Description = Default.Description,
            PricePerMonth = Default.PricePerMonth
        };

        yield return new Plan
        {
            Name = Premium.Name,
            Description = Premium.Description,
            PricePerMonth = Premium.PricePerMonth
        };
    }
}