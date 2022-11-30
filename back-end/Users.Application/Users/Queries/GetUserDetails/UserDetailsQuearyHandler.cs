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
    public class UserDetailsQuearyHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserDetailsQuearyHandler(IUsersDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users.
                FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            return _mapper.Map<UserDetailsVm>(entity);
        }
    }
}
