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
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        public List<CsvDataObject> _listTypingData {get; set;}


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public SeedingTyping(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }


        // Seeding Functions //-------------------------------------------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            // Check if we got provided with necessary data
            if (_listTypingData == null)
                return;

            // Convert CSV data to corresponding object
            var listTypingData = _listTypingData.Cast<TypingData>().ToList();

            // Do seeding
            var listTyping = _mapper.Map<List<Typing>>(listTypingData);
            foreach (var typing in listTyping)
                _modelBuilder.Entity<Typing>().HasData(typing);
        }

        // Hardcoded Seeding //-------------------------------------------------------------------------------------------------------------------------------
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
