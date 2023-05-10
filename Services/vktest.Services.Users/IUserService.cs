using vktest.Services.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Context.Entities;

namespace vktest.Services.Movies
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<User> AddUser(AddUserModel model);
        Task DeleteUser(int userId);
    }
}
