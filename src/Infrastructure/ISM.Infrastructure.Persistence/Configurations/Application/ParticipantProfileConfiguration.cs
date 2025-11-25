using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class ParticipantProfileConfiguration : IEntityTypeConfiguration<ParticipantProfile>
{
    public void Configure(EntityTypeBuilder<ParticipantProfile> builder)
    {
        builder.ToTable("Participants", Schemas.Application);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.OrganizationUnitOrCustomerGroup)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<ParticipantProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(p => p.ContactInfo, cfg =>
        {
            cfg.Property(c => c.Email)
                .HasColumnName("Email")
                .HasMaxLength(200)
                .IsRequired();

            cfg.Property(c => c.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(50)
                .IsRequired();
        });
    }
}
