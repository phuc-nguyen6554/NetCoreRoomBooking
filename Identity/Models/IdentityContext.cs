using Identity.Models.Roles;
using Identity.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            builder.Entity<Role>().HasData(new Role {Id = 1, RoleName = "Admin", Description = "Admin Role" },
                                           new Role {Id = 2, RoleName = "Employee", Description = "Employee Role"});
        }
    }
}
