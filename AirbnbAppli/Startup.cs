using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirbnbAppli.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AirbnbAppli
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
            services.AddControllersWithViews();
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("mydb")));
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "logements",
                    pattern: "{controller=Logements}/{action=Create}"
                 );
                endpoints.MapControllerRoute(
                   name: "logements",
                   pattern: "librairie/livres/{id}",
                   defaults: new { controller = "Logements", action = "Details" }
                );
                endpoints.MapControllerRoute(
                   name: "utilisateurs",
                   pattern: "{controller=Utilisateurs}/{action=Create}"
                );
            });

            // ne pas oublier pour se connecter à la BDD
            using (var scope = app.ApplicationServices.CreateScope())
            {
                Context ctx = scope.ServiceProvider.GetService<Context>();
                InitialiseDb(ctx);
            }

        }

        public void InitialiseDb(Context ctx)
        {
            ctx.Database.EnsureCreated();
            ctx.SaveChanges();
        }
    }
}
