using Microsoft.AspNetCore.Identity;

namespace StockWebApp.Models
{
    public class Role : IdentityRole
    {
        public Role(string roleName)
        {
            Name = roleName;
        }

        public Role(string id, string roleName)
        {
            Id = id;
            Name = roleName;
        }
    }
}
