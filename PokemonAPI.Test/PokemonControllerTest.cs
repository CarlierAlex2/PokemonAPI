using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;
using FluentAssertions;
using Newtonsoft.Json;

using PokemonAPI.DTO;
using PokemonAPI.Models;

namespace PokemonAPI.Test
{
    public class PokemonControllerTest: IClassFixture<CustomWebApplicationFactory>
    {
        //dotnet test
        //dotnet test /p:CollectCoverage=true
        //dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit*]\*" /p:CoverletOutput="./TestResults/"
        //dotnet reportgenerator "-reports:TestResults\coverage.cobertura.xml" "-targetdir:TestResults\html" -reporttypes:HTML

        public HttpClient Client { get; set;}
        public PokemonControllerTest(CustomWebApplicationFactory fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task GetTypings_Return_Ok()
        {
            var response = await Client.GetAsync("/api/types");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var listTyping = JsonConvert.DeserializeObject<List<Typing>>(content);
            Assert.True(listTyping.Count > 0);
        }

        [Fact]
        public async Task GetTypingDetail_Return_Ok()
        {
            string typingName = "Steel";
            var response = await Client.GetAsync($"/api/type/{typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var typingDTO = JsonConvert.DeserializeObject<TypingDTO>(content);
            Assert.NotNull(typingDTO);
            Assert.Equal<string>(typingName, typingDTO.Name);
        }

        [Fact]
        public async Task GetPokemons_Return_Ok()
        {
            var response = await Client.GetAsync("/api/pokemons");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var listPokemon = JsonConvert.DeserializeObject<List<PokemonDTO>>(content);
            Assert.True(listPokemon.Count > 0);
        }

        [Fact]
        public async Task GetPokemons_ByType_Return_Ok()
        {
            string typingName = "Grass";
            var response = await Client.GetAsync($"/api/pokemons?typeName={typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var listPokemon = JsonConvert.DeserializeObject<List<PokemonDTO>>(content);
            Assert.True(listPokemon.Count > 0);

            PokemonDTO pokemon = listPokemon[0];
            Assert.NotNull(pokemon);
            Assert.Contains<string>(typingName, pokemon.Types);
        }
    }
}
