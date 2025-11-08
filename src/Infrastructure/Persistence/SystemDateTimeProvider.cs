using AiJobEx1.Application.Common.Interfaces;

namespace AiJobEx1.Infrastructure.Persistence;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
