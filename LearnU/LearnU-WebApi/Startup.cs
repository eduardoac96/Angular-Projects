using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearnU_WebApi.Models;
using LearnU_WebApi.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static LearnU_WebApi.Authorization.MustBeAntiqueUser;

namespace LearnU_WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = _configuration["connectionStrings:LearnUConnectionString"];

            services.AddDbContext<LearnUContext>(o => o.UseSqlServer(dbConnectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            // Configuring automapper to easily convert between entities and resources and the other way around
            services.AddAutoMapper();

            // Configuring the JWT Authentication
            var signingKey = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var repository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var userId = Guid.Parse(context.Principal.Identity.Name); // When logging in we're storing the user id into the Name property
                        var user = repository.GetUser(userId).Result;
                        if (user == null)
                        {
                            // If user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // These 2 settings below are necessary so token expiration is checked when a token is validated
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(Constants.PolicyNames.MustBeAdministrator), policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireClaim(ClaimTypes.Role, nameof(Constants.RoleClaimValues.Administrator));
                });
                options.AddPolicy(nameof(Constants.PolicyNames.MustBeAntiqueUser), policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new MustBeAntiqueUserRequirement(Constants.Constants.SECONDS_AFTER_CREATED_DATE_FOR_ANTIQUE));
                });
            });

            // Configuring the information necessary to create instances
            services.AddScoped<IAuthorizationHandler, MustBeAntiqueUserHandler>();// Registering our handler on our container so it can create instances when needed
            services.AddScoped<IUserRepository, UserServices>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
