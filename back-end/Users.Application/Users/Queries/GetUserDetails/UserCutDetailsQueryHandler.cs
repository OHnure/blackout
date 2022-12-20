using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Common.Exceptions;
using Users.Domain;
using Users.Application.Interfaces;
using MediatR;
using AutoMapper;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class UserCutDetailsQueryHandler : IRequestHandler<GetCutUserDetailsQuery, int>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserCutDetailsQueryHandler(IUsersDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<int> Handle(GetCutUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.
                FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Email);
            }
            if (entity.Password != request.Password)
            {
                throw new InvalidPasswordException();
            }
            return entity.Id;
        }
    }
}
