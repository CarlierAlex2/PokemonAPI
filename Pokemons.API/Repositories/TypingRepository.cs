using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Pokemons.API.Models;
using Pokemons.API.Data;


namespace Pokemons.API.Repositories
{
    public interface ITypingRepository
    {
        Task<Typing> GetTyping_ById(int id);
        Task<Typing> GetTyping_ByName(string name, bool isDetailed = false);
        Task<List<string>> GetTypings_List();
        Task<List<Typing>> GetTypings();
    }


    public class TypingRepository : ITypingRepository
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        private readonly IPokemonContext _context;


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public TypingRepository(IPokemonContext context)
        {
            _context = context;
        }


        #region // Get Typing - group //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<List<Typing>> GetTypings()
        {
            // Return list of Typing
            return await _context.Typings.ToListAsync();
        }

        public async Task<List<string>> GetTypings_List()
        {
            // Return list of Typing names
            return await _context.Typings.Select(t => t.Name).Distinct().ToListAsync();
        }
        #endregion



        #region // Get Typing - specific //-------------------------------------------------------------------------------------------------------------------------------
        public async Task<Typing> GetTyping_ById(int id)
        {
            // Return a Typing, filter by id
            return await _context.Typings.Where(Typing => Typing.TypingId == id).FirstOrDefaultAsync();
        }

        public async Task<Typing> GetTyping_ByName(string name, bool isDetailed = false)
        {
            // Return a Typing, filter by name, with details (TypeEffects)
            if (isDetailed == true)
                return await GetTyping_ByName_Detailed(name);

            // Return a Typing, filter by name
            return await GetTyping_ByName_Simple(name);
        }

        private async Task<Typing> GetTyping_ByName_Simple(string name)
        {
            // Return a Typing, filter by name
            return await _context.Typings.Where(Typing => Typing.Name == name).FirstOrDefaultAsync();
        }

        private async Task<Typing> GetTyping_ByName_Detailed(string name)
        {
            // Return a Typing, filter by name, with details (TypeEffects)
            return await _context.Typings
            .Where(Typing => Typing.Name == name)

            .Include(Typing => Typing.TypeOffense.OrderByDescending(t => t.Power)) //include TypeOffense
            .ThenInclude(typeEffect => typeEffect.DefenseTyping)

            .Include(Typing => Typing.TypeDefense.OrderByDescending(t => t.Power)) //include TypeDefense
            .ThenInclude(typeEffect => typeEffect.OffenseTyping)

            .FirstOrDefaultAsync();
        }
        #endregion
    }
}
