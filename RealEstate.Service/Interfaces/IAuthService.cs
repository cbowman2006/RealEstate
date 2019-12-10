using System.Threading.Tasks;
using RealEstate.Data.Models;

namespace RealEstate.Service.Interfaces
{
    public interface IAuthService
    {
         Task<User> Register (User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username); 
    }
}