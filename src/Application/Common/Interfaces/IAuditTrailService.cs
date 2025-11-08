using AiJobEx1.Domain.Entities;

namespace AiJobEx1.Application.Common.Interfaces;

public interface IAuditTrailService
{
    Task WriteAsync(AuditLog log, CancellationToken cancellationToken = default);
}
