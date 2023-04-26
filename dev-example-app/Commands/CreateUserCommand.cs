using dev_example_app.Models;
using MediatR;

namespace dev_example_app.Commands
{
    public record CreateUserCommand : IRequest<UserModel>
    {
        public UserModel User { get; set; }
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int Age { get; set; }

        //public CreateUserCommand(UserModel User)
        //{
        //    Id = User.Id;

        //    Name = User.Name;

        //    Age = User.Age;
        //}

    }
}
