using CleanArchitectureDemo.Domain.Contracts;

namespace CleanArchitectureDemo.Domain.Entities;

public class Customer : AuditableEntity<Guid>
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}