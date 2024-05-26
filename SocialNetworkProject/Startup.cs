using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data;
using SocialNetworkProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetworkProject.Data.Repository;
using SocialNetworkProject.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SocialNetworkProject
{
    public class Startup
    {
        private IConfigurationRoot confString;

        public Startup(Microsoft.Extensions.Hosting.IHostEnvironment hostEnv)
        {
            confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(confString.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAllPlayers, PlayerRepository>();
            services.AddTransient<IAllTeams, TeamRepository>();
            services.AddTransient<IAllRoles, RoleRepository>();
            services.AddTransient<IAllUsers, UserRepository>();
            services.AddScoped<IAllPosts, PostRepository>();
            services.AddScoped<IAllTournaments, TournamentRepository>();
            services.AddScoped<IAllSubscriptions, SubscriptionRepository>();
            services.AddScoped<IAllChat, ChatRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //options.Cookie.HttpOnly = true;
                    //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    //options.Cookie.SameSite = SameSiteMode.Strict;
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/RegistrationLogin/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/RegistrationLogin/Login");
                });


            services.AddMvc();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes => {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Start}");
                routes.MapRoute(name: "teamFilter", template: "Players/List/{Team?}",defaults: new { Controller="Players",action="List"});
                routes.MapRoute(name: "registrationLoginLogout",template: "RegistrationLogin/Logout",defaults: new { controller = "RegistrationLogin", action = "Logout" });
            });


            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }

        }
    }
}
