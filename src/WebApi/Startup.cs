﻿using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeManager.ApplicationCore.Helpers;
using RecipeManager.ApplicationCore.Models;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.Infrastructure.Data;
using RecipeManager.Infrastructure.Entities;
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
            services.Configure<AdminOptions>(Configuration.GetSection("AdminOptions"));

            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIngredientService, IngredientService>();

            services.AddInfrastructure(Configuration);
            
            services.AddIdentityCore<ApplicationUser>()
                .AddSignInManager()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme, options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync,
                    };
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
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

            services.AddAutoMapper(
                cfg =>
                {
                    cfg.AddExpressionMapping();
                    cfg.AddProfile<MappingProfile>();
                },
                Array.Empty<Assembly>());

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context, IOptionsMonitor<DbOptions> options, IUserService userService)
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

            // Without the using statement, Azure Pipelines might get a deadlock
            using (NoSynchronizationContextScope.Enter())
            {
                // TODO: This system can be used to set an admin password in production too (as long as that is managed securely)
                userService.SetPasswordAsync(FakeDataSeeder.FakeAdminName, "Abcd123#").Wait();
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

            //app.UseHttpsRedirection();
            
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
