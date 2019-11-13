using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Northwind.Models
{
    public class EditRole
    {
        public IdentityRole Role { get; set; }
        public List<AppUser> UsersInRole { get; set; }
        public List<AppUser> UsersNotInRole { get; set; }

        public EditRole()
        {
            UsersInRole = new List<AppUser>();
            UsersNotInRole = new List<AppUser>();
        }
    }
}