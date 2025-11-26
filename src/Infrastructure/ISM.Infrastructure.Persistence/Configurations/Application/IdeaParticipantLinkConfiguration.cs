using ISM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISM.Infrastructure.Persistence.Configurations.Application;

public class IdeaParticipantLinkConfiguration : IEntityTypeConfiguration<IdeaParticipantLink>
{
    public void Configure(EntityTypeBuilder<IdeaParticipantLink> builder)
    {
        builder.ToTable("IdeaParticipantLinks", Schemas.Application);

        builder.HasKey(l => l.Id);

        builder.Property(l => l.EncryptedParticipantPayload)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(l => l.CreatedAt)
            .IsRequired();

        builder.HasOne(l => l.Idea)
            .WithOne(i => i.ConfidentialLink)
            .HasForeignKey<IdeaParticipantLink>(l => l.IdeaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.ParticipantProfile)
            .WithMany()
            .HasForeignKey(l => l.ParticipantProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
