using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiJobEx1.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Department> Departments { get; }
    DbSet<UserDepartment> UserDepartments { get; }
    DbSet<JobDescription> JobDescriptions { get; }
    DbSet<JobDescriptionChangeRequest> JobDescriptionChangeRequests { get; }
    DbSet<Document> Documents { get; }
    DbSet<AiSession> AiSessions { get; }
    DbSet<AiMessage> AiMessages { get; }
    DbSet<Faq> Faqs { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
