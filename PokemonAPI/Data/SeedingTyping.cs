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
using Microsoft.Extensions.Options;
using CsvHelper.Configuration;
using System.Linq;

namespace PokemonAPI.Data
{
    public class SeedingTyping
    {
        public static List<Typing> Seeding(
            ModelBuilder modelBuilder, 
            IMapper mapper,
            CsvSettings csvSettings)
        {
            //var listTypings = CreateListTyping();
            //WriteToTypingCSV(listTypings, csvSettings);
            var listTypings = ReadCSVTyping(csvSettings);
            SeedTypings(modelBuilder, listTypings);

            //var listEffects = CreateEffectList();
            //WriteToTypingEffectCSV(listEffects, csvSettings);
            var listEffects = ReadCSVTypeEffect(csvSettings);
            SeedTypeEffect(modelBuilder, mapper, listTypings, listEffects);

            return listTypings;
        }

        private static List<Typing> ReadCSVTyping(CsvSettings csvSettings)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var reader = new StreamReader(csvSettings.CsvTyping))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Typing>();
                return records.ToList<Typing>();
            }   
        }

        private static List<TypeEffectDTO> ReadCSVTypeEffect(CsvSettings csvSettings)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var reader = new StreamReader(csvSettings.CsvTypeEffect))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<TypeEffectDTO>();
                return records.ToList<TypeEffectDTO>();
            }   
        }

        private static void WriteToTypingCSV(List<Typing> listObjects, CsvSettings csvSettings)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var writer = new StreamWriter(csvSettings.CsvTyping))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(listObjects);
            }
        }

        private static void WriteToTypingEffectCSV(List<TypeEffectDTO> listObjects, CsvSettings csvSettings)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var writer = new StreamWriter(csvSettings.CsvTypeEffect))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(listObjects);
            }
        }

        private static List<Typing> CreateListTyping()
        {
            var listTypings = new List<Typing>();

            #region Create Typing List
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Normal" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Fire" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Fighting" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Water" });

            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Flying" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Grass" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Poison" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Electric" });

            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Ground" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Psychic" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Rock" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Ice" });

            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Bug" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Dragon" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Ghost" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Dark" });

            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Steel" });
            listTypings.Add(new Typing() { TypingId = listTypings.Count + 1, Name = "Fairy" });
            #endregion

            return listTypings;
        }
        private static void SeedTypings(ModelBuilder modelBuilder, List<Typing> listTypings)
        {
            foreach (var Typing in listTypings)
            {
                modelBuilder.Entity<Typing>().HasData(Typing);
            }
        }

        public static List<TypeEffectDTO> CreateEffectList()
        {
            var listEffects = new List<TypeEffectDTO>();

            #region Create TypeEffect List
            
            #region Seeding Pokemon - Normal ---------------------------------------------
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Normal", Defend = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Normal", Defend = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Normal", Defend = "Ghost", Power=0 });
            #endregion

            #region Seeding Pokemon - Fire ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Ice", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Water", Power=2 });
            #endregion

            #region Seeding Pokemon - Fighting ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Normal", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Steel", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Bug", Power=0 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Psychic", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Ghost", Power=0 });
            #endregion

            #region Seeding Pokemon - Water ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Rock", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Water", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Flying ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Fighting", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Grass", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Steel", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Grass ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Water", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Steel", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Poison ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Grass", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Ghost", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Electric ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Water", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Ground", Power=0 });
            #endregion

            #region Seeding Pokemon - Ground ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Electric", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Poison", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Steel", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Flying", Power=0 });
            #endregion

            #region Seeding Pokemon - Psychic ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Fight", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Poison", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Psychic", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Dark", Power=0 });
            #endregion

            #region Seeding Pokemon - Rock ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Ice", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Steel", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Ice ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Ground", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Ice", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Water", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Bug ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Psychic", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Ghost", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Steel", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Dragon ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Dragon", Defend = "Dragon", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Dragon", Defend = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Dragon", Defend = "Fairy", Power=0 });
            #endregion

            #region Seeding Pokemon - Ghost ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Psychic", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Dark", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Normal", Power=0 });
            #endregion

            #region Seeding Pokemon - Dark ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Psychic", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Dark", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Fighting", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Steel ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Rock", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Water", Power=0.5m });
            #endregion

            #region Seeding Pokemon - Fairy ---------------------------------------------
            //Strong
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Fighting", Power=2 });
            //Weak
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Steel", Power=0.5m });
            #endregion

            #endregion

            //Return list --------------------------
            return listEffects;
        }

        public static void SeedTypeEffect(ModelBuilder modelBuilder, IMapper mapper, List<Typing> listTypings, List<TypeEffectDTO> listEffectDTO)
        {
            // Add effects --------------------------
            int id = 1;

            foreach (var typeEffectDTO in listEffectDTO)
            {
                TypeEffect typeEffect = mapper.Map<TypeEffect>(typeEffectDTO);
                int offenseIndex = listTypings.FindIndex(t => t.Name == typeEffectDTO.Attack);
                int defenseIndex = listTypings.FindIndex(t => t.Name == typeEffectDTO.Defend);

                if(offenseIndex >= 0 && defenseIndex >= 0)
                {
                    typeEffect.TypeEffectId = id;
                    typeEffect.OffenseTypingId = listTypings[offenseIndex].TypingId;
                    typeEffect.DefenseTypingId = listTypings[defenseIndex].TypingId;
                    
                    modelBuilder.Entity<TypeEffect>().HasData(typeEffect);
                }

                id++;
            }
        }
        /*
        public static void SeedTypeEffectV0(ModelBuilder modelBuilder, IMapper mapper, List<Typing> listTypings, List<TypeEffectDTO> listEffects)
        {
            // Add effects --------------------------
            int id = 1;

            foreach (var effect in listEffects)
            {
                int offenseIndex = listTypings.FindIndex(t => t.Name == effect.Attack);
                int defenseIndex = listTypings.FindIndex(t => t.Name == effect.Defend);
                decimal power = effect.Power;

                if(offenseIndex >= 0 && defenseIndex >= 0 && power >= 0)
                {
                    Typing offenseType = listTypings[offenseIndex];
                    Typing defenseType = listTypings[defenseIndex];
                    modelBuilder.Entity<TypeEffect>().HasData(
                        new TypeEffect(){
                        TypeEffectId = id,
                        OffenseTypingId = offenseType.TypingId, 
                        DefenseTypingId = defenseType.TypingId, 
                        Power = power}
                    );
                }

                id++;
            }
        }
        */
    }
}
