using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendingMessageToUsers.Data;
using SendingMessageToUsers.Entities;
using SendingMessageToUsers.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SendingMessageToUsers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;

            });

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseInMemoryDatabase("MEMORY"))
                .AddIdentity<ApplicationUser, ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddFacebook(config =>
            {
                config.AppId = Configuration["Authentication:Facebook:AppId"];
                config.AppSecret = Configuration["Authentication:Facebook:AppSecret"];

            })
            .AddVkontakte(config =>
            {
                config.ClientId = Configuration["Authentication:Vkontakte:ClientId"];
                config.ClientSecret = Configuration["Authentication:Vkontakte:ClientSecret"];
            })
            .AddGoogle(config =>
            {
                config.ClientId = Configuration["Authentication:Google:ClientId"];
                config.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });
        
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/Login";
                config.AccessDeniedPath = "/Home/AccessDenied";
            });

            services.AddControllersWithViews(options => { options.SuppressAsyncSuffixInActionNames = false; });
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

 

    app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
