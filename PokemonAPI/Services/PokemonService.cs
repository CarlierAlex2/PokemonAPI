using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using PokemonAPI.Repositories;
using PokemonAPI.Models;
using PokemonAPI.Configuration;

using Microsoft.Extensions.Options;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        Task<List<PokemonType>> GetPokemonTypes();
        Task<PokemonType> GetPokemonTypeDetail(string name);
    }

    public class PokemonService : IPokemonService
    {
        private readonly IPokemonTypeRepository _typeRepository;
        public PokemonService(
            IPokemonTypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<List<PokemonType>> GetPokemonTypes()
        {
            var results = await _typeRepository.GetPokemonTypes();
            return results;
        }

        public async Task<PokemonType> GetPokemonTypeDetail(string name)
        {
            var result = await _typeRepository.GetPokemonTypeDetail(name);
            return result;
        }
    }
}
