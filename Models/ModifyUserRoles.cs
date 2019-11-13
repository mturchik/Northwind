using System.Collections.Generic;

namespace Northwind.Models
{
    public class ModifyUserRoles
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public List<string> IdsToAdd { get; set; }
        public List<string> IdsToRemove { get; set; }

        public ModifyUserRoles()
        {
            IdsToAdd = new List<string>();
            IdsToRemove = new List<string>();
        }
    }
}