using ISM.Application.Common.Abstractions.Repositories.Identity;
using ISM.Application.Common.Abstractions.Services;
using ISM.Domain.Identity;
using ISM.SharedKernel.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ISM.Application.Common.Services;

public sealed class PasswordPolicyService : IPasswordPolicyService
{
    private readonly IUserPasswordHistoryRepository _historyRepository;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IDateTimeProvider _clock;
    private const int HistoryLength = 5;

    public PasswordPolicyService(
        IUserPasswordHistoryRepository historyRepository,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IDateTimeProvider clock)
    {
        _historyRepository = historyRepository;
        _passwordHasher = passwordHasher;
        _clock = clock;
    }

    public async Task EnsurePasswordCompliesAsync(ApplicationUser user, string newPassword, CancellationToken cancellationToken = default)
    {
        var recent = await _historyRepository.GetRecentAsync(user.Id, HistoryLength, cancellationToken);

        foreach (var history in recent)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, history.PasswordHash, newPassword);
            if (result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded)
                throw new BusinessRuleViolationException("New password cannot match any of the last 5 passwords.");
        }
    }

    public async Task RecordPasswordChangeAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken = default)
    {
        await _historyRepository.AddAsync(UserPasswordHistory.Create(user.Id, passwordHash, _clock.UtcNow), cancellationToken);
        await _historyRepository.PruneExcessAsync(user.Id, HistoryLength, cancellationToken);
        await _historyRepository.SaveChangesAsync(cancellationToken);
    }

    public bool IsPasswordExpired(ApplicationUser user, DateTimeOffset utcNow, int days = 90)
    {
        var lastChanged = user.PasswordLastChangedAt ?? user.CreatedAt;
        return lastChanged.AddDays(days) <= utcNow;
    }
}
