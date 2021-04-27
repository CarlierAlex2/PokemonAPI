using System;
using System.Collections.Generic;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Helpers;


namespace PokemonAPI.Data.Seeding
{
    public class SeedingPokemon : SeedingHelper
    {
        public List<Typing> _listTypings {get; set;}
        public List<PokemonDTO> _listPokemonDTO {get; set;}
        public SeedingPokemon(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }
        
        //Seeding functions -------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            if (_listTypings == null || _listPokemonDTO == null)
                return;

            //Seeding ---------------------------------------------------------------------------
            for(int index = 0; index < _listPokemonDTO.Count; index++)
            {
                var pokemonDTO = _listPokemonDTO[index];
                Pokemon pokemon = _mapper.Map<Pokemon>(pokemonDTO);
                pokemon.PokemonId = Guid.NewGuid();
                var pokemonTypings = new List<PokemonTyping>();
                foreach(var t in pokemonDTO.Types)
                {
                    var newPokemonTyping = new PokemonTyping(){
                        PokemonId = pokemon.PokemonId,
                        TypingId = _listTypings.Find(typing => typing.Name == t).TypingId
                        };
                    pokemonTypings.Add(newPokemonTyping);
                }

                _modelBuilder.Entity<Pokemon>().HasData(pokemon);
                _modelBuilder.Entity<PokemonTyping>().HasData(pokemonTypings);
            }
        }

        //Hardcoded seeding -------------------------------------------------------------------------------------------
        private List<ModelObject> CreateList()
        {
            var listPokemon = new List<ModelObject>();

            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 4, Name = "Charmander", Generation = 1, Classification = "Lizard Pokemon", EggGroup = "Monster,Dragon",
                Types = new List<string>{"Fire"}});

            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 260, Name = "Swampert", Generation = 3, Classification = "Mud Fish Pokemon", EggGroup = "Monster,Water 1",
                Types = new List<string>{"Water", "Ground"}});

            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 753, Name = "Fomantis", Generation = 7, Classification = "Sickle Grass Pokemon", EggGroup = "Grass",
                Types = new List<string>{"Grass"}});

            return listPokemon;
        }
    }
}
