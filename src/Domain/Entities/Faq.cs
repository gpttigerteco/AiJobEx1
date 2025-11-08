namespace AiJobEx1.Domain.Entities;

public class Faq : BaseEntity
{
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;
    public Guid? DepartmentId { get; set; }
    public int Popularity { get; set; }

    public Department? Department { get; set; }
}
