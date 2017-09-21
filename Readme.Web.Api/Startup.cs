using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Readme.DataAccess.EntityFramework.Models;
using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using Readme.DataAccess.EntityFramework;
using Readme.Logic.UnitOfWork.Interface;
using Readme.Logic.UnitOfWork.Implement;
using VersionRouting;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Readme.DataAccess.Dapper.UnitOfWork.Interface;
using Readme.DataAccess.Dapper.UnitOfWork.Implement;
using Readme.DataAccess.MongoDB.UnitOfWork.Interface;
using Readme.DataAccess.MongoDB.UnitOfWork.Implement;
using Microsoft.AspNetCore.SignalR;
using Readme.Web.Api.Hubs;
using Readme.Web.Api.Hubs;

namespace Readme.Web.Api
{
    public class Startup
    {
        /*
         ***************** Initial Client Id & Secret Key *************** 
         */
        private TokenAuthOption tokenOptions;
        private SymmetricSecurityKey _symmetricSecurityKey;
        public string client_id = "7d21b637c25e49908b7f1b2a96d6363d";
        public string secret_key = "2umZKKnWwDtyFOxBW+0JleeiwDSgcsNNTfoCo4IyaYg=";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add JWT auth.
            _symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(secret_key));
            tokenOptions = new TokenAuthOption()
            {
                Audience = client_id,
                Issuer = "http://localhost:50917/",//authport
                SigningCredentials = new SigningCredentials(_symmetricSecurityKey, "HS256"),
                Secret_key = secret_key
            };
            services.AddSingleton<TokenAuthOption>(tokenOptions);
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("ReadmeAuthorize", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = _symmetricSecurityKey,
                    ValidAudience = tokenOptions.Audience,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidateLifetime = true
                };
            });

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Conventions.Add(new NameSpaceVersionRoutingConvention("api"));
            });

            //add swagger interactive documentation
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "Readme API", Version = "v1" });
            });


            //add permission enable cross-origin requests (CORS) from angular
            services.AddCors(options =>
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowCredentials();
                    })
                );
            //>AddSignalR
            services.AddSignalR(options => { });
            //<AddSignalR

            services.AddScoped<ReadmeContext>();
            services.AddScoped<IMongoDBUnitOfWork, MongoDBUnitOfWork>();
            services.AddScoped<IDapperUnitOfWork, DapperUnitOfWork>();
            services.AddScoped<IEntityUnitOfWork, EntityUnitOfWork>();
            services.AddScoped<ILogicUnitOfWork, LogicUnitOfWork>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Readme API V1");
            });

            app.UseCors("AllowAll");

            //>UseSignalR
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseSignalR(config =>
            {
                config.MapHub<NotificationHub>("NotificationHub");
            });

            //UseCors allow origin for test signalR by fontend other Origins
            app.UseCors(config => config.WithOrigins(
                "http://localhost:8100",
                "http://localhost:62454"
                )
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            //<UseSignalR
        }
    }
}
