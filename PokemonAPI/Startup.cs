using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Microsoft.Identity.Web;

//to fix infinite loopin
using Newtonsoft.Json;

using PokemonAPI.Configuration;
using PokemonAPI.Services;
using PokemonAPI.Repositories;
using PokemonAPI.Data;
using PokemonAPI.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PokemonAPI
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
            // configurations
            services.Configure<CsvSettings>(Configuration.GetSection("CsvSettings"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            // db context
            services.AddDbContext<PokemonContext>(); //nog altijd nodig voor migrations

            // caching
            services.AddResponseCaching();

            // controllers
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // versioning
            services.AddApiVersioning(config=> {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            // context
            services.AddTransient<IPokemonContext, PokemonContext>();
            // repositories
            services.AddTransient<ITypingRepository, TypingRepository>();
            services.AddTransient<IPokemonRepository, PokemonRepository>();
            // services
            services.AddTransient<IPokemonService, PokemonService>();

            // mapping
            services.AddAutoMapper(typeof(Startup)); //automapper

            // authorization + authentication - auth0
            /*
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Authority = "https://dev-epycux53.eu.auth0.com/";
                options.Audience = "https://alex.carlier.PokemonAPI";
            });
            */

            // authorization + authentication - PCKE
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAdB2C");

            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "PokemonAPI", 
                    Version = "v1",
                    Description = "An API to search for Pokemon and their Types"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //https://code-maze.com/how-to-prepare-aspnetcore-app-dockerization/
            //https://code-maze.com/swagger-ui-asp-net-core-web-api/
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokemonAPI v1"));
            }*/ //tussen deze comments => enkel in develop mode


            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokemonAPI v1"));

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
