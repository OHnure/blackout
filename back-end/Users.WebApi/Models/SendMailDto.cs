using AutoMapper;
using Users.Application.Users.Commands.SendEmail;
using Users.Application.Common.Mapping;

namespace Users.WebApi.Models;

public class SendMailDto : IMapWith<SendMailCommand>
{
    public int Id { get; set; }

    public string Body { get; set; }
    
    public string Subject { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SendMailDto, SendMailCommand>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Body, opt => opt.MapFrom(s => s.Body))
            .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject));
    }
}