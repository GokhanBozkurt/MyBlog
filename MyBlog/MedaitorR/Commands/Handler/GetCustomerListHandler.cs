using MedaitorR.Commands.Query;
using MedaitorR.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedaitorR.Commands.Handler
{
    public class GetCustomerListHandler : IRequestHandler<GetCustomerListQuery, List<Customer>>
    {
        private readonly MedaitorDbContext context;

        public GetCustomerListHandler(MedaitorDbContext context) => this.context = context;
        public  Task<List<Customer>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Customers.ToList());
        }
    }
}
