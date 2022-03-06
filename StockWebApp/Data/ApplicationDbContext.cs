using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockWebApp.Models;

namespace StockWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Seed(builder);
        }

        protected void Seed(ModelBuilder builder)
        {
            var admin_id = Guid.NewGuid().ToString();
            string[] roles = new string[]
            {
                "Administrator",
                "Guest",
                "User",
                "Company",
                "Broker",
                "Dealer"
            };

            foreach (var role in roles)
            {
                if (role.Equals("Administrator")) builder.Entity<Role>().HasData(new Role(id: admin_id, roleName: role));
                else builder.Entity<Role>().HasData(new Role(roleName: role));
            }

            var admin = new User()
            {
                Id = admin_id,
                Email = "admin@stock.app",
                UserName = "Admin",
                EmailConfirmed = true,
            };
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            hasher.HashPassword(admin, "Admin123!");

            builder.Entity<User>().HasData(admin);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = admin_id, RoleId = admin_id });
        }
    }
}