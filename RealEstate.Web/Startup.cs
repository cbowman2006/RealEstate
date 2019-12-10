using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Data;
using RealEstate.Service.Core;
using RealEstate.Service.Interfaces;

namespace RealEstate.Web
{
	public class Startup
	{
		private readonly IHostingEnvironment environment;

		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Configuration = configuration;
			this.environment = environment;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<RealEstateDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("RealEstateConnection")));
				services.AddScoped<IAuthService, AuthService>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
			services.AddCors();
			services.AddAutoMapper(typeof(AuthService).Assembly);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
//			Cookie Authentication
// 			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie("Cookies", options => {
// 				options.Cookie.Name = "auth_cookie";
// 				options.LoginPath = "/login";
// 				options.LogoutPath = "/logout";
// 				options.Cookie.HttpOnly = true;
// 				options.Events = new CookieAuthenticationEvents
// 				{
// 					OnRedirectToLogin = redirectContext =>
// 					{
// 						redirectContext.HttpContext.Response.StatusCode = 401;
// 						return Task.CompletedTask;
// 					}
// 				};
// /* 				options.Cookie.SecurePolicy = environment.IsDevelopment() 
// 					? CookieSecurePolicy.None : CookieSecurePolicy.Always; */

// 			});
// 			services.Configure<CookiePolicyOptions>(options => 
// 			{
// 				options.MinimumSameSitePolicy = SameSiteMode.Strict;
// 				options.HttpOnly = HttpOnlyPolicy.None;
// 				options.Secure = environment.IsDevelopment()
// 					? CookieSecurePolicy.None : CookieSecurePolicy.Always;
// 			});


			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			//app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
