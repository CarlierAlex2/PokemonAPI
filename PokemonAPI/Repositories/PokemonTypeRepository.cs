using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.Data;
using System.Linq;

namespace PokemonAPI.Repositories
{
    public interface IPokemonTypeRepository
    {
        Task<List<PokemonType>> GetPokemonTypes();
        Task<PokemonType> GetPokemonTypeDetail(string name);
    }

    public class PokemonTypeRepository : IPokemonTypeRepository
    {
        private readonly IPokemonContext _context;
        public PokemonTypeRepository(IPokemonContext context)
        {
            _context = context;
        }

        public async Task<List<PokemonType>> GetPokemonTypes()
        {
            return await _context.PokemonTypes.ToListAsync();
        }

        public async Task<PokemonType> GetPokemonTypeById(int id)
        {
            return await _context.PokemonTypes
            .Where(pokemonType => pokemonType.PokemonTypeId == id)
            .SingleOrDefaultAsync();
        }

        public async Task<PokemonType> GetPokemonTypeDetail(string name)
        {
            return await _context.PokemonTypes
            .Where(pokemonType => pokemonType.Name == name)
            .Include(pokemonType => pokemonType.TypeEffects)
            .ThenInclude(typeEffect => typeEffect.TargetPokemonType)
            .SingleOrDefaultAsync();
        }
    }
}
