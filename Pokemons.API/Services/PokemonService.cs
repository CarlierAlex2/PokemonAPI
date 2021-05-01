using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using AutoMapper;
using Microsoft.Extensions.Options;

using Pokemons.API.Models;
using Pokemons.API.DTO;
using Pokemons.API.Repositories;
using Pokemons.API.Helpers;


namespace Pokemons.API.Services
{
    public interface IPokemonService
    {
        Task<Pokemon> AddPokemon(PokemonDTO pokemonDTO);
        Task DeletePokemon_ByEntry(int pokedexEntry);
        Task DeletePokemon_ByEntryAndGen(int pokedexEntry, int generation);
        Task<PokemonDTO> GetPokemon_ByName(string name);
        Task<List<PokemonDTO>> GetPokemons_ByEntry(int pokedexEntry);
        Task<PokemonDTO> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation);
        Task<List<PokemonDTO>> GetPokemon_ByType_V1(string typeName);
        Task<List<PokemonBaseDTO>> GetPokemon_ByType_V2(string typeName);
        Task<PokemonList> GetPokemons_List();
        Task<PokemonList> GetPokemons_List_ByType(string typeName);
        Task<PokemonStatisticsList> GetPokemons_Statistics(List<string> names);
        Task<List<PokemonDTO>> GetPokemons_V1();
        Task<List<PokemonBaseDTO>> GetPokemons_V2();
        Task<TypingDTO> GetTyping_ByName(string name);
        Task<TypingList> GetTypings_List();
        Task<List<Typing>> GetTypings_V1();
        Task<List<TypingBaseDTO>> GetTypings_V2();
        Task<Tuple<bool, string>> VerifyPokemonDTO(PokemonDTO pokemonDTO);
    }


    public class PokemonService : IPokemonService
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        private readonly IMapper _mapper;
        private readonly ITypingRepository _typeRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IConvertHelper _convertHelper;


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public PokemonService(
            IMapper mapper,
            ITypingRepository typeRepository, IPokemonRepository pokemonRepository,
            IConvertHelper convertHelper)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
            _pokemonRepository = pokemonRepository;
            _convertHelper = convertHelper;
        }


        #region // Get Typing - group //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<List<Typing>> GetTypings_V1()
        {
            // Get list of Typing
            var results = await _typeRepository.GetTypings();
            if (results.Count <= 0)
                return null;

            return results;
        }

        public async Task<List<TypingBaseDTO>> GetTypings_V2()
        {
            // Get list of Typing, less clutter
            var results = await _typeRepository.GetTypings();
            if (results.Count <= 0)
                return null;

            List<TypingBaseDTO> listTypingBaseDTO = _convertHelper.TypingBase_To_DTO_List(results);
            return listTypingBaseDTO;
        }
        #endregion


        #region // Get Typing - list //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<TypingList> GetTypings_List()
        {
            // Get list of Typing names
            var results = await _typeRepository.GetTypings_List();
            if (results.Count <= 0)
                return null;

            TypingList typingList = new TypingList() { Names = results };
            return typingList;
        }
        #endregion



        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<TypingDTO> GetTyping_ByName(string name)
        {
            // Get Typing, filter by name, with details
            Typing result = await _typeRepository.GetTyping_ByName(name, true);
            if (result == null)
                return null;

            TypingDTO resultDTO = _convertHelper.Typing_To_DTO(result);
            return resultDTO;
        }
        #endregion


        #region // Get Pokemon - group //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<List<PokemonDTO>> GetPokemons_V1()
        {
            // Get list of Pokemon
            var results = await _pokemonRepository.GetPokemons();
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = _convertHelper.Pokemon_To_DTOList(results);
            return resultDTO;
        }

        public async Task<List<PokemonBaseDTO>> GetPokemons_V2()
        {
            // Get list of Pokemon, less clutter
            var results = await _pokemonRepository.GetPokemons();
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonBaseDTO> resultDTO = _convertHelper.PokemonBase_To_DTOList(results);
            return resultDTO;
        }

        public async Task<List<PokemonDTO>> GetPokemon_ByType_V1(string typeName)
        {
            // Get list of Pokemon, filter by type
            var results = await _pokemonRepository.GetPokemons_ByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = _convertHelper.Pokemon_To_DTOList(results);
            return resultDTO;
        }

        public async Task<List<PokemonBaseDTO>> GetPokemon_ByType_V2(string typeName)
        {
            // Get list of Pokemon, filter by type, less clutter
            var results = await _pokemonRepository.GetPokemons_ByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonBaseDTO> resultDTO = _convertHelper.PokemonBase_To_DTOList(results);
            return resultDTO;
        }
        #endregion



        #region // Get Pokemon - List //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<PokemonList> GetPokemons_List()
        {
            // Get list of Pokemon names
            var results = await _pokemonRepository.GetPokemons_List();
            if (results == null || results.Count <= 0)
                return null;

            PokemonList listObj = new PokemonList() { Names = results };
            return listObj;
        }

        public async Task<PokemonList> GetPokemons_List_ByType(string typeName)
        {
            // Get list of Pokemon names, filter by type
            var results = await _pokemonRepository.GetPokemons_List_ByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            PokemonList listObj = new PokemonList() { Names = results };
            return listObj;
        }
        #endregion



        #region // Get Pokemon - specific //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<PokemonDTO> GetPokemon_ByName(string name)
        {
            // Get a Pokemon by name
            var results = await _pokemonRepository.GetPokemon_ByName(name);
            if (results == null)
                return null;

            PokemonDTO resultDTO = _convertHelper.Pokemon_To_DTO(results);
            return resultDTO;
        }
        
        public async Task<List<PokemonDTO>> GetPokemons_ByEntry(int pokedexEntry)
        {
            // Get a list of Pokemon by entry
            var results = await _pokemonRepository.GetPokemons_ByEntry(pokedexEntry, DetailLevel.Details);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = _convertHelper.Pokemon_To_DTOList(results);
            return resultDTO;
        }

        public async Task<PokemonDTO> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation)
        {
            // Get a Pokemon by entry and gen
            var results = await _pokemonRepository.GetPokemon_ByEntryAndGen(pokedexEntry, generation, DetailLevel.Details);
            if (results == null)
                return null;

            PokemonDTO resultDTO = _convertHelper.Pokemon_To_DTO(results);
            return resultDTO;
        }
        #endregion



        #region // Get Pokemon Statistics //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<PokemonStatisticsList> GetPokemons_Statistics(List<string> names)
        {
            // Get statistics about a group of pokemon (avg, min, max), filter by names

            // Search for pokemon by name, if one doesn't exist, it is skipped
            List<Pokemon> results = new List<Pokemon>();
            foreach (var name in names)
            {
                var result = await _pokemonRepository.GetPokemon_ByName(name);
                if (result != null)
                    results.Add(result);
            }

            // Calculate statistics for group of Pokemon
            if (results.Count <= 0)
                return null;
            PokemonStatisticsList statObj = PokemonCalculator.Calculate_Statistics(results);
            return statObj;
        }
        #endregion



        #region // Add Pokemon //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<Pokemon> AddPokemon(PokemonDTO pokemonDTO)
        {
            // Add a Pokemon to database

            // Check if Pokemon with entry and gen already exists
            PokemonDTO checkExists = await GetPokemon_ByEntryAndGen(pokemonDTO.PokedexEntry, pokemonDTO.Generation);
            if (checkExists != null)
                return null;

            // Add Pokemon
            Pokemon pokemon = await CreatePokemonDTO_FromPokemon(pokemonDTO);
            pokemon = await _pokemonRepository.AddPokemon(pokemon);
            return pokemon;
        }
        #endregion



        #region // Delete Pokemon //-------------------------------------------------------------------------------------------------------------------------------
        public async Task DeletePokemon_ByEntry(int pokedexEntry)
        {
            // Remove Pokemon by entry
            
            // Search for Pokemon list by entry
            //List<Pokemon> listPokemon = await _pokemonRepository.GetPokemons_ByEntry(pokedexEntry, DetailLevel.ForeignKeys);
            List<Pokemon> listPokemon = await _pokemonRepository.GetPokemons_ByEntry(pokedexEntry, DetailLevel.Basic);
            if (listPokemon == null)
                return;

            // Remove Pokemon list
            await _pokemonRepository.DeletePokemon_List(listPokemon);
        }

        public async Task DeletePokemon_ByEntryAndGen(int pokedexEntry, int generation)
        {
            // Remove Pokemon by entry and gen
            
            // Search for a Pokemon by entry and gen (unique values)
            //Pokemon pokemon = await _pokemonRepository.GetPokemon_ByEntryAndGen(pokedexEntry, generation, DetailLevel.ForeignKeys);
            Pokemon pokemon = await _pokemonRepository.GetPokemon_ByEntryAndGen(pokedexEntry, generation, DetailLevel.Basic);
            if (pokemon == null)
                return;
            
            // Remove Pokemon
            await _pokemonRepository.DeletePokemon(pokemon);
        }
        #endregion



        #region // Helper functions //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<Tuple<bool, string>> VerifyPokemonDTO(PokemonDTO pokemonDTO)
        {
            // Verify if values are correct
            var listTypes = await _typeRepository.GetTypings_List();
            return PokemonHelper.VerifyPokemonDTO(pokemonDTO, listTypes);
        }

        private async Task<Pokemon> CreatePokemonDTO_FromPokemon(PokemonDTO pokemonDTO)
        {
            // Add pokemon + return results
            Pokemon pokemon = _mapper.Map<Pokemon>(pokemonDTO);
            pokemon.PokemonId = Guid.NewGuid();
            pokemon.PokemonTypings = new List<PokemonTyping>();
            await AddPokemonTyping_To_Pokemon(pokemonDTO, pokemon);

            return pokemon;
        }

        private async Task AddPokemonTyping_To_Pokemon(PokemonDTO pokemonDTO, Pokemon pokemon)
        {
            foreach (var typingName in pokemonDTO.Types)
            {
                var typing = await _typeRepository.GetTyping_ByName(typingName);
                pokemon.PokemonTypings.Add(new PokemonTyping()
                {
                    PokemonId = pokemon.PokemonId,
                    TypingId = typing.TypingId
                });
            } 
        }
        #endregion
    }
}
