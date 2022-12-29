using AutoMapper;
using CleanArchitectureDemo.Application.Repositories;
using CleanArchitectureDemo.Domain.Entities;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Customers.Commands.AddUpdate;

public class AddUpdateCustomerCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}

internal class AddUpdateCustomerCommandHandler : IRequestHandler<AddUpdateCustomerCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<Guid> _unitOfWork;

    public AddUpdateCustomerCommandHandler(IMapper mapper, IUnitOfWork<Guid> unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddUpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        Guid result;
        if (command.Id == Guid.Empty)
        {
            var customer = _mapper.Map<Customer>(command);
            await _unitOfWork.Repository<Customer>().AddAsync(customer, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
            result = customer.Id;
        }
        else
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(command.Id, cancellationToken);
            customer.Name = command.Name ?? customer.Name;
            customer.Email = command.Email ?? customer.Email;
            customer.Website = command.Website ?? customer.Website;
            customer.Phone = command.Phone ?? customer.Phone;
            customer.Fax = command.Fax ?? customer.Fax;
            customer.Address = command.Address ?? customer.Address;
            customer.IsActive = command.IsActive;
            await _unitOfWork.Repository<Customer>().UpdateAsync(customer, cancellationToken);
            result = customer.Id;
        }
        await _unitOfWork.Commit(cancellationToken);
        return result;
    }
}