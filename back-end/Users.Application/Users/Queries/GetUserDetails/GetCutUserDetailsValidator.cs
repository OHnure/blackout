using System;
using FluentValidation;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class GetCutUserDetailsValidator : AbstractValidator<GetCutUserDetailsQuery>
    {
        public GetCutUserDetailsValidator()
        {
            RuleFor(getUserDetailsQuery =>
            getUserDetailsQuery.Email).EmailAddress();
            RuleFor(getUserDetailsQuery =>
            getUserDetailsQuery.Password).NotEmpty().MinimumLength(4).MaximumLength(32);
        }
    }
}
