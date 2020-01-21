using IdentityExample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(cfg =>
            {
                cfg.UseInMemoryDatabase("MemoryDb");
            });

            // Identity registration and hook up to EF
            services.AddIdentity<IdentityUser, IdentityRole>(cfg =>
            {
                cfg.Password.RequiredLength = 4;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Cookie.Name = "Naumans.Identity.Cookie";
                cfg.LoginPath = "/Home/Login";
            });

            //services.AddAuthentication("Cookie.Auth").AddCookie("Cookie.Auth", cfg => {
            //    cfg.Cookie.Name = "Naumans.Cookie";
            //    cfg.LoginPath = "/Home/Authenticate";
            //});

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting().UseAuthentication().UseAuthorization().UseEndpoints(e => e.MapDefaultControllerRoute());
        }
    }
}
