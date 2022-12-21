using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel.Data;

namespace Domain.Entities;

[Table("Users")]
public sealed class User : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public bool IsAdmin { get; set; }
    public ICollection<Connection> Connections { get; set; } = default!;
    public User? Mate { get; set; }

    public void AddConnection(string connectionId)
    {
        var connection = new Connection
        {
            ConnectionId = connectionId
        };
        
        Connections.Add(connection);
    }
    
    public void RemoveConnection(string connectionId)
    {
        var connection = Connections.First(c => c.ConnectionId == connectionId);
        Connections.Remove(connection);
    }
}