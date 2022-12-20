using System.Net;
using MediatR;
using Org.BouncyCastle.Crypto.Engines;

namespace Users.Application.Users.Commands.SendEmail;

public class SendMailCommand : IRequest<HttpStatusCode>
{
    public int Id { get; set; }

    public string Body { get; set; }
    
    public string Subject { get; set; }
}