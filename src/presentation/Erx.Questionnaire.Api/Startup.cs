using Application;
using Common.Constants;
using Erx.Questionnaire.Api.Filter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erx.Questionnaire.Api
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
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddCors();
            services.AddControllers();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, BasicKeyAuthorizeHandler>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = "https://localhost:5001/";
                options.Authority = "https://localhost:5000/";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AuthorizePolicy.CLIENT_KEY,
                                  policy =>
                                  {
                                      policy.Requirements.Add(new BasicKeyRequirement(Configuration["AuthKey:ClientKey"]));
                                  });

                options.AddPolicy(Constants.AuthorizePolicy.ADMIN_KEY,
                                  policy =>
                                  {
                                      policy.Requirements.Add(new BasicKeyRequirement(Configuration["AuthKey:AdminKey"]));
                                  });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("admin", new OpenApiInfo { Title = "Erx Questionnaire Admin Api v1", Version = "v1" });
                c.SwaggerDoc("client", new OpenApiInfo { Title = "Erx Questionnaire Client Api v1", Version = "v1" });
                c.IgnoreObsoleteActions();
                c.AddSecurityDefinition("Auth",
                                        new OpenApiSecurityScheme
                                        {
                                            In = ParameterLocation.Header,
                                            Description = "Please insert key/jwt with Bearer/Key into field",
                                            Name = "Authorization",
                                            Type = SecuritySchemeType.ApiKey
                                        });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Auth" }
                        },
                        new string[]
                        {
                        }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("admin/swagger.json", "Erx Questionnaire Admin Api v1");
                    c.SwaggerEndpoint("client/swagger.json", "Erx Questionnaire Client Api v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplyDatabaseMigration();
        }
    }
}
