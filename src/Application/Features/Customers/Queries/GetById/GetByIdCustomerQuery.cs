using AutoMapper;
using CleanArchitectureDemo.Application.Repositories;
using CleanArchitectureDemo.Domain.Entities;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Customers.Queries.GetById;

public class GetByIdCustomerQuery : IRequest<GetCustomerByIdResponse>
{
    public Guid CustomerId { get; set; }
}

internal class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, GetCustomerByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<Guid> _unitOfWork;

    public GetByIdCustomerQueryHandler(IMapper mapper, IUnitOfWork<Guid> unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCustomerByIdResponse> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(request.CustomerId, cancellationToken);
        var mappedCustomer = _mapper.Map<GetCustomerByIdResponse>(customer);
        return mappedCustomer;
    }
}