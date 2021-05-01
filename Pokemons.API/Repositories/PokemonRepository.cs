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
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        private readonly IPokemonContext _context;
        
        
        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public PokemonRepository(IPokemonContext context)
        {
            _context = context;
        }



        #region // Get functions - Group //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<List<Pokemon>> GetPokemons()
        {
            // Return a list of Pokemon with details
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .OrderBy(t => t.PokedexEntry).ToListAsync();
        }

        public async Task<List<Pokemon>> GetPokemons_ByType(string typeName)
        {
            // Return a list of Pokemon with details, filtered by Typing name
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .Where(p => p.PokemonTypings.Any(t => t.Typing.Name == typeName))
            .OrderBy(t => t.PokedexEntry).ToListAsync();
        }

        public async Task<List<string>> GetPokemons_List()
        {
            // Return a list of Pokemon names
            return await _context.Pokemons
            .Select(t => t.Name).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetPokemons_List_ByType(string typeName)
        {
            // Return a list of Pokemon names, filtered by Typing name
            return await _context.Pokemons
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .Where(p => p.PokemonTypings.Any(t => t.Typing.Name == typeName))
            .OrderBy(t => t.PokedexEntry).Select(pokemon => pokemon.Name).ToListAsync();
        }
        #endregion



        #region // Get Functions - Specific //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<Pokemon> GetPokemon_ById(Guid id)
        {
            // Return a Pokemon that matches the given id
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokemonId == id)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .FirstOrDefaultAsync();
        }

        public async Task<Pokemon> GetPokemon_ByName(string name)
        {
            // Return a Pokemon that matches the given name
            return await _context.Pokemons
            .Where(pokemon => pokemon.Name == name)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .FirstOrDefaultAsync();
        }
        #endregion



        #region // Get Functions - By PokedexEntry //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<List<Pokemon>> GetPokemons_ByEntry(int pokedexEntry, DetailLevel detailLevel = 0)
        {
            // Return Pokemon, filtered by entry (cause variants)

            if (detailLevel == DetailLevel.Details) // including Typing for details
                return await GetPokemons_ByEntry_Detailed(pokedexEntry);

            else if (detailLevel == DetailLevel.ForeignKeys) // including only PokemonTyping (for)
                return await GetPokemons_ByEntry_ForeignKeys(pokedexEntry);

            return await GetPokemons_ByEntry_Simple(pokedexEntry); // no inclusions
        }

        private async Task<List<Pokemon>> GetPokemons_ByEntry_Detailed(int pokedexEntry)
        {
            // Return Pokemon, filtered by entry (cause variants), including Typing for details
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokedexEntry == pokedexEntry)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .ToListAsync();
        }

        private async Task<List<Pokemon>> GetPokemons_ByEntry_ForeignKeys(int pokedexEntry)
        {
            // Return Pokemon, filtered by entry (cause variants), including only PokemonTyping (for)
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokedexEntry == pokedexEntry)
            .Include(pokemon => pokemon.PokemonTypings)
            .ToListAsync();
        }

        private async Task<List<Pokemon>> GetPokemons_ByEntry_Simple(int pokedexEntry)
        {
            // Return only Pokemon, filtered by entry (cause variants), no inclusions
            return await _context.Pokemons.Where(pokemon => pokemon.PokedexEntry == pokedexEntry).ToListAsync();
        }
        #endregion



        #region // Get Functions - By PokedexEntry and Generation //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<Pokemon> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation, DetailLevel detailLevel = 0)
        {
            // Return Pokemon, filtered by entry and gen
            
            if (detailLevel == DetailLevel.Details)
                return await GetPokemon_ByEntryAndGen_Detailed(pokedexEntry, generation); // including Typing for details

            else if (detailLevel == DetailLevel.ForeignKeys)
                return await GetPokemon_ByEntryAndGen_ForeignKeys(pokedexEntry, generation); // including only PokemonTyping (for)

            return await GetPokemon_ByEntryAndGen_Simple(pokedexEntry, generation); // no inclusions
        }

        private async Task<Pokemon> GetPokemon_ByEntryAndGen_Detailed(int pokedexEntry, int generation)
        {
            // Return Pokemon, filtered by entry and gen, including Typing for details
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokedexEntry == pokedexEntry && pokemon.Generation == generation)
            .Include(pokemon => pokemon.PokemonTypings)
            .ThenInclude(pokTyping => pokTyping.Typing)
            .SingleOrDefaultAsync();
        }

        private async Task<Pokemon> GetPokemon_ByEntryAndGen_ForeignKeys(int pokedexEntry, int generation)
        {
            // Return Pokemon, filtered by entry and gen, including only PokemonTyping (for)
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokedexEntry == pokedexEntry && pokemon.Generation == generation)
            .Include(pokemon => pokemon.PokemonTypings)
            .SingleOrDefaultAsync();
        }

        private async Task<Pokemon> GetPokemon_ByEntryAndGen_Simple(int pokedexEntry, int generation)
        {
            // Return only Pokemon, filtered by entry and gen, no inclusions
            return await _context.Pokemons
            .Where(pokemon => pokemon.PokedexEntry == pokedexEntry && pokemon.Generation == generation)
            .SingleOrDefaultAsync();
        }
        #endregion



        #region // Add functions //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<Pokemon> AddPokemon(Pokemon pokemon)
        {
            // Add a Pokemon
            await _context.Pokemons.AddAsync(pokemon);
            await _context.SaveChangesAsync();
            return pokemon;
        }
        #endregion



        #region // Delete functions //-------------------------------------------------------------------------------------------------------------------------------
        public async Task DeletePokemon(Pokemon pokemon)
        {
            // Delete a Pokemon
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePokemon_List(List<Pokemon> listPokemon)
        {
            // Delete a list of pokemon
            _context.Pokemons.RemoveRange(listPokemon);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
