using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Pokemons.API.Services;

namespace Pokemons.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Pokemons.API.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            /*
            builder.ConfigureTestServices(services => 
            {
                
            });
            */
        }
    }
}
