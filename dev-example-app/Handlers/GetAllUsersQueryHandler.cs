using dev_example_app.Models;
using dev_example_app.Queries;
using dev_example_app.Repository;
using MediatR;

namespace dev_example_app.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>
    {
        private readonly IUserInterface _userInterface;

        public GetAllUsersQueryHandler(IUserInterface userInterface) => _userInterface = userInterface;

        public Task<IEnumerable<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            Task<IEnumerable<UserModel>> users = _userInterface.GetAllUsers();
            return users;
        }
    }
}
