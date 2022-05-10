using MedaitorR.Data;
using MediatR;

namespace MedaitorR.Commands.Query
{
    public class GetCustomerQuery: IRequest<Customer>
    {
        public int ID{ get; set; }
    }
}
