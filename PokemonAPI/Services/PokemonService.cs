using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using PokemonAPI.Repositories;
using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Configuration;

using AutoMapper;
using Microsoft.Extensions.Options;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        Task<Pokemon> AddPokemon(PokemonDTO pokemonDTO);
        Task DeletePokemonByEntry(int pokedexEntry);
        Task DeletePokemonByEntryAndGen(int pokedexEntry, int generation);
        Task<List<PokemonDTO>> GetPokemonByEntry(int pokedexEntry);
        Task<PokemonDTO> GetPokemonByEntryAndGen(int pokedexEntry, int generation);
        Task<PokemonDTO> GetPokemonById(Guid id);
        Task<List<PokemonDTO>> GetPokemonByType(string typeName);
        Task<List<PokemonDTO>> GetPokemons();
        Task<TypingDTO> GetTypingByName(string name);
        Task<List<Typing>> GetTypings();
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

        //Pokemon Type -------------------------------------------------------------------------------------------
        public async Task<List<Typing>> GetTypings()
        {
            var results = await _typeRepository.GetTypings();
            return results;
        }

        public async Task<TypingDTO> GetTypingByName(string name)
        {
            Typing result = await _typeRepository.GetTypingByName(name, true);
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

        //Pokemon -------------------------------------------------------------------------------------------
        public async Task<List<PokemonDTO>> GetPokemons()
        {
            var results = await _pokemonRepository.GetPokemons();
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = GetPokemonDTOList(results);
            return resultDTO;
        }

        public async Task<PokemonDTO> GetPokemonById(Guid id)
        {
            var results = await _pokemonRepository.GetPokemonById(id);
            if (results == null)
                return null;

            PokemonDTO resultDTO = GetPokemonDTO(results);
            return resultDTO;
        }

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

        public async Task<List<PokemonDTO>> GetPokemonByType(string typeName)
        {
            var results = await _pokemonRepository.GetPokemonByType(typeName);
            if (results == null || results.Count <= 0)
                return null;

            List<PokemonDTO> resultDTO = GetPokemonDTOList(results);
            return resultDTO;
        }

        private PokemonDTO GetPokemonDTO(Pokemon pokemon)
        {
            var dto = _mapper.Map<PokemonDTO>(pokemon);
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
                if(dto != null)
                    resultDTO.Add(dto);
            }
            return resultDTO;
        }

        public async Task<Pokemon> AddPokemon(PokemonDTO pokemonDTO)
        {
            PokemonDTO checkExists = await GetPokemonByEntryAndGen(pokemonDTO.PokedexEntry, pokemonDTO.Generation);
            if(checkExists != null)
                return null;

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
    }
}
