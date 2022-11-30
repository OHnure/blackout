using System;
using FluentValidation;

namespace Users.Application.Users.Queries.GetUserList
{
    internal class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
