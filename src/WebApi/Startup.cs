using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.Infrastructure.Data;
using RecipeManager.WebApi.Errors;
using RecipeManager.WebApi.Filters;
using RecipeManager.WebApi.Interfaces;
using RecipeManager.WebApi.Services;

namespace RecipeManager.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PagingOptions>(this.Configuration.GetSection("DefaultPagingOptions"));

            services.AddScoped<IRecipeService, RecipeService>();

            services.AddDbContext<RecipeApiDbContext>(options =>
            {
                // TODO: Swap out for a real database in production
                options.UseInMemoryDatabase("recipesdb");
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<LinkRewritingFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiError(context.ModelState);
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowMyApp",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            this.CurrentEnvironment = env;

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

            app.UseHttpsRedirection();
            
            app.UseCors("AllowMyApp");
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseMvc();
        }
    }
}
