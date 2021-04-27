using System;
using System.Collections.Generic;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Helpers;


namespace PokemonAPI.Data.Seeding
{
    public class SeedingTypeEffect : SeedingHelper
    {
        public List<Typing> _listTypings {get; set;}
        public List<TypeEffectDTO> _listEffectDTO {get; set;}

        public SeedingTypeEffect(ModelBuilder modelBuilder, IMapper mapper) : base(modelBuilder, mapper)
        {
            
        }

        //Seeding Functions -------------------------------------------------------------------------------------------
        protected override void DoSeeding()
        {
            if (_listTypings == null || _listEffectDTO == null)
                return;

            // Add effects --------------------------
            int id = 1;

            foreach (var typeEffectDTO in _listEffectDTO)
            {
                TypeEffect typeEffect = _mapper.Map<TypeEffect>(typeEffectDTO);
                int offenseIndex = _listTypings.FindIndex(t => t.Name == typeEffectDTO.Attack);
                int defenseIndex = _listTypings.FindIndex(t => t.Name == typeEffectDTO.Defend);

                if(offenseIndex >= 0 && defenseIndex >= 0)
                {
                    typeEffect.TypeEffectId = id;
                    typeEffect.OffenseTypingId = _listTypings[offenseIndex].TypingId;
                    typeEffect.DefenseTypingId = _listTypings[defenseIndex].TypingId;
                    
                    _modelBuilder.Entity<TypeEffect>().HasData(typeEffect);
                }

                id++;
            }
        }


        //Hardcoded Seeding -------------------------------------------------------------------------------------------
        public List<ModelObject> CreateEffectList()
        {
            var listEffects = new List<ModelObject>();
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Water", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Water", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Water", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Grass", Power=0.5m });
            return listEffects;
        }
    }
}
