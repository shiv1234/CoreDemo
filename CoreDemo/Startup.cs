using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace CoreDemo
{
    public class Startup
    {
        private IConfiguration _iconfig;

        public Startup(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(_iconfig.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("Home.html");
            //app.UseFileServer(fileServerOptions);

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routs => 
            //{
            //    routs.MapRoute("default", "{controller = Home}/{action = Index}/{Id?}");
            //});
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
