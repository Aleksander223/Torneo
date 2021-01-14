using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;

namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [UsernameValidation(ErrorMessage="Make sure username is at least 3 characters long and do not use _")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [MinLength(3)]
        public string Bio { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }

    
 
}
