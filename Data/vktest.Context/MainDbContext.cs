using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vktest.Context.Entities;

namespace vktest.Context
{
    public class MainDbContext :  DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<User_Group> UserGroups { get; set; }
        public DbSet<User_State> UserStates { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x=>x.Login).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<User>().HasOne(x => x.Group).WithMany(x => x.Users).HasForeignKey(x => x.GroupId);
            modelBuilder.Entity<User>().HasOne(x => x.State).WithMany(x => x.Users).HasForeignKey(x => x.StateId);

            modelBuilder.Entity<User_Group>().ToTable("user_roles");
            modelBuilder.Entity<User_Group>().Property(x => x.Code).IsRequired();
            modelBuilder.Entity<User_Group>().Property(x => x.Description).IsRequired().HasMaxLength(40);
            



            modelBuilder.Entity<User_State>().ToTable("user_states");
            modelBuilder.Entity<User_State>().Property(x => x.Code).IsRequired();
            modelBuilder.Entity<User_State>().Property(x => x.Description).IsRequired().HasMaxLength(40);

        }
    }
}
