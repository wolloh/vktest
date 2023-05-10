using AutoMapper;
using vktest.Common.Exceptions;
using vktest.Common.Validator;
using vktest.Services.Users.Models;
using vktest.Context;
using vktest.Context.Entities;
using Microsoft.EntityFrameworkCore;
using vktest.Common.Enums;

namespace vktest.Services.Movies
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddUserModel> addUserModelValidator;
        private SemaphoreSlim semaphore = new SemaphoreSlim(initialCount: 1);

        public UserService(
            IDbContextFactory<MainDbContext> contextFactory,
            IMapper mapper,
            IModelValidator<AddUserModel> addUserModelValidator)
        {
            this.mapper = mapper;
            this.contextFactory= contextFactory;
            this.addUserModelValidator= addUserModelValidator;
        }
        public  async Task<User> AddUser(AddUserModel model)
        {
            addUserModelValidator.Check(model);
            using var context = await contextFactory.CreateDbContextAsync();
            var user = mapper.Map<User>(model);
            await semaphore.WaitAsync();
            var userLogin= await context
                            .Users
                            .SingleOrDefaultAsync(x=>x.Login==user.Login);

            ProcessException.ThrowIf(
                () => userLogin is not null, 
                $"The user with login:{user.Login} already existing"
            );

            var adminCount = await context
                    .Users
                    .CountAsync(x => x.Group.Code == UserType.Admin);

            ProcessException.ThrowIf(
                () => model.isAdmin && adminCount == 1,
                "You cant create user with such group code"
            );
            Thread.Sleep(5000);
            user.GroupId = model.isAdmin ? (await context.UserGroups.FirstOrDefaultAsync(x => x.Code == UserType.Admin))!.Id
                    : (await context.UserGroups.FirstOrDefaultAsync(x => x.Code == UserType.User))!.Id;

            user.StateId = (await context
                    .UserStates
                    .FirstOrDefaultAsync(x => x.Code == UserState.Active))!.Id;

            user.Created_Date= DateTime.Now;

            await context.Users.AddAsync(user);
            context.SaveChanges();
            semaphore.Release();
            return user;

        }

        public async Task DeleteUser(int userId)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId))
                ?? throw new ProcessException($"The user (id: {userId}) was not found");
            user.State = await context
                    .UserStates
                    .FirstOrDefaultAsync(x => x.Code == UserState.Blocked);
            context.Users.Update(user);
            context.SaveChanges();
        }

        public async Task<User> GetUser(int userId)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var user = await context.Users
                .Include(x => x.State)
                .Include(x => x.Group)
                .FirstOrDefaultAsync(x => x.Id.Equals(userId));

            ProcessException.ThrowIf(
                () => user is null,
                $"The user with id:{userId} doesnt exist"
            );
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(int offset,int limit)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var users = context
                .Users
                .Include(x=>x.State) 
                .Include(x=>x.Group)
                .AsQueryable();

            users=users
                   .Skip(Math.Max(offset, 0))
                   .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await users.ToListAsync());
            return data;
        }
    }
}
