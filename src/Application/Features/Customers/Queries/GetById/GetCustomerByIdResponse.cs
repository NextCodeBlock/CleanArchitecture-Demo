namespace CleanArchitectureDemo.Application.Features.Customers.Queries.GetById;

public class GetCustomerByIdResponse
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Address { get; set; }
}