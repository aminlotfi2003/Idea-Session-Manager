using ISM.Application.Abstractions.Services;

namespace ISM.Infrastructure.Identity.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
