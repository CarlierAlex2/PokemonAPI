using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Pokemons.API.Models;
using Pokemons.API.Data;

namespace Pokemons.API.Repositories
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
        Task DeletePokemon_List(List<Pokemon> listPokemon);
        Task<List<Pokemon>> GetPokemons_ByEntry(int pokedexEntry, DetailLevel detailLevel = DetailLevel.Basic);
        Task<Pokemon> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation, DetailLevel detailLevel = DetailLevel.Basic);
        Task<Pokemon> GetPokemon_ById(Guid id);
        Task<Pokemon> GetPokemon_ByName(string name);
        Task<List<Pokemon>> GetPokemons_ByType(string typeName);
        Task<List<string>> GetPokemons_List();
        Task<List<string>> GetPokemons_List_ByType(string typeName);
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

        public async Task<List<string>> GetPokemons_List()
        {
            return await _context.Pokemons
            .Select(t => t.Name).Distinct().ToListAsync();
        }

        public async Task<List<Pokemon>> GetPokemons_ByType(string typeName)
        {
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .Where(p => p.PokemonTypings.Any(t => t.Typing.Name == typeName))
            .OrderBy(t => t.PokedexEntry)
            .ToListAsync();
        }

        public async Task<List<string>> GetPokemons_List_ByType(string typeName)
        {
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .Where(p => p.PokemonTypings.Any(t => t.Typing.Name == typeName))
            .OrderBy(t => t.PokedexEntry)
            .Select(pokemon => pokemon.Name)
            .ToListAsync();
        }

        public async Task<Pokemon> GetPokemon_ById(Guid id)
        {
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokemonId == id)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .FirstOrDefaultAsync();
        }

        public async Task<Pokemon> GetPokemon_ByName(string name)
        {
            return await _context.Pokemons
            .Where(pokemon => pokemon.Name == name)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .FirstOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemons_ByEntry(int pokedexEntry, DetailLevel detailLevel = 0)
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

        public async Task<Pokemon> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation, DetailLevel detailLevel = 0)
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

        public async Task DeletePokemon_List(List<Pokemon> listPokemon)
        {
            _context.Pokemons.RemoveRange(listPokemon);
            await _context.SaveChangesAsync();
        }
    }
}
