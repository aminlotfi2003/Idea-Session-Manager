using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class EvaluationCriteriaConfiguration : IEntityTypeConfiguration<EvaluationCriteria>
{
    public void Configure(EntityTypeBuilder<EvaluationCriteria> builder)
    {
        builder.ToTable("EvaluationCriteria", Schemas.Application);

        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.InnovationEvent)
            .WithMany(x => x.EvaluationCriteria)
            .HasForeignKey(c => c.InnovationEventId);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .HasMaxLength(1000);

        builder.Property(c => c.MinScore)
            .IsRequired();

        builder.Property(c => c.MaxScore)
            .IsRequired();

        builder.Property(c => c.Weight)
            .IsRequired();

        builder.Property(c => c.Order)
            .IsRequired();
    }
}
