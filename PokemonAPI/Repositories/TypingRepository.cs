using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.Data;
using System.Linq;

namespace PokemonAPI.Repositories
{
    public interface ITypingRepository
    {
        Task<Typing> GetTypingById(int id);
        Task<Typing> GetTypingDetail(string name);
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

        public async Task<Typing> GetTypingDetail(string name)
        {
            return await _context.Typings
            .Where(Typing => Typing.Name == name)
            .Include(Typing => Typing.TypeOffense
                .OrderByDescending(t => t.Power)
                )
            .ThenInclude(typeEffect => typeEffect.DefenseTyping)
            .Include(Typing => Typing.TypeDefense
                .OrderByDescending(t => t.Power)
                )
            .ThenInclude(typeEffect => typeEffect.OffenseTyping)
            .FirstOrDefaultAsync();
        }
    }
}
