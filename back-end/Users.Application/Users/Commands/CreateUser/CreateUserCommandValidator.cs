using System;
using FluentValidation;

namespace Users.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(createUserCommand =>
            createUserCommand.Name).NotEmpty().MinimumLength(4).MaximumLength(32);
            RuleFor(createUserCommand =>
            createUserCommand.Email).NotEmpty().EmailAddress();
            RuleFor(createUserCommand =>
            createUserCommand.Password).NotEmpty().MinimumLength(4).MaximumLength(32);
            RuleFor(createUserCommand =>
            createUserCommand.City).MaximumLength(32);
            RuleFor(createUserCommand =>
            createUserCommand.Address).MaximumLength(100);
            RuleFor(createUserCommand =>
            createUserCommand.RepeatedPassword).Equal(createUserCommand =>
            createUserCommand.Password);
        }
    }
}
