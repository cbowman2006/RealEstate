using System;

namespace RealEstate.Service.ViewModels
{
    public class UserDetail
    { 
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        
    }
}