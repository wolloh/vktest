using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vktest.Api.Controllers.Models;
using vktest.Api.Controllers.Users.Models;
using vktest.Common.Responses;
using vktest.Services.Movies;
using vktest.Services.Users.Models;

namespace vktest.Api.Controllers.Users
{
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [Produces("application/json")]
    [Route("api/users")]
    [ApiController]
    public class UserController:ControllerBase
    {

        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;

        public UserController(IMapper mapper, ILogger<UserController> logger, IUserService userService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.userService = userService;

        }

        [ProducesResponseType(typeof(UserResponse), 200)]
        [HttpGet("")]
        public async Task<IEnumerable<UserResponse>> GetUsers()
        {
            var users = await userService.GetUsers();
            var response = mapper.Map<IEnumerable<UserResponse>>(users);

            return response;
        }

        [ProducesResponseType(typeof(UserResponse), 200)]
        [HttpGet("{id}")]
        public async Task<UserResponse> GetUserById([FromRoute] int id)
        {
            var user = await userService.GetUser(id);
            var response = mapper.Map<UserResponse>(user);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await userService.DeleteUser(id);
            return Ok();
        }


        [HttpPost("")]
        public async Task<UserResponse> AddUser([FromBody] AddUserRequest request)
        {
            var model = mapper.Map<AddUserModel>(request);
            var user = await userService.AddUser(model);
            var response = mapper.Map<UserResponse>(user);

            return response;
        }
    }
}
