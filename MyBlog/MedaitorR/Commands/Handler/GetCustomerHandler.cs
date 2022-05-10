using MedaitorR.Commands.Query;
using MedaitorR.Data;
using MediatR;

namespace MedaitorR.Commands.Handler
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly MedaitorDbContext? context;

        public GetCustomerHandler(MedaitorDbContext context) => this.context = context;
        public Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Customers.FirstOrDefault(a => a.ID == request.ID));
        }
    }
}
