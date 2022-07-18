using DomainValidation.Api.Controllers.Abstract;
using DomainValidation.Application.Commands;
using DomainValidation.Application.Queries;
using DomainValidation.Application.ReadModels;
using DomainValidation.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainValidation.Api.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerReadModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerReadModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var customers = await _mediator.Send(new GetCustomersQuery());
            return Ok(customers);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedReadModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateCustomerRequest request)
        {
            var result = await _mediator.Send(new CreateCustomerCommand(request.FirstName, request.LastName, request.Email));
            return CreatedResult(nameof(Get), result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return NoContentResult(result);
        }

        [HttpPut("{id}/address")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressRequest request)
        {
            var result = await _mediator.Send(new UpdateAddressCommand(id, request.Street, request.City, request.State, request.ZipCode));
            return NoContentResult(result);
        }
    }
}