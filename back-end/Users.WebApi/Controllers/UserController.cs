using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Queries.GetUserList;
using Users.Application.Users.Queries.GetUserDetails;
using Utils;
using Users.Application.Users.Commands.CreateUser;
using AutoMapper;

namespace Users.WebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper) => _mapper = mapper;
        [HttpGet]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetUserListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsVm>> Get(int id)
        {
            var query = new GetUserDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<UserDetailsVm>> Create([FromBody]CreateUserCommand createUserDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createUserDto);
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }
    }
}
