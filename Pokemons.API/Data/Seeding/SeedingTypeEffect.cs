using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream.CsvData;
using Pokemons.API.DTO;

namespace Pokemons.API.Data.Seeding
{
    public class SeedingTypeEffect : SeedingHelper
    {
        public List<CsvDataObject> _listTypingData {get; set;}
        public List<CsvDataObject> _listTypeEffectData {get; set;}

        public SeedingTypeEffect(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }

        //Seeding Functions -------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            if (_listTypingData == null || _listTypeEffectData == null)
                return;
            var listTypingData = _listTypingData.Cast<TypingData>().ToList();
            var listTypeEffectData = _listTypeEffectData.Cast<TypeEffectData>().ToList();

            // Add effects --------------------------
            var listTypeEffect = MappingHelper.ExtractTypeEffects(listTypingData, listTypeEffectData, _mapper);
            foreach (var effectObj in listTypeEffect)
            {
                _modelBuilder.Entity<TypeEffect>().HasData(effectObj);
            }
        }


        //Hardcoded Seeding -------------------------------------------------------------------------------------------
        public List<TypeEffectData> CreateSeedingData()
        {
            var listEffects = new List<TypeEffectData>();
            listEffects.Add(new TypeEffectData() { Attack = "Fire", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectData() { Attack = "Fire", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectData() { Attack = "Fire", Defend = "Water", Power=2 });

            listEffects.Add(new TypeEffectData() { Attack = "Water", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectData() { Attack = "Water", Defend = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectData() { Attack = "Water", Defend = "Water", Power=0.5m });

            listEffects.Add(new TypeEffectData() { Attack = "Grass", Defend = "Water", Power=2 });
            listEffects.Add(new TypeEffectData() { Attack = "Grass", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectData() { Attack = "Grass", Defend = "Grass", Power=0.5m });
            return listEffects;
        }
    }
}
