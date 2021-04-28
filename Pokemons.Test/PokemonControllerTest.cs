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

using Pokemons.API.DTO;
using Pokemons.API.Models;
using Pokemons.API.Helpers;

namespace Pokemons.Test
{
    public class PokemonControllerTest:  IClassFixture<CustomWebApplicationFactory>
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
            var body = JsonConvert.DeserializeObject<TypingList>(content);
            Assert.NotEmpty(body.Names);
        }

        [Fact]
        public async Task GetTypingDetail_Return_Ok()
        {
            string typingName = "Steel";
            var response = await Client.GetAsync($"/api/type/{typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<TypingDTO>(content);
            Assert.NotNull(body);
            Assert.Equal<string>(typingName, body.Name);
        }

        [Fact]
        public async Task GetPokemons_Return_Ok()
        {
            var response = await Client.GetAsync("/api/pokemons");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonList>(content);
            Assert.NotEmpty(body.Names);
        }

        [Fact]
        public async Task GetPokemons_ByType_Return_Ok()
        {
            string typingName = "Grass";
            var response = await Client.GetAsync( $"/api/pokemons?typeName={typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonList>(content);
            Assert.NotEmpty(body.Names);
        }

        /*
        [Fact]
        public async Task GetPokemons_ByType_Return_NotOk()
        {
            string typingName = "test";
            var response = await Client.GetAsync($"/api/pokemons?typeName={typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        */

        /*
        [Fact]
        public async Task GetPokemon_ById_ByType_Return_Ok()
        {
            Guid id = Guid.Parse("9ae91a19-ba47-4506-ab97-fe20718b9bea");

            var response = await Client.GetAsync($"/api/pokemon/id/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonDTO>(content);
            Assert.NotNull(body);
        }
        */

        [Fact]
        public async Task GetPokemon_ByEntry_Return_Ok()
        {
            int pokedexEntry = 637;
            var response = await Client.GetAsync($"/api/pokemons/entry/{pokedexEntry}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<List<PokemonDTO>>(content);
            Assert.NotNull(body);
            Assert.NotEmpty(body);
            Assert.Equal<int>(pokedexEntry, body[0].PokedexEntry);
        }

        [Fact]
        public async Task GetPokemon_ByEntry_Return_NotOk()
        {
            int pokedexEntry = -1;
            var response = await Client.GetAsync($"/api/pokemons/entry/{pokedexEntry}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            pokedexEntry = 1001;
            response = await Client.GetAsync($"/api/pokemons/entry/{pokedexEntry}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetPokemon_ByEntryAndGen_Return_Ok()
        {
            int pokedexEntry = 637;
            int generation = 5;
            var response = await Client.GetAsync($"/api/pokemon/entry/{pokedexEntry}/gen/{generation}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonDTO>(content);
            Assert.NotNull(body);
            Assert.Equal<int>(pokedexEntry, body.PokedexEntry);
        }

        [Fact]
        public async Task GetPokemon_ByEntryAndGen_Return_NotOk()
        {
            int pokedexEntry = 1001;
            int generation = 1001;
            var response = await Client.GetAsync($"/api/pokemon/entry/{pokedexEntry}/gen/{generation}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            generation = 0;
            response = await Client.GetAsync($"/api/pokemon/entry/{pokedexEntry}/gen/{generation}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            generation = 1001;
            pokedexEntry = 0;
            response = await Client.GetAsync($"/api/pokemon/entry/{pokedexEntry}/gen/{generation}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Add_And_Delete_Pokemon_Ok()
        {
            PokemonDTO pokemonDTO = new PokemonDTO()
            {
                Name = "test pokemon",
                PokedexEntry = 1000,
                Generation = 10,
                Types = new List<string>{"Electric"},
                Classification = "testing pokemon",
                EggGroup = "tester",
                Hp = 1,
                Attack = 1,
                Defense = 1,
                SpAtk = 1,
                SpDef = 1,
                Speed = 1
            };

            await AddPokemon_Ok(pokemonDTO);
            await DeletePokemon_Ok(pokemonDTO);
        }

        private async Task AddPokemon_Ok(PokemonDTO pokemonDTO)
        {
            string json = JsonConvert.SerializeObject(pokemonDTO);
            StringContent contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/pokemon", contentSend);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentReceived = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<Pokemon>(contentReceived);
            Assert.NotNull(body);
            Assert.Equal<string>(pokemonDTO.Name, body.Name);
        }

        private async Task DeletePokemon_Ok(PokemonDTO pokemonDTO)
        {
            string json = JsonConvert.SerializeObject(pokemonDTO);
            StringContent contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            int pokedexEntry = pokemonDTO.PokedexEntry;
            int generation = pokemonDTO.Generation;
            var responseDelete = await Client.DeleteAsync($"/api/pokemons/entry/{pokedexEntry}?generation={generation}");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
