using System;
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
        Task<Pokemon> GetPokemonById(int id);
        Task<List<Pokemon>> GetPokemonByType(string typeName);
        Task<List<Pokemon>> GetPokemons();
        Task<TypingDTO> GetTypingDetail(string name);
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

        public async Task<TypingDTO> GetTypingDetail(string name)
        {
            Typing result = await _typeRepository.GetTypingDetail(name);
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
        public async Task<List<Pokemon>> GetPokemons()
        {
            var results = await _pokemonRepository.GetPokemons();
            return results;
        }

        public async Task<Pokemon> GetPokemonById(int id)
        {
            var results = await _pokemonRepository.GetPokemonById(id);
            return results;
        }

        public async Task<List<Pokemon>> GetPokemonByType(string typeName)
        {
            var results = await _pokemonRepository.GetPokemonByType(typeName);
            return results;
        }
    }
}
