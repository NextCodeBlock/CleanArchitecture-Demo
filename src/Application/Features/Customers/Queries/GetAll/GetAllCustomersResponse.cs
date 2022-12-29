namespace CleanArchitectureDemo.Application.Features.Customers.Queries.GetAll;

public class GetAllCustomersResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Address { get; set; }
}