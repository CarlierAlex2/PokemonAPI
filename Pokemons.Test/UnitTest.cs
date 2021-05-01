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
    public class UnitTest:  IClassFixture<CustomWebApplicationFactory>
    {
        [Fact]
        public void UnitTest_GetPokemonStatistics()
        {
            var p1 = new Pokemon(){
                Name = "pokemon 1",
                PokedexEntry = 1,
                Generation = 1,
                Classification = "testing pokemon",
                EggGroup = "tester",
                Hp = 25,
                Attack = 20,
                Defense = 15,
                SpAtk = 60,
                SpDef = 50,
                Speed = 65
            };

            var p2 = new Pokemon(){
                Name = "pokemon 2",
                PokedexEntry = 2,
                Generation = 1,
                Classification = "testing pokemon",
                EggGroup = "tester",
                Hp = 39,
                Attack = 52,
                Defense = 43,
                SpAtk = 105,
                SpDef = 55,
                Speed = 90
            };

            var list = new List<Pokemon>() {p1, p2};
            var statObj = PokemonCalculator.Calculate_Statistics(list);

            Assert.Equal<double>(25, statObj.Statistics["Hp"].Minimum);
            Assert.Equal<double>(32, statObj.Statistics["Hp"].Average);
            Assert.Equal<double>(39, statObj.Statistics["Hp"].Maximum);

            Assert.Equal<double>(20, statObj.Statistics["Attack"].Minimum);
            Assert.Equal<double>(36, statObj.Statistics["Attack"].Average);
            Assert.Equal<double>(52, statObj.Statistics["Attack"].Maximum);

            Assert.Equal<double>(15, statObj.Statistics["Defense"].Minimum);
            Assert.Equal<double>(29, statObj.Statistics["Defense"].Average);
            Assert.Equal<double>(43, statObj.Statistics["Defense"].Maximum);

            Assert.Equal<double>(60, statObj.Statistics["SpAtk"].Minimum);
            Assert.Equal<double>(82.5, statObj.Statistics["SpAtk"].Average);
            Assert.Equal<double>(105, statObj.Statistics["SpAtk"].Maximum);

            Assert.Equal<double>(50, statObj.Statistics["SpDef"].Minimum);
            Assert.Equal<double>(52.5, statObj.Statistics["SpDef"].Average);
            Assert.Equal<double>(55, statObj.Statistics["SpDef"].Maximum);

            Assert.Equal<double>(65, statObj.Statistics["Speed"].Minimum);
            Assert.Equal<double>(77.5, statObj.Statistics["Speed"].Average);
            Assert.Equal<double>(90, statObj.Statistics["Speed"].Maximum);
        }

        [Fact]
        public void UnitTest_CleanupPokemonDTO()
        {

            var testDto = new PokemonDTO()
            {
                Name = "Villager #27", 
                Types = new List<string>{"Gr ass "}, 
                EggGroup = "Mine craft ",
                PokedexEntry = 111, Generation = 111, Classification = "Village Pokemon",
                Hp = 1, Attack = 1,Defense = 1, SpAtk = 1, SpDef = 1, Speed = 1
            };
            
            var result = PokemonHelper.CleanupPokemonDTO(testDto);
            Assert.Equal("Villager#27", result.Name);
            Assert.Equal("Minecraft", result.EggGroup);
            Assert.Equal(new List<string>{"Grass"}, result.Types);
        }

        [Fact]
        public void UnitTest_VerifyPokemonDTO()
        {
            List<string> listTyping = new List<string>() {"Grass", "Fire", "Water"};
            var testDto = new PokemonDTO()
            {
                Name = "Villager",
                PokedexEntry = 111,
                Generation = 111,
                Types = new List<string>{"Grass"},
                Classification = "Village Pokemon",
                EggGroup = "Minecraft",
                Hp = 1,
                Attack = 1,
                Defense = 1,
                SpAtk = 1,
                SpDef = 1,
                Speed = 1
            };
            
            var result = PokemonHelper.VerifyPokemonDTO(testDto, listTyping);
            Assert.Equal(true, result.Item1);

            testDto.Types = new List<string>(){ "Dirt" };
            result = PokemonHelper.VerifyPokemonDTO(testDto, listTyping);
            Assert.NotEqual(true, result.Item1);
        }
    }
}
