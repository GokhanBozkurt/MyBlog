using MedaitorR.Commands.Query;
using MedaitorR.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedaitorR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await mediator.Send(new GetCustomerListQuery());
        }

        [HttpGet("{id}",Name ="Get Customer By ID")]
        public async Task<Customer> Get(int id)
        {
            return await mediator.Send(new GetCustomerQuery() { ID=id});
        }
    }
}
