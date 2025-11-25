using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.ValueObjects;

public sealed class IdeaCode : ValueObject
{
    public string Value { get; } = null!;

    private IdeaCode() { } // for EF

    private IdeaCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Idea code cannot be empty.", nameof(value));

        Value = value;
    }

    public static IdeaCode Create(string value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
