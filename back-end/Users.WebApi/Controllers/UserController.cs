using System.Net;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Queries.GetUserList;
using Users.Application.Users.Queries.GetUserDetails;
using Utils;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Commands.UpdateUser;
using AutoMapper;
using System.Net.Mail;
using Users.Application.Users.Commands.SendEmail;
using Users.WebApi.Models;

namespace Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserLoginController : BaseController
    {
        private readonly IMapper _mapper;

        public UserLoginController(IMapper mapper) => _mapper = mapper;
        [HttpPost("getAllUsers")]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetUserListQuery();
            var vm = await Mediator.Send(query);            
            return Ok(vm);
        }

        [HttpPost("{mail}/{password}")]
        public async Task<ActionResult<UserDetailsVm>> Get(string mail, string password)
        {
            var query = new GetCutUserDetailsQuery
            {
                Email = mail,
                Password = password
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<UserDetailsVm>> Get(int id)
        {
            var query = new GetFullUserDetailsQuery
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

        [HttpPut]
        public async Task<ActionResult<UserDetailsVm>> Update([FromBody] UpdateUserDto updateUserDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPost("sendMail")]
        public async Task<ActionResult> SendEmail([FromBody] SendMailDto sendMailDto)
        {
            var command = _mapper.Map<SendMailCommand>(sendMailDto);
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

    }    
}
