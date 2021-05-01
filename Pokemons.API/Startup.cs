using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.OpenApi.Models;
using Microsoft.Identity.Web;

using Newtonsoft.Json; //to fix infinite looping during SELECT

using Pokemons.API.Configuration;
using Pokemons.API.Services;
using Pokemons.API.Repositories;
using Pokemons.API.Data;
using Pokemons.API.Helpers;

namespace Pokemons.API
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
            services.AddDbContext<PokemonContext>();

            // caching
            services.AddResponseCaching();

            // controllers
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // versioning
            services.AddApiVersioning(config=> {
                config.DefaultApiVersion = new ApiVersion(2, 0);
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
            // extra
            services.AddTransient<IConvertHelper, ConvertHelper>();

            // automapper
            services.AddAutoMapper(typeof(Startup));

            // authorization + authentication - auth0
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Authority = "https://dev-epycux53.eu.auth0.com/";
                options.Audience = "https://alex.carlier.PokemonAPI";
            });
            

            // swagger
            services.AddSwaggerGen(c =>
            {
                /*c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Pokemons API v1", 
                    Version = "v1",
                    Description = "An API to search for Pokemon and their Types"
                    });*/

                c.SwaggerDoc("v2", new OpenApiInfo { 
                    Title = "Pokemons API v2", 
                    Version = "v2",
                    Description = "An API to search for Pokemon and their Types"
                    });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.Last());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                app.UseSwagger();
                /*app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemons API v1"));*/
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "Pokemons API v2"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); // <--
            app.UseAuthorization();

            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
