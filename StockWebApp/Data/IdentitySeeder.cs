using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StockWebApp.Data
{
    public class IdentitySeeder
    {
        private readonly ApplicationDbContext _context;

        public IdentitySeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void Initialize()
        {
            string[] roles = new string[]
            {
                "Administrator",
                "Guest",
                "User",
                "Company",
                "Broker",
                "Dealer"
            };
            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(_context);
                if (!_context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role));
                }
            }
            if (!_context.Users.Any(u => u.Email == "admin@stock.app"))
            {
                var userStore = new UserStore<IdentityUser>(_context);
                var user = new IdentityUser
                {
                    Email = "admin@stock.app",
                    NormalizedEmail = "admin@stock.app",
                    UserName = "Admin",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "Administrator");
            }
            await _context.SaveChangesAsync();
        }
    }
}
