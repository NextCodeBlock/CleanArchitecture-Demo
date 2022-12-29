using AutoMapper;
using CleanArchitectureDemo.Application.Repositories;
using CleanArchitectureDemo.Domain.Entities;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Customers.Commands.AddUpdate;

public class DeleteCustomerCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
}

internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Guid>
{
    private readonly IUnitOfWork<Guid> _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteCustomerCommandHandler(IMapper mapper, IUnitOfWork<Guid> unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(command.CustomerId, cancellationToken);
        await _unitOfWork.Repository<Customer>().DeleteAsync(customer);
        await _unitOfWork.Commit(cancellationToken);
        return customer.Id;
    }
}