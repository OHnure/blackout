using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class GetCutUserDetailsQuery : IRequest<int>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
