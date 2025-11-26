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

        builder.Property(j => j.ApplicationUserId)
            .IsRequired();

        builder.HasOne(j => j.ApplicationUser)
            .WithOne()
            .HasForeignKey<Judge>(j => j.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(j => j.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(j => j.ExpertiseAreas)
            .HasMaxLength(500);

        builder.Property(j => j.IsActive)
            .IsRequired();

        builder.HasMany(j => j.EventJudges)
            .WithOne(ej => ej.Judge)
            .HasForeignKey(ej => ej.JudgeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(j => j.Evaluations)
            .WithOne(e => e.Judge)
            .HasForeignKey(e => e.JudgeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
