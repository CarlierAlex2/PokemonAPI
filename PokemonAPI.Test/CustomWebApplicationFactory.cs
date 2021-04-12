using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace PokemonAPI.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<PokemonAPI.Startup>
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
