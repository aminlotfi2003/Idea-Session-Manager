using ISM.Application.Common.Abstractions.Services;

namespace ISM.Application.Common.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
