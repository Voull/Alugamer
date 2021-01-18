using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Alugamer.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Alugamer.Auth;
using Alugamer.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Alugamer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddControllersWithViews();

			var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["authKey"]);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
				x.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies["auth"];
						return Task.CompletedTask;
					},
					OnTokenValidated = context =>
                    {
						UserInfo info = JsonConvert.DeserializeObject<UserInfo>(context.Principal.FindFirst(ClaimTypes.UserData).Value);
						context.Response.Cookies.Append("auth", TokenService.GenerateToken(info));
						return Task.CompletedTask;
					}
					
				}; 
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == StatusCodes.Status404NotFound && context.Response.ContentLength == null)
                {
                    context.Request.Path = "/404";
                    await next();
                }
				else if(context.Response.StatusCode == (int)StatusCodes.Status401Unauthorized)
                {
					context.Request.Path = "/Login";
					await next();
                }
            });

            app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});

#if (TRAVIS || TESTE)
			Initialization();
#endif

		}

#if (TRAVIS || TESTE)
		protected void Initialization()
		{
			TesteDAO testeDAO = new TesteDAO();
			testeDAO.InicializaBDTeste();
		}
#endif
	}
}

