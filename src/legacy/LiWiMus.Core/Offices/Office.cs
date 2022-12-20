namespace LiWiMus.Core.Offices;

public class Office : BaseEntity
{
    public Coordinate Coordinate { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public string Name { get; set; } = null!;
}