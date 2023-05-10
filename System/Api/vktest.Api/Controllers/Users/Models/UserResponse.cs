using AutoMapper;
using System.Diagnostics.Metrics;
using vktest.Common.Enums;
using vktest.Context.Entities;

namespace vktest.Api.Controllers.Users.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Created_Date { get; set; }

        public UserType UserType { get; set; }
        public string TypeDescription { get; set; }

        public UserState UserState { get; set; }
        public string StateDescription { get; set; }

    }

    public class UserResponseProfile : Profile
    {
        public UserResponseProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(d=>d.UserType,a=>a.MapFrom(s=>s.Group.Code))
                .ForMember(d => d.TypeDescription, a => a.MapFrom(s => s.Group.Description))
                .ForMember(d => d.UserState, a => a.MapFrom(s => s.State.Code));
        }
    }
}
