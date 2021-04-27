using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Configuration;

using AutoMapper;

using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;

namespace PokemonAPI.Data
{
    public class SeedingPokemon
    {
        private readonly ModelBuilder _modelBuilder;
        private readonly CsvSettings _csvSettings;
        private readonly IMapper _mapper;

        public SeedingPokemon(
            ModelBuilder modelBuilder, 
            CsvSettings csvSettings,
            IMapper mapper)
        {
            _modelBuilder = modelBuilder;
            _csvSettings = csvSettings;
            _mapper = mapper;
        }
        
        public void Seeding(List<Typing> listTypings)
        {
            var listPokemon = ReadCSVPokemonDTO();
            SeedPokemons(listTypings, listPokemon);
        }

        private void SeedPokemons(List<Typing> listTypings, List<PokemonDTO> listPokemon)
        {
            //Seeding ---------------------------------------------------------------------------
            for(int index = 0; index < listPokemon.Count; index++)
            {
                var pokemonDTO = listPokemon[index];
                Pokemon pokemon = _mapper.Map<Pokemon>(pokemonDTO);
                pokemon.PokemonId = Guid.NewGuid();
                var pokemonTypings = new List<PokemonTyping>();
                foreach(var t in pokemonDTO.Types)
                {
                    var newPokemonTyping = new PokemonTyping(){
                        PokemonId = pokemon.PokemonId,
                        TypingId = listTypings.Find(typing => typing.Name == t).TypingId
                        };
                    pokemonTypings.Add(newPokemonTyping);
                }

                _modelBuilder.Entity<Pokemon>().HasData(pokemon);
                _modelBuilder.Entity<PokemonTyping>().HasData(pokemonTypings);
            }
        }

        private List<PokemonDTO> ReadCSVPokemonDTO()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var reader = new StreamReader(_csvSettings.CsvPokemon))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<PokemonData>().ToList<PokemonData>();
                var listDTO = PokemonDataHelper.DataToDto(records, _mapper);
                return listDTO;
            }   
        }

        private void WriteToRegistrationsCSV(List<PokemonDTO> listPokemonDTO)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var writer = new StreamWriter(_csvSettings.CsvPokemon))
            using (var csv = new CsvWriter(writer, config))
            {
                var listData = PokemonDataHelper.DtoToData(listPokemonDTO, _mapper);
                csv.WriteRecords(listData);
            }
        }

        private List<PokemonDTO> CreateList()
        {
            var listPokemon = new List<PokemonDTO>();

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
