using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream.CsvData;

namespace Pokemons.API.Data.Seeding
{
    public class SeedingTyping : SeedingHelper
    {
        public List<CsvDataObject> _listTypingData {get; set;}
        public SeedingTyping(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }

        //Seeding Functions -------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            if (_listTypingData == null)
                return;
            var listTypingData = _listTypingData.Cast<TypingData>().ToList();

            foreach (var dataObj in listTypingData)
            {
                var typing = _mapper.Map<Typing>(dataObj);
                _modelBuilder.Entity<Typing>().HasData(typing);
            }
        }

        //Hardcoded Seeding -------------------------------------------------------------------------------------------
        private List<TypingData> CreateSeedingData()
        {
            var listTypings = new List<TypingData>();
            listTypings.Add(new TypingData() { TypingId = listTypings.Count + 1, Name = "Fire" });
            listTypings.Add(new TypingData() { TypingId = listTypings.Count + 1, Name = "Water" });
            listTypings.Add(new TypingData() { TypingId = listTypings.Count + 1, Name = "Grass" });
            return listTypings;
        }
    }
}
