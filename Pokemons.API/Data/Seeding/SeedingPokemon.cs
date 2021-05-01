using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream.CsvData;
using Pokemons.API.Helpers;


namespace Pokemons.API.Data.Seeding
{
    public class SeedingPokemon : SeedingHelper
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        public List<CsvDataObject> _listTypingData {get; set;}
        public List<CsvDataObject> _listPokemonData {get; set;}


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public SeedingPokemon(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }
        

        // Seeding functions //-------------------------------------------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            // Check if we got provided with necessary data
            if (_listTypingData == null || _listPokemonData == null)
                return;

            // Convert CSV data to corresponding object
            var listTypingData = _listTypingData.Cast<TypingData>().ToList();
            var listPokemonData = _listPokemonData.Cast<PokemonData>().ToList();

            // Do Seeding
            foreach(var dataObj in listPokemonData)
            {
                // Create Pokemon obj
                Pokemon pokemon = _mapper.Map<Pokemon>(dataObj);
                pokemon.PokemonId = Guid.NewGuid();

                // Extract list of PokemonTyping
                var listTypeNames = DataHelper.ExtractTypesFromString(dataObj.Types);
                var pokemonTypings = DataHelper.ExtractPokemonTypings(pokemon, listTypeNames, listTypingData);

                // Actual seeding
                _modelBuilder.Entity<Pokemon>().HasData(pokemon);
                _modelBuilder.Entity<PokemonTyping>().HasData(pokemonTypings);
            }
        }


        // Hardcoded seeding //-------------------------------------------------------------------------------------------------------------------------------
        private List<PokemonData> CreateSeedingData()
        {
            var listPokemon = new List<PokemonData>();

            // Fire
            listPokemon.Add(new PokemonData() {
                PokedexEntry = 4, Name = "Charmander", Generation = 1, Types = "Fire",
                Classification = "Lizard Pokemon", EggGroup = "Monster"});

            // Water
            listPokemon.Add(new PokemonData() { 
                PokedexEntry = 7, Name = "Squirtle", Generation = 1, Types = "Water",
                Classification = "Turtle Pokemon", EggGroup = "Monster"});

            // Grass
            listPokemon.Add(new PokemonData() { 
                PokedexEntry = 1, Name = "Bulbasaur", Generation = 1, Types = "Grass",
                Classification = "Frog Pokemon", EggGroup = "Monster"});

            return listPokemon;
        }
    }
}
