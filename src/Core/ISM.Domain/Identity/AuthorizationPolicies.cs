namespace ISM.Domain.Identity;

public static class AuthorizationPolicies
{
    public const string AdminOnly = "AdminOnly";
    public const string JudgeOnly = "JudgeOnly";
    public const string ParticipantOnly = "ParticipantOnly";
}
