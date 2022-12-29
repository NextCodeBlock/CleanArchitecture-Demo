using CleanArchitectureDemo.Application.Features.Customers.Commands.AddUpdate;
using CleanArchitectureDemo.Application.Features.Customers.Queries.GetAll;
using CleanArchitectureDemo.Application.Features.Customers.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseApiController<CustomerController>
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all customer list");
            var customers = await _mediator.Send(new GetAllCustomersQuery(), HttpContext.RequestAborted);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Get customer by id: {id}", id);
            var customer = await _mediator.Send(new GetByIdCustomerQuery {CustomerId = id}, HttpContext.RequestAborted);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUpdateCustomerCommand command)
        {
            _logger.LogInformation("Add and update customer");
            return Ok(await _mediator.Send(command, HttpContext.RequestAborted));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Delete customer by id: {id}", id);
            return Ok(await _mediator.Send(new DeleteCustomerCommand() { CustomerId = id }, HttpContext.RequestAborted));
        }
    }
}