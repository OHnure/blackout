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
    public class UserFullDetailsQueryHandler : IRequestHandler<GetFullUserDetailsQuery, UserDetailsVm>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserFullDetailsQueryHandler(IUsersDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<UserDetailsVm> Handle(GetFullUserDetailsQuery request, CancellationToken cancellationToken)
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
