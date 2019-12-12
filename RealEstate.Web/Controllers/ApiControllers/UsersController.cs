using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Data.Models;
using RealEstate.Service.Interfaces;
using RealEstate.Service.ViewModels;

namespace RealEstate.Web.Controllers.ApiControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : Controller
	{
		private readonly IUserService userService;
		private readonly IMapper mapper;

		public UsersController(IUserService userService, IMapper mapper)
		{
			this.userService = userService;
			this.mapper = mapper;
		}

        [HttpGet]
        public async Task<IActionResult> Getuser()
        {
            var users = await userService.GetUsers();

            var usersToReturn = mapper.Map<IEnumerable<UserDetail>>(users);

            return Ok(usersToReturn);
        }


		[HttpGet("{id}", Name = "GetUser")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await userService.GetUser(id);
            if (user == null)
                return NotFound();

			var userToReturn = mapper.Map<UserDetail>(user);

			return Ok(userToReturn);
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, UserUpdate userUpdate)
        {
            var userFromRepo = await userService.GetUser(id);

            mapper.Map(userUpdate, userFromRepo);

            if(await userService.SaveAll())
                return NoContent();
                
            throw new Exception($"Updating user {id} failed on save");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userService.GetUser(id);

            if(user == null)
                return NotFound();

            userService.Remove(user);

            await userService.SaveAll();

            return Ok(id);

        }


	}
}