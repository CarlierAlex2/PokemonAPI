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


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public PokemonService(
            IMapper mapper,
            ITypingRepository typeRepository,
            IPokemonRepository pokemonRepository)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
            _pokemonRepository = pokemonRepository;
        }


        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
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

            List<TypingBaseDTO> listTypingBaseDTO = GetTypingBaseDTOList(results);
            return listTypingBaseDTO;
        }
        #endregion


        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
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

            TypingDTO resultDTO = _mapper.Map<TypingDTO>(result);
            for (int index = 0; index < result.TypeOffense.Count; index++)
            {
                resultDTO.TypeOffense[index] = _mapper.Map<TypeEffectOffenseDTO>(result.TypeOffense[index]);
                resultDTO.TypeOffense[index].Defend = result.TypeOffense[index].DefenseTyping.Name; //offense -> defense
            }
            for (int index = 0; index < result.TypeDefense.Count; index++)
            {
                resultDTO.TypeDefense[index] = _mapper.Map<TypeEffectDefenseDTO>(result.TypeDefense[index]);
                resultDTO.TypeDefense[index].Attack = result.TypeDefense[index].OffenseTyping.Name; //defense -> offense
            }

            return resultDTO;
        }
        #endregion


        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<List<PokemonDTO>> GetPokemons_V1()
        {
            // Get list of Pokemon
            var results = await _pokemonRepository.GetPokemons();
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = GetPokemonDTOList(results);
            return GetPokemonDTOList(results);
        }

        public async Task<List<PokemonBaseDTO>> GetPokemons_V2()
        {
            // Get list of Pokemon, less clutter
            var results = await _pokemonRepository.GetPokemons();
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonBaseDTO> resultDTO = GetPokemonBaseDTOList(results);
            return resultDTO;
        }

        public async Task<List<PokemonDTO>> GetPokemon_ByType_V1(string typeName)
        {
            // Get list of Pokemon, filter by type
            var results = await _pokemonRepository.GetPokemons_ByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = GetPokemonDTOList(results);
            return resultDTO;
        }

        public async Task<List<PokemonBaseDTO>> GetPokemon_ByType_V2(string typeName)
        {
            // Get list of Pokemon, filter by type, less clutter
            var results = await _pokemonRepository.GetPokemons_ByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonBaseDTO> resultDTO = GetPokemonBaseDTOList(results);
            return resultDTO;
        }
        #endregion



        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
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



        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<PokemonDTO> GetPokemon_ByName(string name)
        {
            // Get a Pokemon by name
            var results = await _pokemonRepository.GetPokemon_ByName(name);
            if (results == null)
                return null;

            PokemonDTO resultDTO = GetPokemonDTO(results);
            return resultDTO;
        }
        
        public async Task<List<PokemonDTO>> GetPokemons_ByEntry(int pokedexEntry)
        {
            // Get a list of Pokemon by entry
            var results = await _pokemonRepository.GetPokemons_ByEntry(pokedexEntry, DetailLevel.Details);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = GetPokemonDTOList(results);
            return resultDTO;
        }

        public async Task<PokemonDTO> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation)
        {
            // Get a Pokemon by entry and gen
            var results = await _pokemonRepository.GetPokemon_ByEntryAndGen(pokedexEntry, generation, DetailLevel.Details);
            if (results == null)
                return null;

            PokemonDTO resultDTO = GetPokemonDTO(results);
            return resultDTO;
        }
        #endregion



        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
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



        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<Pokemon> AddPokemon(PokemonDTO pokemonDTO)
        {
            // Add a Pokemon to database

            // Check if Pokemon with entry and gen already exists
            PokemonDTO checkExists = await GetPokemon_ByEntryAndGen(pokemonDTO.PokedexEntry, pokemonDTO.Generation);
            if (checkExists != null)
                return null;

            // Add Pokemon
            Pokemon pokemon = await GetPokemonDTO_FromPokemon(pokemonDTO);
            pokemon = await _pokemonRepository.AddPokemon(pokemon);
            return pokemon;
        }
        #endregion



        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
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
        
        private List<TypingBaseDTO> GetTypingBaseDTOList(List<Typing> listTyping)
        {
            // Convert list TypingBaseDTO to TypingDto
            List<TypingBaseDTO> dto = _mapper.Map<List<TypingBaseDTO>>(listTyping);
            return dto;
        }

        private async Task<Pokemon> GetPokemonDTO_FromPokemon(PokemonDTO pokemonDTO)
        {
            // Add pokemon + return results
            Pokemon pokemon = _mapper.Map<Pokemon>(pokemonDTO);
            pokemon.PokemonId = Guid.NewGuid();
            pokemon.PokemonTypings = new List<PokemonTyping>();
            foreach (var typingName in pokemonDTO.Types)
            {
                var typing = await _typeRepository.GetTyping_ByName(typingName);
                pokemon.PokemonTypings.Add(new PokemonTyping()
                {
                    PokemonId = pokemon.PokemonId,
                    TypingId = typing.TypingId
                });
            }

            return pokemon;
        }

        private PokemonDTO GetPokemonDTO(Pokemon pokemon)
        {
            // Convert Pokemon to PokemonDTO
            PokemonDTO dto = _mapper.Map<PokemonDTO>(pokemon);
            if (pokemon.PokemonTypings != null)
                dto.Types = pokemon.PokemonTypings.Select(r => r.Typing.Name).ToList();

            return dto;
        }

        private PokemonBaseDTO GetPokemonBaseDTO(Pokemon pokemon)
        {
            // Convert Pokemon to PokemonBase
            PokemonBaseDTO dto = _mapper.Map<PokemonBaseDTO>(pokemon);
            if (pokemon.PokemonTypings != null)
                dto.Types = pokemon.PokemonTypings.Select(r => r.Typing.Name).ToList();

            return dto;
        }

        private List<PokemonDTO> GetPokemonDTOList(List<Pokemon> pokemons)
        {
            // Convert list Pokemon to PokemonDTO
            List<PokemonDTO> resultDTO = new List<PokemonDTO>();
            foreach (var pok in pokemons)
            {
                var dto = GetPokemonDTO(pok);
                if (dto != null)
                    resultDTO.Add(dto);
            }
            return resultDTO;
        }

        private List<PokemonBaseDTO> GetPokemonBaseDTOList(List<Pokemon> pokemons)
        {
            // Convert list Pokemon to PokemonBaseDTO
            List<PokemonBaseDTO> resultDTO = new List<PokemonBaseDTO>();
            foreach (var pok in pokemons)
            {
                var dto = GetPokemonBaseDTO(pok);
                if (dto != null)
                    resultDTO.Add(dto);
            }
            return resultDTO;
        }
        #endregion
    }
}
