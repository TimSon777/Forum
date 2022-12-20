using LiWiMus.Core.Offices;
using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.Areas.Company;

public class OfficeDto : HasId
{
    public Coordinate Coordinate { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public string Name { get; set; } = null!;
}