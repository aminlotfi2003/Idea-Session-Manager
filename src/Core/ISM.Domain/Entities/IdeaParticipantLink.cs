using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class IdeaParticipantLink : Entity
{
    private IdeaParticipantLink() { }

    public Guid IdeaId { get; private set; }
    public Idea Idea { get; private set; } = null!;

    public Guid ParticipantProfileId { get; private set; }
    public ParticipantProfile ParticipantProfile { get; private set; } = null!;

    public string EncryptedParticipantPayload { get; private set; } = default!;
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? RevealedAt { get; private set; }

    public static IdeaParticipantLink Create(Guid ideaId, Guid participantProfileId, string encryptedParticipantPayload)
    {
        if (string.IsNullOrWhiteSpace(encryptedParticipantPayload))
        {
            throw new ArgumentException("Encrypted participant payload must be provided.");
        }

        return new IdeaParticipantLink
        {
            IdeaId = ideaId,
            ParticipantProfileId = participantProfileId,
            EncryptedParticipantPayload = encryptedParticipantPayload,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    public void MarkRevealed()
    {
        RevealedAt = DateTimeOffset.UtcNow;
    }
}
