using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Data.Models;
using RealEstate.Service.Interfaces;


namespace RealEstate.Service.Core
{
	public class AuthService : IAuthService
	{
		private readonly RealEstateDbContext context;
		public AuthService(RealEstateDbContext context)
		{
			this.context = context;

		}

		public async Task<User> Login(string username, string password)
		{
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if(user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user;
			
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
		}

		public async Task<User> Register(User user, string password)
		{
			byte[] paswoordHash, passwordSalt;
            CreatePasswordHash(password, out paswoordHash, out passwordSalt);

            user.PasswordHash = paswoordHash;
            user.PasswordSalt = passwordSalt;

            await context.Users.AddAsync(user);

            await context.SaveChangesAsync();

            return user;
            
		}

		private void CreatePasswordHash(string password, out byte[] paswoordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                paswoordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
		}

		public async Task<bool> UserExists(string username)
		{
			if(await context.Users.AnyAsync(u => u.Username == username))
                return true;

            return false;
		}
	}
}