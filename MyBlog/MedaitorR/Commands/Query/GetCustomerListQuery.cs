using MedaitorR.Data;
using MediatR;

namespace MedaitorR.Commands.Query
{
    public class GetCustomerListQuery:IRequest<List<Customer>>
    {

    }
}
