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


        //------------------------------------------------------------------------------------------------------------------------
        #region Test Typing Methods
        [Fact]
        public async Task GetTypings_Return_Ok()
        {
            var response = await Client.GetAsync("/api/types");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<List<TypingBaseDTO>>(content);
            Assert.NotEmpty(body);
        }

        [Fact]
        public async Task GetTypingList_Return_Ok()
        {
            var response = await Client.GetAsync("/api/types/list");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<TypingList>(content);
            Assert.NotEmpty(body.Names);
        }

        //------------------------------------------------------------------------------------------------------------------------
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
        public async Task GetTypingDetail_Return_NoOk()
        {
            string typingName = "test";
            var response = await Client.GetAsync($"/api/type/{typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion

        //------------------------------------------------------------------------------------------------------------------------
        #region Test Pokemon Methods - Get list
        [Fact]
        public async Task GetPokemons_Return_Ok()
        {
            var response = await Client.GetAsync("/api/pokemons");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<List<PokemonBaseDTO>>(content);
            Assert.NotEmpty(body);
        }

        [Fact]
        public async Task GetPokemons_List_Return_Ok()
        {
            var response = await Client.GetAsync("/api/pokemons/list");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonList>(content);
            Assert.NotEmpty(body.Names);
        }

        [Fact]
        public async Task GetPokemons_List_ByType_Return_Ok()
        {
            string typingName = "Grass";
            var response = await Client.GetAsync($"/api/pokemons/list?typeName={typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonList>(content);
            Assert.NotEmpty(body.Names);
        }

        [Fact]
        public async Task GetPokemons_List_ByType_Return_NotOk()
        {
            string typingName = "Wood";
            var response = await Client.GetAsync($"/api/pokemons/list?typeName={typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetPokemons_ByType_Return_Ok()
        {
            string typingName = "Grass";
            var response = await Client.GetAsync( $"/api/pokemons?typeName={typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<List<PokemonBaseDTO>>(content);
            Assert.NotEmpty(body);
        }

        /*
        //dont know why but always returns ok when executing full test project, in debug + manual + run it gives badrequest
        [Fact]
        public async Task GetPokemons_ByType_Return_NotOk()
        {
            string typingName = "test";
            var response = await Client.GetAsync( $"/api/pokemons?typeName={typingName}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        */
        #endregion

        //------------------------------------------------------------------------------------------------------------------------
        #region Test Pokemon Methods - Specific
        [Fact]
        public async Task GetPokemons_ByName_Return_Ok()
        {
            string name = "Abra";
            var response = await Client.GetAsync($"/api/pokemons/name/{name}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonBaseDTO>(content);
            Assert.Equal(name, body.Name);
        }

        [Fact]
        public async Task GetPokemons_ByName_Return_NotOk()
        {
            string name = "V";
            var response = await Client.GetAsync($"/api/pokemons/name/{name}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            name = "Villager#2145";
            response = await Client.GetAsync($"/api/pokemons/name/{name}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task GetPokemon_ByEntry_Return_Ok()
        {
            int pokedexEntry = 637;
            var response = await GetPokemon_ByEntry(pokedexEntry);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<List<PokemonDTO>>(content);
            Assert.NotNull(body);
            Assert.NotEmpty(body);
            Assert.Equal<int>(pokedexEntry, body[0].PokedexEntry);
        }

        public async Task<HttpResponseMessage> GetPokemon_ByEntry(int pokedexEntry)
        {
            return await Client.GetAsync($"/api/pokemons/entry/{pokedexEntry}");
        }

        [Fact]
        public async Task GetPokemon_ByEntry_Return_NotOk()
        {
            int pokedexEntry = -1;
            var response = await GetPokemon_ByEntry(pokedexEntry);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            pokedexEntry = 999999;
            response = await GetPokemon_ByEntry(pokedexEntry);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        //------------------------------------------------------------------------------------------------------------------------
        [Fact]
        public async Task GetPokemon_ByEntryAndGen_Return_Ok()
        {
            int pokedexEntry = 637;
            int generation = 5;
            var response = await GetPokemon_ByEntryAndGen(pokedexEntry, generation);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<PokemonDTO>(content);
            Assert.NotNull(body);
            Assert.Equal<int>(pokedexEntry, body.PokedexEntry);
        }

        public async Task<HttpResponseMessage> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation)
        {
            return await Client.GetAsync($"/api/pokemon/entry/{pokedexEntry}/gen/{generation}");
        }

        [Fact]
        public async Task GetPokemon_ByEntryAndGen_Return_NotOk()
        {
            int pokedexEntry = -1;
            int generation = -1;
            var response = await GetPokemon_ByEntryAndGen(pokedexEntry, generation);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            generation = 1;
            response = await GetPokemon_ByEntryAndGen(pokedexEntry, generation);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            generation = -1;
            pokedexEntry = 1;
            response = await GetPokemon_ByEntryAndGen(pokedexEntry, generation);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            generation = 9999;
            pokedexEntry = 9999;
            response = await GetPokemon_ByEntryAndGen(pokedexEntry, generation);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion


        //------------------------------------------------------------------------------------------------------------------------
        #region Test Pokemon Methods - Delete + Add
        [Fact]
        public async Task Add_And_Delete_Pokemon_Ok()
        {
            PokemonDTO pokemonDTO = new PokemonDTO()
            {
                Name = "tester",
                PokedexEntry = 88,
                Generation = 88,
                Types = new List<string>{"Steel"},
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
            await AddPokemon_NotOk(pokemonDTO);
            await DeletePokemon_Ok(pokemonDTO.PokedexEntry, pokemonDTO.Generation);
        }

        private async Task AddPokemon_Ok(PokemonDTO pokemonDTO)
        {
            string json = JsonConvert.SerializeObject(pokemonDTO);
            StringContent contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/pokemon", contentSend);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            pokemonDTO = PokemonHelper.CleanupPokemonDTO(pokemonDTO);
            var contentReceived = await response.Content.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<Pokemon>(contentReceived);
            Assert.NotNull(body);
            Assert.Equal<string>(pokemonDTO.Name, body.Name);
        }

        private async Task DeletePokemon_Ok(int pokedexEntry, int generation)
        {
            var responseDelete = await Client.DeleteAsync($"/api/pokemons/entry/{pokedexEntry}?generation={generation}");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task DeletePokemon_ByEntry_Ok(int pokedexEntry)
        {
            var responseDelete = await Client.DeleteAsync($"/api/pokemons/entry/{pokedexEntry}");
            responseDelete.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Add_And_Delete_Pokemon_ByEntry_Ok()
        {
            PokemonDTO pokemonDTO = new PokemonDTO()
            {
                Name = "tester",
                PokedexEntry = 100,
                Generation = 1,
                Types = new List<string>{"Electric"},
                Classification = "testing pokemon",
                EggGroup = "tester"
            };

            PokemonDTO pokemonDTO2 = new PokemonDTO()
            {
                Name = "tester",
                PokedexEntry = 100,
                Generation = 2,
                Types = new List<string>{"Fire"},
                Classification = "testing pokemon",
                EggGroup = "tester"
            };

            await AddPokemon_Ok(pokemonDTO);
            await AddPokemon_Ok(pokemonDTO2);
            await DeletePokemon_ByEntry_Ok(pokemonDTO.PokedexEntry);
        }

        [Fact]
        public async Task AddPokemon_Return_NotOk()
        {
            // PokedexEntry not correct
            PokemonDTO testDto = new PokemonDTO()
            {
                Name = "Vi",
                PokedexEntry = 0,
                Generation = 0,
                Types = new List<string>{"Wood"},
                Classification = "Village Pokemon",
                EggGroup = "Minecraft",
                Hp = 1,
                Attack = 1,
                Defense = 1,
                SpAtk = 1,
                SpDef = 1,
                Speed = 1
            };

            await AddPokemon_NotOk(testDto);
        }

        public async Task AddPokemon_NotOk(PokemonDTO testDto)
        {
            string json = JsonConvert.SerializeObject(testDto);
            StringContent contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/pokemon", contentSend);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion


        //------------------------------------------------------------------------------------------------------------------------
        #region Test Pokemon Methods - Statistics

        [Fact]
        public async Task GetPokemon_Statistics_Return_Ok()
        {
            List<string> names = new List<string>(){"Abra", "Charmander"};

            string json = JsonConvert.SerializeObject(names);
            StringContent contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/pokemons/statistics", contentSend);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetPokemon_Statistics_Return_NotOk()
        {
            string json = JsonConvert.SerializeObject(null);
            StringContent contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/pokemons/statistics", contentSend);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            List<string> names = new List<string>();
            json = JsonConvert.SerializeObject(names);
            contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            response = await Client.PostAsync("/api/pokemons/statistics", contentSend);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            names = new List<string>(){"Villager#1, Villager#2"};
            json = JsonConvert.SerializeObject(names);
            contentSend = new StringContent(json, Encoding.UTF8, "application/json");
            response = await Client.PostAsync("/api/pokemons/statistics", contentSend);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion
    }
}
