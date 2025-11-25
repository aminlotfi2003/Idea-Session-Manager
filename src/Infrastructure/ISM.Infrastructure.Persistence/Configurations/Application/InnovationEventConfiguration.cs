using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class InnovationEventConfiguration : IEntityTypeConfiguration<InnovationEvent>
{
    public void Configure(EntityTypeBuilder<InnovationEvent> builder)
    {
        builder.ToTable("InnovationEvents", Schemas.Application);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(e => e.Goals)
            .HasMaxLength(2000);

        builder.Property(e => e.AllowedParticipantGroups)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Status)
            .IsRequired();

        builder.Property(e => e.RulesDocumentPath)
            .HasMaxLength(500);

        builder.HasMany(e => e.EvaluationCriteria)
            .WithOne(i => i.InnovationEvent)
            .HasForeignKey(c => c.InnovationEventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Ideas)
            .WithOne(i => i.InnovationEvent)
            .HasForeignKey(i => i.InnovationEventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
