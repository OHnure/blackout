using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Common.Mapping;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserDetails
{
    public class UserDetailsVm : IMapWith<User>
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? City { get; set; }

        public string? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>()
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
