using System;
using AutoMapper;
using Users.Application.Common.Mapping;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserList
{
    public class UserLookupDto : IMapWith<User>
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? City { get; set; }

        public string? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
                .ForMember(noteVm => noteVm.Name,
                opt => opt.MapFrom(note => note.Name))
                .ForMember(noteVm => noteVm.Email,
                opt => opt.MapFrom(note => note.Email))
                .ForMember(noteVm => noteVm.Password,
                opt => opt.MapFrom(note => note.Password))
                .ForMember(noteVm => noteVm.City,
                opt => opt.MapFrom(note => note.City))
                .ForMember(noteVm => noteVm.Name,
                opt => opt.MapFrom(note => note.Name));
        }
    }
}
