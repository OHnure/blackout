using AutoMapper;
using Users.Application.Common.Mapping;
using Users.Application.Users.Commands.UpdateUser;

namespace Users.WebApi.Models
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? City { get; set; }

        public string? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ForMember(noteVm => noteVm.Name,
                opt => opt.MapFrom(note => note.Name))
                .ForMember(noteVm => noteVm.Password,
                opt => opt.MapFrom(note => note.Password))
                .ForMember(noteVm => noteVm.City,
                opt => opt.MapFrom(note => note.City))
                .ForMember(noteVm => noteVm.Address,
                opt => opt.MapFrom(note => note.Address));
        }
    }
}
