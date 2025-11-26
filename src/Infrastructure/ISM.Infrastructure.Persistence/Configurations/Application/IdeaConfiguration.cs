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

        builder.Property(i => i.ProposedImplementation)
            .HasMaxLength(2000);

        builder.Property(i => i.ValueProposition)
            .HasMaxLength(2000);

        builder.Property(i => i.SubmittedAt)
            .IsRequired();

        builder.Property(i => i.Status)
            .IsRequired();

        builder.Property(i => i.FinalScore);

        builder.Property(i => i.FinalDecision)
            .IsRequired();

        builder.Property(i => i.Rank);

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

        builder.HasOne(i => i.ConfidentialLink)
            .WithOne(l => l.Idea)
            .HasForeignKey<IdeaParticipantLink>(l => l.IdeaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
