using System;
using FluentValidation;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsValidator : AbstractValidator<GetUserDetailsQuery>
    {
        public GetUserDetailsValidator()
        {
            RuleFor(getUserDetailsQuery =>
            getUserDetailsQuery.Id).NotEmpty();
        }
    }
}
