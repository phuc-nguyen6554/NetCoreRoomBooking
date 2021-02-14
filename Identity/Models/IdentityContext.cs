using Identity.Models.Roles;
using Identity.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Data;

namespace Identity.Models
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions options) : base(options) { }

        public DbSet<User> UserData { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Role>().HasData(new Role {Id = 1, RoleName = Constrain.AdminRole, Description = "Admin Role" },
                                           new Role {Id = 2, RoleName = Constrain.EmployeeRole, Description = "Employee Role"});
        }
    }
}
