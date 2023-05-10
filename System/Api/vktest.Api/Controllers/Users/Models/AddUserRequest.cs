using AutoMapper;
using vktest.Services.Users.Models;
using FluentValidation;

namespace vktest.Api.Controllers.Models
{
    public class AddUserRequest
    {
        public string Login{ get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }=false;
    }

    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("User name is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Email is required.");
        }
    }

    public class AddUserAccountRequestProfile : Profile
    {
        public AddUserAccountRequestProfile()
        {
            CreateMap<AddUserRequest, AddUserModel>();
        }
    }
}
