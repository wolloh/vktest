using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Context.Entities;

namespace vktest.Services.Users.Models
{
    public class UserBaseModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Created_Date { get; set; }
    }

    public class UserBaseModelProfile : Profile
    {
        public UserBaseModelProfile() 
        {
            CreateMap<User, UserBaseModel>();
        }
    }
}
