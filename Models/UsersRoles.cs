using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Northwind.Models
{
    public class UsersRoles
    {
        public List<IdentityRole> Roles { get; set; }
        public List<AppUser> Users { get; set; }
    }
}