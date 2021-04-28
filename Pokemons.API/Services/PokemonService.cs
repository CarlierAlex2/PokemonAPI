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
        Task DeletePokemonByEntry(int pokedexEntry);
        Task DeletePokemonByEntryAndGen(int pokedexEntry, int generation);
        Task<List<PokemonDTO>> GetPokemonByEntry(int pokedexEntry);
        Task<PokemonDTO> GetPokemonByEntryAndGen(int pokedexEntry, int generation);
        Task<List<PokemonDTO>> GetPokemonByType_V1(string typeName);
        Task<List<PokemonBaseDTO>> GetPokemonByType_V2(string typeName);
        Task<PokemonList> GetPokemonList();
        Task<PokemonList> GetPokemonListByType(string typeName);
        Task<PokemonStatisticsList> GetPokemonStatistics(List<string> names);
        Task<List<PokemonDTO>> GetPokemons_V1();
        Task<List<PokemonBaseDTO>> GetPokemons_V2();
        Task<TypingDTO> GetTypingByName(string name);
        Task<TypingList> GetTypingList();
        Task<List<Typing>> GetTypings_V1();
        Task<List<TypingBaseDTO>> GetTypings_V2();
    }

    public class PokemonService : IPokemonService
    {
        private readonly IMapper _mapper;
        private readonly ITypingRepository _typeRepository;
        private readonly IPokemonRepository _pokemonRepository;
        public PokemonService(
            IMapper mapper,
            ITypingRepository typeRepository,
            IPokemonRepository pokemonRepository)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
            _pokemonRepository = pokemonRepository;
        }


        //------------------------------------------------------------------------------------------------------------------------
        public async Task<List<Typing>> GetTypings_V1()
        {
            var results = await _typeRepository.GetTypings();
            if (results.Count <= 0)
                return null;

            return results;
        }

        public async Task<List<TypingBaseDTO>> GetTypings_V2()
        {
            var results = await _typeRepository.GetTypings();
            if (results.Count <= 0)
                return null;

            return GetTypingBaseDTOList(results);
        }

        public async Task<TypingList> GetTypingList()
        {
            var results = await _typeRepository.GetTypingList();
            if (results.Count <= 0)
                return null;

            var listObj = new TypingList() { Names = results };
            return listObj;
        }


        //------------------------------------------------------------------------------------------------------------------------
        public async Task<TypingDTO> GetTypingByName(string name)
        {
            Typing result = await _typeRepository.GetTypingByName(name, true);
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

        //------------------------------------------------------------------------------------------------------------------------
        public async Task<List<PokemonDTO>> GetPokemons_V1()
        {
            var results = await _pokemonRepository.GetPokemons();
            if (results == null || results.Count <= 0)
                return null;

            var resultDTO = GetPokemonDTOList(results);
            return resultDTO;
        }

        public async Task<List<PokemonBaseDTO>> GetPokemons_V2()
        {
            var results = await _pokemonRepository.GetPokemons();
            if (results == null || results.Count <= 0)
                return null;

            var resultDTO = GetPokemonBaseDTOList(results);
            return resultDTO;
        }

        public async Task<PokemonList> GetPokemonList()
        {
            var results = await _pokemonRepository.GetPokemonList();
            if (results == null || results.Count <= 0)
                return null;

            var listObj = new PokemonList() { Names = results };
            return listObj;
        }


        //------------------------------------------------------------------------------------------------------------------------
        public async Task<List<PokemonDTO>> GetPokemonByType_V1(string typeName)
        {
            var results = await _pokemonRepository.GetPokemonByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            var resultDTO = GetPokemonDTOList(results);
            return resultDTO;
        }

        public async Task<List<PokemonBaseDTO>> GetPokemonByType_V2(string typeName)
        {
            var results = await _pokemonRepository.GetPokemonByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            var resultDTO = GetPokemonBaseDTOList(results);
            return resultDTO;
        }

        public async Task<PokemonList> GetPokemonListByType(string typeName)
        {
            var results = await _pokemonRepository.GetPokemonListByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            var listObj = new PokemonList() { Names = results };
            return listObj;
        }


        //------------------------------------------------------------------------------------------------------------------------
        public async Task<List<PokemonDTO>> GetPokemonByEntry(int pokedexEntry)
        {
            var results = await _pokemonRepository.GetPokemonByEntry(pokedexEntry, DetailLevel.Details);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = GetPokemonDTOList(results);
            return resultDTO;
        }

        public async Task<PokemonDTO> GetPokemonByEntryAndGen(int pokedexEntry, int generation)
        {
            var results = await _pokemonRepository.GetPokemonByEntryAndGen(pokedexEntry, generation, DetailLevel.Details);
            if (results == null)
                return null;

            PokemonDTO resultDTO = GetPokemonDTO(results);
            return resultDTO;
        }


        //------------------------------------------------------------------------------------------------------------------------
        public async Task<PokemonStatisticsList> GetPokemonStatistics(List<string> names)
        {
            List<Pokemon> results = new List<Pokemon>();
            foreach (var name in names)
            {
                var result = await _pokemonRepository.GetPokemonByName(name);
                if (result != null)
                    results.Add(result);
            }

            if (results.Count <= 0)
                return null;
            var statObj = PokemonCalculator.GetPokemonStatistics(results);
            return statObj;
        }


        //------------------------------------------------------------------------------------------------------------------------
        public async Task<Pokemon> AddPokemon(PokemonDTO pokemonDTO)
        {
            PokemonDTO checkExists = await GetPokemonByEntryAndGen(pokemonDTO.PokedexEntry, pokemonDTO.Generation);
            if (checkExists != null)
                return null;

            pokemonDTO.EggGroup = pokemonDTO.EggGroup.Replace(" ", "");
            Pokemon pokemon = _mapper.Map<Pokemon>(pokemonDTO);
            pokemon.PokemonId = Guid.NewGuid();
            pokemon.PokemonTypings = new List<PokemonTyping>();
            foreach (var typingName in pokemonDTO.Types)
            {
                var typing = await _typeRepository.GetTypingByName(typingName);
                pokemon.PokemonTypings.Add(new PokemonTyping()
                {
                    PokemonId = pokemon.PokemonId,
                    TypingId = typing.TypingId
                });
            }
            pokemon = await _pokemonRepository.AddPokemon(pokemon);
            return pokemon;
        }


        //------------------------------------------------------------------------------------------------------------------------
        public async Task DeletePokemonByEntry(int pokedexEntry)
        {
            List<Pokemon> listPokemon = await _pokemonRepository.GetPokemonByEntry(pokedexEntry, DetailLevel.ForeignKeys);
            if (listPokemon == null)
                return;

            await _pokemonRepository.DeletePokemonList(listPokemon);
        }

        public async Task DeletePokemonByEntryAndGen(int pokedexEntry, int generation)
        {
            Pokemon pokemon = await _pokemonRepository.GetPokemonByEntryAndGen(pokedexEntry, generation, DetailLevel.ForeignKeys);
            if (pokemon == null)
                return;

            await _pokemonRepository.DeletePokemon(pokemon);
        }


        //------------------------------------------------------------------------------------------------------------------------
        private List<TypingBaseDTO> GetTypingBaseDTOList(List<Typing> listTyping)
        {
            var dto = _mapper.Map<List<TypingBaseDTO>>(listTyping);
            return dto;
        }

        private PokemonDTO GetPokemonDTO(Pokemon pokemon)
        {
            var dto = _mapper.Map<PokemonDTO>(pokemon);
            if (pokemon.PokemonTypings != null)
                dto.Types = pokemon.PokemonTypings.Select(r => r.Typing.Name).ToList();

            return dto;
        }

        private PokemonBaseDTO GetPokemonBaseDTO(Pokemon pokemon)
        {
            var dto = _mapper.Map<PokemonBaseDTO>(pokemon);
            if (pokemon.PokemonTypings != null)
                dto.Types = pokemon.PokemonTypings.Select(r => r.Typing.Name).ToList();

            return dto;
        }

        private List<PokemonDTO> GetPokemonDTOList(List<Pokemon> pokemons)
        {
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
            List<PokemonBaseDTO> resultDTO = new List<PokemonBaseDTO>();
            foreach (var pok in pokemons)
            {
                var dto = GetPokemonBaseDTO(pok);
                if (dto != null)
                    resultDTO.Add(dto);
            }
            return resultDTO;
        }
    }
}
