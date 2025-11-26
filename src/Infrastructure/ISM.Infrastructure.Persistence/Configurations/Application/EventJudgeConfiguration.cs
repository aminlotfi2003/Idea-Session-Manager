using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class EventJudgeConfiguration : IEntityTypeConfiguration<EventJudge>
{
    public void Configure(EntityTypeBuilder<EventJudge> builder)
    {
        builder.ToTable("EventJudges", Schemas.Application);

        builder.HasKey(ej => new { ej.InnovationEventId, ej.JudgeId });

        builder.HasOne(ej => ej.InnovationEvent)
            .WithMany(e => e.EventJudges)
            .HasForeignKey(ej => ej.InnovationEventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ej => ej.Judge)
            .WithMany(j => j.EventJudges)
            .HasForeignKey(ej => ej.JudgeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
