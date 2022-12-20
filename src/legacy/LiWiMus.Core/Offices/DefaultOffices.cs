namespace LiWiMus.Core.Offices;

public class DefaultOffices
{
    public static IEnumerable<Office> GetAll()
    {
        return new List<Office>
        {
            new()
            {
                Id = 1,
                Address = new Address
                {
                    Country = "Russia",
                    Region = "Tatarstan",
                    City = "Kazan",
                    Street = "Kremlyovskaya",
                    BuildingNumber = "35"
                },
                Coordinate = new Coordinate(55.7921, 49.1221),
                Name = "Developer main headquarters"
            },
            new()
            {
                Id = 2,
                Address = new Address
                {
                    Country = "Russia",
                    Region = "Tatarstan",
                    City = "Kazan",
                    Street = "Pushkina Street",
                    BuildingNumber = "32"
                },
                Coordinate = new Coordinate(55.792038, 49.126168),
                Name = "Developer office branch 1"
            },
            new()
            {
                Id = 3,
                Address = new Address
                {
                    Country = "Russia",
                    Region = "Tatarstan",
                    City = "Kazan",
                    Street = "Derevnya Universiady",
                    BuildingNumber = "18"
                },
                Coordinate = new Coordinate(55.742711, 49.181801),
                Name = "Developer office branch 2"
            },
            new()
            {
                Id = 4,
                Address = new Address
                {
                    Country = "Russia",
                    Region = "Tatarstan",
                    City = "Kazan",
                    Street = "Professora Kamaya",
                    BuildingNumber = "10к4"
                },
                Coordinate = new Coordinate(55.748767, 49.182088),
                Name = "Developer office branch 3"
            },
            new()
            {
                Id = 5,
                Address = new Address
                {
                    Country = "Russia",
                    Region = "Tatarstan",
                    City = "Kazan",
                    Street = "Chingiza Aytmatova",
                    BuildingNumber = "8"
                },
                Coordinate = new Coordinate(55.764303, 49.234765),
                Name = "Developer office branch 4"
            }
        };
    }
}