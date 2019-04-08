using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.UI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Paint.ActionFilters;
using Paint.Business;
using Paint.Contracts;

namespace Paint
{

    public class Startup
    {
        public static CultureInfo[] supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fa-IR")
            };
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {

                // BEGIN02
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
                // END02


            });

            services.AddScoped<IRegisterationBusiness, RegisterationBusiness>();
            services.AddHttpClient();

    

            services.AddMvc(options =>
            {
                options.Filters.Add(new DefaultCultureActionFilter());
            });
            services.AddSession();



            //Mohsen*****************************

        
           // var connection = @"Server=(localdb)\mssqllocaldb;Database=EvaluationDB.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<EvaluationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalDBConection")));

            services.AddScoped<IMyDependency, MyDependency>();

            //


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

          // app.Run(async (context) => { });
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("fa-IR")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = {
                    new CookieRequestCultureProvider()
                }
            });
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            ///Mohsen***********************
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EvaluationContext>())
                {
                    context.Database.Migrate();
                }
            }
            ///


        }

    }
    public interface IMyDependency
    {
        Task WriteMessage(string message);
    }
    public class MyDependency : IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        public MyDependency(ILogger<MyDependency> logger)
        {
            _logger = logger;
        }

        public Task WriteMessage(string message)
        {
            _logger.LogInformation(
                "MyDependency.WriteMessage called. Message: {MESSAGE}",
                message);

            return Task.FromResult(0);
        }
    }
}
