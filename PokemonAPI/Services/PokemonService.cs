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
        Task<List<PokemonType>> GetPokemonTypes();
        Task<PokemonTypeDTO> GetPokemonTypeDetail(string name);
    }

    public class PokemonService : IPokemonService
    {
        private readonly IMapper _mapper;
        private readonly IPokemonTypeRepository _typeRepository;
        public PokemonService(
            IMapper mapper, 
            IPokemonTypeRepository typeRepository)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
        }

        public async Task<List<PokemonType>> GetPokemonTypes()
        {
            var results = await _typeRepository.GetPokemonTypes();
            return results;
        }

        public async Task<PokemonTypeDTO> GetPokemonTypeDetail(string name)
        {
            PokemonType result = await _typeRepository.GetPokemonTypeDetail(name);
            PokemonTypeDTO resultDTO = _mapper.Map<PokemonTypeDTO>(result);
            for(int index = 0; index < result.TypeOffense.Count; index++)
            {
                resultDTO.TypeOffense[index] = _mapper.Map<TypeEffectOffenseDTO>(result.TypeOffense[index]);
                resultDTO.TypeOffense[index].Defend = result.TypeOffense[index].DefensePokemonType.Name; //offense -> defense
            }
            for(int index = 0; index < result.TypeDefense.Count; index++)
            {
                resultDTO.TypeDefense[index] = _mapper.Map<TypeEffectDefenseDTO>(result.TypeDefense[index]);
                resultDTO.TypeDefense[index].Attack = result.TypeDefense[index].OffensePokemonType.Name; //defense -> offense
            }

            return resultDTO;
        }
    }
}
