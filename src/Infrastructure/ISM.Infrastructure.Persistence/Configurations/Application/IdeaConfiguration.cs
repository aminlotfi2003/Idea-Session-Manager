using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class IdeaConfiguration : IEntityTypeConfiguration<Idea>
{
    public void Configure(EntityTypeBuilder<Idea> builder)
    {
        builder.ToTable("Ideas", Schemas.Application);

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(i => i.Requirements)
            .HasMaxLength(2000);

        builder.Property(i => i.ProposedImplementationMethod)
            .HasMaxLength(2000);

        builder.Property(i => i.ValueProposition)
            .HasMaxLength(2000);

        builder.Property(i => i.SubmissionDate)
            .IsRequired();

        builder.Property(i => i.Status)
            .IsRequired();

        builder.Property(i => i.FinalScore);

        builder.Property(i => i.OverallDecision)
            .IsRequired();

        builder.Property(i => i.Rank);

        builder.Property(i => i.EncryptedParticipantReferenceId)
            .IsRequired();

        // ValueObject IdeaCode
        builder.OwnsOne(i => i.IdeaCode, cfg =>
        {
            cfg.Property(x => x.Value)
               .HasColumnName("IdeaCode")
               .HasMaxLength(50)
               .IsRequired();
        });

        builder.HasMany(i => i.Evaluations)
            .WithOne(e => e.Idea)
            .HasForeignKey(e => e.IdeaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
