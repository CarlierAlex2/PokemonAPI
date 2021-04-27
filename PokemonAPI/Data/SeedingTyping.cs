using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Configuration;

using AutoMapper;

using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;

namespace PokemonAPI.Data
{
    public class SeedingTyping
    {
        private readonly ModelBuilder _modelBuilder;
        private readonly CsvSettings _csvSettings;
        private readonly IMapper _mapper;

        public SeedingTyping(
            ModelBuilder modelBuilder, 
            CsvSettings csvSettings,
            IMapper mapper)
        {
            _modelBuilder = modelBuilder;
            _csvSettings = csvSettings;
            _mapper = mapper;
        }

        public List<Typing> Seeding()
        {
            var listTypings = ReadCSVTyping();
            SeedTypings(listTypings);

            var listEffects = ReadCSVTypeEffect();
            SeedTypeEffect(listTypings, listEffects);

            return listTypings;
        }

        private void SeedTypings(List<Typing> listTypings)
        {
            foreach (var Typing in listTypings)
            {
                _modelBuilder.Entity<Typing>().HasData(Typing);
            }
        }

        public void SeedTypeEffect(List<Typing> listTypings, List<TypeEffectDTO> listEffectDTO)
        {
            // Add effects --------------------------
            int id = 1;

            foreach (var typeEffectDTO in listEffectDTO)
            {
                TypeEffect typeEffect = _mapper.Map<TypeEffect>(typeEffectDTO);
                int offenseIndex = listTypings.FindIndex(t => t.Name == typeEffectDTO.Attack);
                int defenseIndex = listTypings.FindIndex(t => t.Name == typeEffectDTO.Defend);

                if(offenseIndex >= 0 && defenseIndex >= 0)
                {
                    typeEffect.TypeEffectId = id;
                    typeEffect.OffenseTypingId = listTypings[offenseIndex].TypingId;
                    typeEffect.DefenseTypingId = listTypings[defenseIndex].TypingId;
                    
                    _modelBuilder.Entity<TypeEffect>().HasData(typeEffect);
                }

                id++;
            }
        }

        private List<Typing> ReadCSVTyping()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var reader = new StreamReader(_csvSettings.CsvTyping))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Typing>();
                return records.ToList<Typing>();
            }   
        }

        private List<TypeEffectDTO> ReadCSVTypeEffect()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var reader = new StreamReader(_csvSettings.CsvTypeEffect))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<TypeEffectDTO>();
                return records.ToList<TypeEffectDTO>();
            }   
        }

        private void WriteToTypingCSV(List<Typing> listObjects)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var writer = new StreamWriter(_csvSettings.CsvTyping))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(listObjects);
            }
        }

        private void WriteToTypingEffectCSV(List<TypeEffectDTO> listObjects)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var writer = new StreamWriter(_csvSettings.CsvTypeEffect))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(listObjects);
            }
        }

        private List<Typing> CreateListTyping()
        {
            var listTypings = new List<Typing>();
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Fire" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Water" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Grass" });
            return listTypings;
        }

        public List<TypeEffectDTO> CreateEffectList()
        {
            var listEffects = new List<TypeEffectDTO>();
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
