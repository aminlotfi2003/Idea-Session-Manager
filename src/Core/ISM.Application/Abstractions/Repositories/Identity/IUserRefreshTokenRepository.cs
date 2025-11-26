using ISM.Domain.Identity;

namespace ISM.Application.Abstractions.Repositories.Identity;

public interface IUserRefreshTokenRepository
{
    Task AddAsync(UserRefreshToken token, CancellationToken cancellationToken = default);
    Task<UserRefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default);
    Task RevokeUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
