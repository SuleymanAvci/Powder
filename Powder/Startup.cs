using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Powder.Contexts;
using Powder.Entities;
using Powder.Interfaces;
using Powder.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Powder
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PowderContext>();
            services.AddHttpContextAccessor();
            services.AddAuthentication();
            services.AddIdentity<AppUser, IdentityRole>
                (opt =>
                {
                    opt.Password.RequireDigit= false;
                    opt.Password.RequireLowercase= false;
                    opt.Password.RequireUppercase= false;
                    opt.Password.RequiredLength= 1;
                    opt.Password.RequireNonAlphanumeric= false;                    
                }).AddEntityFrameworkStores<PowderContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/LoginIn");
                opt.Cookie.Name = "PowderAsp";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/Home/NotFound","?code={0}");

            //app.UseExceptionHandler("/Error");
            IdentityInitializer.CreateAdmin(userManager, roleManager);
            app.UseRouting();

            //Node Modules disari açma
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules")), RequestPath="/content"
            //});

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{Action=Index}/{id?}");

            });
        }
    }
}
