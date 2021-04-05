using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.Data;

namespace PokemonAPI.Repositories
{
    public interface IPokemonTypeRepository
    {
        Task<List<PokemonType>> GetPokemonTypes();
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
    }
}
