using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Extensions;
using RecipeManager.Infrastructure.Helpers;
using RecipeManager.Infrastructure.Seeding;
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
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment? CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));

            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIngredientService, IngredientService>();

            services.AddInfrastructure(Configuration);
            
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"])),
                    };
                });

            services.AddControllers(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<LinkRewritingFilter>();
            });

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context, IOptionsMonitor<DbOptions> options)
        {
            if (options.CurrentValue.Delete)
            {
                context.Database.EnsureDeleted();
            }

            if (options.CurrentValue.Create)
            {
                context.Database.EnsureCreated();
            }

            if (options.CurrentValue.Migrate)
            {
                context.Database.Migrate();
            }
            
            CurrentEnvironment = env;

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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
