using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Data.Models;
using RealEstate.Service.Interfaces;
using RealEstate.Service.ViewModels;

namespace RealEstate.Web.Controllers.ApiControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly IAuthService authService;
		private readonly IConfiguration config;
		private readonly IMapper mapper;

		public AuthController(IAuthService authService, IConfiguration config, IMapper mapper)
		{
			this.mapper = mapper;
			this.authService = authService;
			this.config = config;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(UserRegistration userRegistration)
		{
			userRegistration.Username = userRegistration.Username.ToLower();

			if (await authService.UserExists(userRegistration.Username))
				return BadRequest("Username already exists");

			var userToCreate = mapper.Map<User>(userRegistration);
			var createdUser = await authService.Register(userToCreate, userRegistration.Password);
			var userToReturn = mapper.Map<UserDetail>(userToCreate);

			return StatusCode(201);

		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(UserLogin userLogin)
		{
			var userFromRepo = await authService.Login(userLogin.Username, userLogin.Password);

			if (userFromRepo == null)
				return Unauthorized();

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
				new Claim(ClaimTypes.Name, userFromRepo.Username)
			};

			var key = new SymmetricSecurityKey(Encoding
				.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(securityToken);
			var user = mapper.Map<UserDetail>(userFromRepo);

			return Ok(new { token, user });

			// var claimsIdentity = new ClaimsIdentity(
			//   claims, CookieAuthenticationDefaults.AuthenticationScheme);
			// var authProperties = new AuthenticationProperties();

			// await HttpContext.SignInAsync(
			//   CookieAuthenticationDefaults.AuthenticationScheme,
			//   new ClaimsPrincipal(claimsIdentity),
			//   authProperties);

		}
	}
}