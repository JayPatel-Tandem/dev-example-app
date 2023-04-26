using dev_example_app.Models;
using MediatR;

namespace dev_example_app.Queries
{
    public record GetAllUsersQuery():IRequest<IEnumerable<UserModel>>
    {

    }
}
