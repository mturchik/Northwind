﻿using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class CreateUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}