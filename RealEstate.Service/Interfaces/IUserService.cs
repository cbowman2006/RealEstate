using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstate.Data.Models;

namespace RealEstate.Service.Interfaces
{
	public interface IUserService
	{
		void Add(User user);
		void Remove(User user);
		Task<bool> SaveAll();
        Task<User> GetUser(int id);
		Task<IEnumerable<User>> GetUsers();

	}
}