using ISM.Domain.ValueObjects;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class ParticipantProfile : Entity, IAggregateRoot
{
    private ParticipantProfile() { }

    public Guid UserId { get; private set; }
    public string FullName { get; private set; } = default!;
    public string OrganizationUnitOrCustomerGroup { get; private set; } = default!;
    public ParticipantContactInfo ContactInfo { get; private set; } = default!;

    public static ParticipantProfile Create(
        Guid userId,
        string fullName,
        string organizationUnitOrCustomerGroup,
        ParticipantContactInfo contactInfo)
    {
        return new ParticipantProfile
        {
            UserId = userId,
            FullName = fullName,
            OrganizationUnitOrCustomerGroup = organizationUnitOrCustomerGroup,
            ContactInfo = contactInfo
        };
    }
}
