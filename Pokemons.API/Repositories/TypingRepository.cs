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
        Task<Typing> GetTypingById(int id);
        Task<Typing> GetTypingByName(string name, bool isDetailed = false);
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

        public async Task<Typing> GetTypingById(int id)
        {
            return await _context.Typings
            .Where(Typing => Typing.TypingId == id)
            .FirstOrDefaultAsync();
        }

        public async Task<Typing> GetTypingByName(string name, bool isDetailed = false)
        {
            if (isDetailed == true)
                return await GetTypingByName_Detailed(name);

            return await GetTypingByName_Simple(name);
        }

        private async Task<Typing> GetTypingByName_Simple(string name)
        {
            return await _context.Typings
            .Where(Typing => Typing.Name == name)
            .FirstOrDefaultAsync();
        }

        private async Task<Typing> GetTypingByName_Detailed(string name)
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
