using System;

namespace RealEstate.Service.ViewModels
{
	public class UserDetail
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get { return FirstName + " " + LastName; } }
		public string Username { get; set; }
		public string Email { get; set; }
		public DateTime Created { get; set; }

	}
}