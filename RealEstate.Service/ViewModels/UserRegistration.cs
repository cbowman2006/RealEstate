using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Service.ViewModels
{
    public class UserRegistration
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = " You must specify a password between 4 and 8 characters")]
        public string Password { get; set; }
        public DateTime Created { get; set; } 
        public UserRegistration()
        {
            Created = DateTime.Now;       
        }
        
    }
}