using dev_example_app.Commands;
using FluentValidation;

namespace dev_example_app.Handlers
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.User.Id).NotNull().NotEmpty();

            RuleFor(user => user.User.Name).NotNull().NotEmpty().MinimumLength(2).OverridePropertyName("Name").WithMessage("Minimum length for Name is 2.");

            RuleFor(user => user.User.Age).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
