using System;
using MediatR;

namespace Users.Application.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserListVm>
    {
        public int Id { get; set; }
    }
}
