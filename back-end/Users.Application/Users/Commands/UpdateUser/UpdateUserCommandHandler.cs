using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Users.Application.Interfaces;
using Users.Application.Common.Exceptions;
using Users.Domain;

namespace Users.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUsersDbContext _context;

        public UpdateUserCommandHandler(IUsersDbContext context) =>
            _context = context;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _context.Users.FirstOrDefaultAsync(user =>
                user.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }
            entity.Name = request.Name;
            entity.Address = request.Address;
            entity.Password = request.Password;
            entity.City = request.City;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
