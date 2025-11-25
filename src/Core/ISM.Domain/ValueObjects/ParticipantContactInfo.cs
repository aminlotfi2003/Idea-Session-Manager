using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.ValueObjects;

public sealed class ParticipantContactInfo : ValueObject
{
    public string Email { get; } = null!;
    public string PhoneNumber { get; } = null!;

    private ParticipantContactInfo() { } // for EF

    private ParticipantContactInfo(string email, string phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static ParticipantContactInfo Create(string email, string phoneNumber)
        => new(email, phoneNumber);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Email;
        yield return PhoneNumber;
    }
}
