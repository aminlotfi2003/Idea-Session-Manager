using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class EvaluationScoreConfiguration : IEntityTypeConfiguration<EvaluationScore>
{
    public void Configure(EntityTypeBuilder<EvaluationScore> builder)
    {
        builder.ToTable("EvaluationScores", Schemas.Application);

        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.IdeaEvaluation)
            .WithMany(e => e.Scores)
            .HasForeignKey(s => s.IdeaEvaluationId);

        builder.HasOne(s => s.EvaluationCriteria)
            .WithMany(c => c.Scores)
            .HasForeignKey(s => s.EvaluationCriteriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(s => s.Score)
            .IsRequired();

        builder.Property(s => s.Comment)
            .HasMaxLength(1000);
    }
}
