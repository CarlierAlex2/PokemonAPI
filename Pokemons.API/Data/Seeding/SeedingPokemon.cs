using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream.CsvData;
using Pokemons.API.DTO;

namespace Pokemons.API.Data.Seeding
{
    public class SeedingPokemon : SeedingHelper
    {
        public List<CsvDataObject> _listTypingData {get; set;}
        public List<CsvDataObject> _listPokemonData {get; set;}
        public SeedingPokemon(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }
        
        //Seeding functions -------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            if (_listTypingData == null || _listPokemonData == null)
                return;
            var listTypingData = _listTypingData.Cast<TypingData>().ToList();
            var listPokemonData = _listPokemonData.Cast<PokemonData>().ToList();

            //Seeding ---------------------------------------------------------------------------
            foreach(var dataObj in listPokemonData)
            {
                Pokemon pokemon = _mapper.Map<Pokemon>(dataObj);
                pokemon.PokemonId = Guid.NewGuid();

                var listTypeNames = MappingHelper.ExtractTypesFromString(dataObj.Types);
                var pokemonTypings = MappingHelper.ExtractPokemonTypings(pokemon, listTypeNames, listTypingData);

                _modelBuilder.Entity<Pokemon>().HasData(pokemon);
                _modelBuilder.Entity<PokemonTyping>().HasData(pokemonTypings);
            }
        }

        //Hardcoded seeding -------------------------------------------------------------------------------------------
        private List<PokemonData> CreateSeedingData()
        {
            var listPokemon = new List<PokemonData>();

            listPokemon.Add(new PokemonData() { 
                PokedexEntry = 4, Name = "Charmander", Generation = 1, Classification = "Lizard Pokemon", EggGroup = "Monster,Dragon",
                Types = "Fire"});

            listPokemon.Add(new PokemonData() { 
                PokedexEntry = 260, Name = "Swampert", Generation = 3, Classification = "Mud Fish Pokemon", EggGroup = "Monster,Water 1",
                Types = "Water,Ground"});

            listPokemon.Add(new PokemonData() { 
                PokedexEntry = 753, Name = "Fomantis", Generation = 7, Classification = "Sickle Grass Pokemon", EggGroup = "Grass",
                Types = "Grass"});

            return listPokemon;
        }
    }
}
