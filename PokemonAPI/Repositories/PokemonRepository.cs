using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.Data;
using System.Linq;

namespace PokemonAPI.Repositories
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetPokemons();
        Task<Pokemon> GetPokemonById(int id);
        Task<List<Pokemon>> GetPokemonByType(string typeName);
    }

    public class PokemonRepository : IPokemonRepository
    {
        private readonly IPokemonContext _context;
        public PokemonRepository(IPokemonContext context)
        {
            _context = context;
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .ToListAsync();
        }

        public async Task<Pokemon> GetPokemonById(int id)
        {
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokemonId == id)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .FirstOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemonByType(string typeName)
        {
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .ToListAsync();
        }
    }
}
