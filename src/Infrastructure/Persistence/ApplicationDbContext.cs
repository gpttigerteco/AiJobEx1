using AiJobEx1.Application.Common.Interfaces;
using AiJobEx1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiJobEx1.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<UserDepartment> UserDepartments => Set<UserDepartment>();
    public DbSet<JobDescription> JobDescriptions => Set<JobDescription>();
    public DbSet<JobDescriptionChangeRequest> JobDescriptionChangeRequests => Set<JobDescriptionChangeRequest>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<AiSession> AiSessions => Set<AiSession>();
    public DbSet<AiMessage> AiMessages => Set<AiMessage>();
    public DbSet<Faq> Faqs => Set<Faq>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
