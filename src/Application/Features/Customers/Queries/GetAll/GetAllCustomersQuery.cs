using AutoMapper;
using CleanArchitectureDemo.Application.Repositories;
using CleanArchitectureDemo.Domain.Entities;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Customers.Queries.GetAll;

public class GetAllCustomersQuery : IRequest<IEnumerable<GetAllCustomersResponse>>
{

}

internal class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<Guid> _unitOfWork;

    public GetAllCustomersQueryHandler(IMapper mapper, IUnitOfWork<Guid> unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GetAllCustomersResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerData = await _unitOfWork.Repository<Customer>().GetAllAsync(cancellationToken);
        var mappedCustomers = _mapper.Map<List<GetAllCustomersResponse>>(customerData);
        return mappedCustomers;
    }
}