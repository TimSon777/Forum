using LiWiMus.Core.Offices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiWiMus.Infrastructure.Data.Config;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        var offices = DefaultOffices.GetAll().ToList();
        var addresses = new List<object>();
        var coordinates = new List<object>();

        foreach (var office in offices)
        {
            addresses.Add(new
            {
                OfficeId = office.Id, office.Address.Country, office.Address.Region, office.Address.City,
                office.Address.Street, office.Address.BuildingNumber
            });
            coordinates.Add(new {OfficeId = office.Id, office.Coordinate.Latitude, office.Coordinate.Longitude});

            office.Address = null!;
            office.Coordinate = null!;
        }

        builder.HasData(offices);

        builder.OwnsOne(office => office.Coordinate).HasData(coordinates);
        builder.OwnsOne(office => office.Address).HasData(addresses);
    }
}