namespace AiJobEx1.Domain.Entities;

public class UserDepartment
{
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
    public string? RoleInDepartment { get; set; }
    public bool IsPrimary { get; set; }

    public User User { get; set; } = default!;
    public Department Department { get; set; } = default!;
}
