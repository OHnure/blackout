using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Users.Domain;
using Users.Application.Interfaces;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Queries.GetUserDetails;
using Microsoft.EntityFrameworkCore;
using Users.Application.Users.Queries.GetUserList;
using System.Net.Http;
using System.Net.Mail;

namespace Users.Application.Users.Commands.SendEmail;

public class SendMailCommandHandler : IRequestHandler<SendMailCommand, HttpStatusCode>
{
    private readonly IUsersDbContext _context;
    public SendMailCommandHandler(IUsersDbContext context) => _context = context;

    public async Task<HttpStatusCode> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        var fromUser = _context.Users.FirstOrDefault(x => x.Id == request.Id);
        var usersList = _context.Users.Where(x => x.Id != request.Id).ToList();
        
        if(fromUser == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        var client = new SmtpClient();

        client.Host = "smtp-relay.sendinblue.com";
        client.Port = 587;
        client.Credentials = new NetworkCredential("mykhailo.sovietskyi@nure.ua", "kqtzOmy3KI7QdaRA");

        foreach (var user in usersList)
        {
            var message = new MailMessage
            {
                To = { new MailAddress(user.Email) },
                Subject = request.Subject,
                Body = request.Body,
                From = new MailAddress(fromUser.Email)
            };
            await client.SendMailAsync(message);
        }
        // Send the email message

        return HttpStatusCode.OK;
    }
}