
using dev_example_app.Commands;
using dev_example_app.Models;
using dev_example_app.Repository;
using MediatR;

namespace dev_example_app.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserModel>
    {
        private readonly IUserInterface _userInterface;

        public CreateUserCommandHandler(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return _userInterface.CreateUser(request.User);
        }
    }
}
