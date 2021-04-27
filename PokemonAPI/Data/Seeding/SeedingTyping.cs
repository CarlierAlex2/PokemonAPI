using System;
using System.Collections.Generic;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Helpers;

namespace PokemonAPI.Data.Seeding
{
    public class SeedingTyping : SeedingHelper
    {
        public List<Typing> _listTypings {get; set;}
        public SeedingTyping(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }

        //Seeding Functions -------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            if (_listTypings == null)
                return;

            foreach (var typing in _listTypings)
            {
                _modelBuilder.Entity<Typing>().HasData(typing);
            }
        }

        //Hardcoded Seeding -------------------------------------------------------------------------------------------
        private List<ModelObject> CreateListTyping()
        {
            var listTypings = new List<ModelObject>();
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Fire" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Water" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Grass" });
            return listTypings;
        }
    }
}
