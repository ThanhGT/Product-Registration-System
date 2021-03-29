using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TT_LAB2.Models;

namespace TT_LAB2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //creates a new instance of the implementation type for the first request only, which then reuses it for every
            //subsequent request
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            //sets up the shared objects required by the application using the MVC framework and razor view engine
            services.AddControllersWithViews();
            //connect to database and check the product/category object in the database to see if there is any data
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "ProductAPI",
                    Description = "API calls for Product",

                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Thanh Tran",
                        Email = "tran67@sheridancollege.ca"
                    }
                });
            });

            services.AddDbContext<MBSConnStrDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:MBSConnStr"]);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //extension method enables support for serving static content from the wwwroot folder
                app.UseStaticFiles();
                //extension method adds a simple message to HTTP response that would not otherwise have a body such as
                //404-not found responses
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
                });
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    //used to register the MVC framework as a source of endpoints
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapControllerRoute("pagination",
                                                 "Products/Page{prodPage:int}",
                                                 new { Controller = "Home", action = "PageSummary" });
                });
            }
        }
    }
}
