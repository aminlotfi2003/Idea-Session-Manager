using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class IdeaEvaluationConfiguration : IEntityTypeConfiguration<IdeaEvaluation>
{
    public void Configure(EntityTypeBuilder<IdeaEvaluation> builder)
    {
        builder.ToTable("IdeaEvaluations", Schemas.Application);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.IdeaId)
            .IsRequired();

        builder.Property(e => e.JudgeId)
            .IsRequired();

        builder.Property(e => e.EvaluationDate)
            .IsRequired();

        builder.Property(e => e.OverallDecision)
            .IsRequired();

        builder.Property(e => e.Comments)
            .HasMaxLength(2000);

        builder.HasMany(e => e.Scores)
            .WithOne(s => s.IdeaEvaluation)
            .HasForeignKey(s => s.IdeaEvaluationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
