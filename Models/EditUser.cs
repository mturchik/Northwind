using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class EditUser
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}