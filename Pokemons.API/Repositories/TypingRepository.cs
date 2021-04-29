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
        private readonly IPokemonContext _context;
        public TypingRepository(IPokemonContext context)
        {
            _context = context;
        }

        public async Task<List<Typing>> GetTypings()
        {
            return await _context.Typings.ToListAsync();
        }

        public async Task<List<string>> GetTypings_List()
        {
            return await _context.Typings.Select(t => t.Name).Distinct().ToListAsync();
        }

        public async Task<Typing> GetTyping_ById(int id)
        {
            return await _context.Typings
            .Where(Typing => Typing.TypingId == id)
            .FirstOrDefaultAsync();
        }

        public async Task<Typing> GetTyping_ByName(string name, bool isDetailed = false)
        {
            if (isDetailed == true)
                return await GetTyping_ByName_Detailed(name);

            return await GetTyping_ByName_Simple(name);
        }

        private async Task<Typing> GetTyping_ByName_Simple(string name)
        {
            return await _context.Typings
            .Where(Typing => Typing.Name == name)
            .FirstOrDefaultAsync();
        }

        private async Task<Typing> GetTyping_ByName_Detailed(string name)
        {
            return await _context.Typings
            .Where(Typing => Typing.Name == name)
            .Include(Typing => Typing.TypeOffense //include TypeOffense
                .OrderByDescending(t => t.Power)
                )
            .ThenInclude(typeEffect => typeEffect.DefenseTyping)
            .Include(Typing => Typing.TypeDefense //include TypeDefense
                .OrderByDescending(t => t.Power)
                )
            .ThenInclude(typeEffect => typeEffect.OffenseTyping)
            .FirstOrDefaultAsync();
        }
    }
}
