using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class JudgeConfiguration : IEntityTypeConfiguration<Judge>
{
    public void Configure(EntityTypeBuilder<Judge> builder)
    {
        builder.ToTable("Judges", Schemas.Application);

        builder.HasKey(j => j.Id);

        builder.HasOne(j => j.User)
            .WithOne()
            .HasForeignKey<Judge>(j => j.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(j => j.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(j => j.ExpertiseAreas)
            .HasMaxLength(500);

        builder.Property(j => j.IsActive)
            .IsRequired();
    }
}
