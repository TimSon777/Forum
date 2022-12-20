using LiWiMus.Core.OnlineConsultants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiWiMus.Infrastructure.Data.Config;

public class OnlineConsultantConfiguration : IEntityTypeConfiguration<OnlineConsultant>
{
    public void Configure(EntityTypeBuilder<OnlineConsultant> builder)
    {
        builder
            .HasMany(oc => oc.Chats)
            .WithOne()
            .OnDelete(DeleteBehavior.SetNull);
    }
}