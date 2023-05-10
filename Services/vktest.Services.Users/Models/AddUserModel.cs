using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Context.Entities;

namespace vktest.Services.Users.Models
{
    public class AddUserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }=false;
    }

    public class AddUserModelValidator : AbstractValidator<AddUserModel>
    {
        public AddUserModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Login is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }

    public class AddUserModelProfile : Profile
    {
        public AddUserModelProfile()
        {
            CreateMap<AddUserModel, User>();
        }
    }
}
