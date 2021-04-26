using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.Data;
using System.Linq;

namespace PokemonAPI.Repositories
{
    public enum DetailLevel
    {
        Basic = 0,
        Details = 1,
        ForeignKeys = 2
    }
    public interface IPokemonRepository
    {
        Task<Pokemon> AddPokemon(Pokemon pokemon);
        Task DeletePokemon(Pokemon pokemon);
        Task DeletePokemonList(List<Pokemon> listPokemon);
        Task<List<Pokemon>> GetPokemonByEntry(int pokedexEntry, DetailLevel detailLevel = DetailLevel.Basic);
        Task<Pokemon> GetPokemonByEntryAndGen(int pokedexEntry, int generation, DetailLevel detailLevel = DetailLevel.Basic);
        Task<Pokemon> GetPokemonById(Guid id);
        Task<List<Pokemon>> GetPokemonByType(string typeName);
        Task<List<Pokemon>> GetPokemons();
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
            .OrderBy(t => t.PokedexEntry)
            .ToListAsync();
        }

        public async Task<Pokemon> GetPokemonById(Guid id)
        {
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokemonId == id)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .FirstOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemonByEntry(int pokedexEntry, DetailLevel detailLevel = 0)
        {
            if (detailLevel == DetailLevel.Details)
            {
                return await _context.Pokemons
                .Where(pokemon => pokemon.PokedexEntry == pokedexEntry)
                .Include(pokemon => pokemon.PokemonTypings)
                .ThenInclude(pokTyping => pokTyping.Typing)
                .ToListAsync();
            }
            else if (detailLevel == DetailLevel.ForeignKeys)
            {
                return await _context.Pokemons
                .Where(pokemon => pokemon.PokedexEntry == pokedexEntry)
                .Include(pokemon => pokemon.PokemonTypings)
                .ToListAsync();
            }
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokedexEntry == pokedexEntry)
            .ToListAsync();
        }

        public async Task<Pokemon> GetPokemonByEntryAndGen(int pokedexEntry, int generation, DetailLevel detailLevel = 0)
        {
            if (detailLevel == DetailLevel.Details)
            {
                return await _context.Pokemons
                .Where(pokemon => pokemon.PokedexEntry == pokedexEntry && pokemon.Generation == generation)
                .Include(pokemon => pokemon.PokemonTypings)
                .ThenInclude(pokTyping => pokTyping.Typing)
                .SingleOrDefaultAsync();
            }
            else if (detailLevel == DetailLevel.ForeignKeys)
            {
                return await _context.Pokemons
                .Where(pokemon => pokemon.PokedexEntry == pokedexEntry && pokemon.Generation == generation)
                .Include(pokemon => pokemon.PokemonTypings)
                .SingleOrDefaultAsync();
            }
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokedexEntry == pokedexEntry && pokemon.Generation == generation)
            .SingleOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemonByType(string typeName)
        {
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .Where(p => p.PokemonTypings.Any(t => t.Typing.Name == typeName))
            .OrderBy(t => t.PokedexEntry)
            .ToListAsync();
        }

        public async Task<Pokemon> AddPokemon(Pokemon pokemon)
        {
            await _context.Pokemons.AddAsync(pokemon);
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task DeletePokemon(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePokemonList(List<Pokemon> listPokemon)
        {
            _context.Pokemons.RemoveRange(listPokemon);
            await _context.SaveChangesAsync();
        }
    }
}
