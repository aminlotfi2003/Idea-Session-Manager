using ISM.Domain.Enums;
using ISM.Domain.Identity;
using ISM.Domain.ValueObjects;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class ParticipantProfile : Entity, IAggregateRoot
{
    private ParticipantProfile() { }

    public Guid? ApplicationUserId { get; private set; }
    public ApplicationUser? ApplicationUser { get; private set; }

    public string FullName { get; private set; } = default!;
    public string OrganizationUnitOrCustomerGroup { get; private set; } = default!;
    public ParticipantContactInfo ContactInfo { get; private set; } = default!;
    public ParticipantType ParticipantType { get; private set; }
    public DateTimeOffset RegistrationDate { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public static ParticipantProfile Create(
        string fullName,
        string organizationUnitOrCustomerGroup,
        ParticipantContactInfo contactInfo,
        ParticipantType participantType,
        DateTimeOffset registrationDate,
        Guid? applicationUserId = null)
    {
        return new ParticipantProfile
        {
            ApplicationUserId = applicationUserId,
            FullName = fullName,
            OrganizationUnitOrCustomerGroup = organizationUnitOrCustomerGroup,
            ContactInfo = contactInfo,
            ParticipantType = participantType,
            RegistrationDate = registrationDate,
            CreatedAt = DateTimeOffset.UtcNow,
            IsActive = true
        };
    }

    public void LinkApplicationUser(Guid applicationUserId)
    {
        ApplicationUserId = applicationUserId;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
