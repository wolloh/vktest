using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Common.Enums;
using vktest.Context.Entities;

namespace vktest.Services.Users.Models
{
    public class UserExtendedModel
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

    public class UserExtendedModelProfile : Profile
    {
        public UserExtendedModelProfile()
        {
            CreateMap<User, UserExtendedModel>();
        }
    }
}
