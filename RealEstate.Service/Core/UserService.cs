using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Data.Models;
using RealEstate.Service.Interfaces;

namespace RealEstate.Service.Core
{
	public class UserService : IUserService
	{
		private readonly RealEstateDbContext context;

		public UserService(RealEstateDbContext context)
        {
			this.context = context;
		}
		public void Add(User user)
		{
			context.Add(user);
		}

		public void Remove(User user)
		{
			context.Remove(user);
		}

		public async Task<User> GetUser(int id)
		{
			var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
		}

		public async Task<IEnumerable<User>> GetUsers()
		{
			var users = await context.Users.ToListAsync();

			return users;
		}

		public async Task<bool> SaveAll()
		{
			return await context.SaveChangesAsync() > 0;
		}
	}
}