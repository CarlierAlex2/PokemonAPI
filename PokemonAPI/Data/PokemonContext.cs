using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;

using PokemonAPI.Models;
using PokemonAPI.Configuration;
using System.Collections.Generic;

namespace PokemonAPI.Data
{
    public interface IPokemonContext
    {
        DbSet<PokemonType> PokemonTypes { get; set; }
        DbSet<TypeEffect> TypeEffects { get; set; }
    }

    public class PokemonContext : DbContext, IPokemonContext
    {
        public DbSet<PokemonType> PokemonTypes { get; set; }
        public DbSet<TypeEffect> TypeEffects { get; set; }
        private readonly ConnectionStrings _connectionStrings;

        public PokemonContext(DbContextOptions<PokemonContext> options, IOptions<ConnectionStrings> connectionstrings) : base(options)
        {
            _connectionStrings = connectionstrings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            options.UseSqlServer(_connectionStrings.SQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeEffect>().HasKey(cs => new { cs.UserPokemonTypeId, cs.TargetPokemonTypeId });

            List<PokemonType> listPokemonType = SeedPokemonTypes(modelBuilder);
            SeedTypeEffect(modelBuilder, listPokemonType);
        }

        private List<PokemonType> SeedPokemonTypes(ModelBuilder modelBuilder)
        {
            var listPokemonType = new List<PokemonType>();
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Normal" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Fire" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Fighting" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Water" });

            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Flying" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Grass" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Poison" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Electric" });

            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Ground" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Psychic" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Rock" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Ice" });

            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Bug" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Dragon" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Ghost" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Dark" });

            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Steel" });
            listPokemonType.Add(new PokemonType() { PokemonTypeId = listPokemonType.Count + 1, Name = "Fairy" });

            foreach (var pokemonType in listPokemonType)
            {
                modelBuilder.Entity<PokemonType>().HasData(pokemonType);
            }

            return listPokemonType;
        }

        private void SeedTypeEffect(ModelBuilder modelBuilder, List<PokemonType> listPokemonType)
        {
            // Add effects --------------------------
            foreach (var effect in CreateEffectList())
            {
                int userIndex = listPokemonType.FindIndex(t => t.Name == effect.UserPokemonType);
                int targetIndex = listPokemonType.FindIndex(t => t.Name == effect.TargetPokemonType);
                decimal power = effect.Power;

                if(userIndex >= 0 && targetIndex >= 0 && power >= 0)
                {
                    var newEffect = new TypeEffect(){
                        UserPokemonTypeId = listPokemonType[userIndex].PokemonTypeId, 
                        TargetPokemonTypeId=listPokemonType[targetIndex].PokemonTypeId, 
                        Power=power};
                    modelBuilder.Entity<TypeEffect>().HasData(newEffect);
                }
            }
        }

        private List<TypeEffectDTO> CreateEffectList()
        {
            var listEffects = new List<TypeEffectDTO>();
            //Normal --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Normal", TargetPokemonType = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Normal", TargetPokemonType = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Normal", TargetPokemonType = "Ghost", Power=0 });

            //Fire --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fire", TargetPokemonType = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fire", TargetPokemonType = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fire", TargetPokemonType = "Ice", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fire", TargetPokemonType = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fire", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fire", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fire", TargetPokemonType = "Water", Power=2 });

            //Fighting --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Normal", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Steel", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Bug", Power=0 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Psychic", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fighting", TargetPokemonType = "Ghost", Power=0 });

            //Water --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Water", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Water", TargetPokemonType = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Water", TargetPokemonType = "Rock", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Water", TargetPokemonType = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Water", TargetPokemonType = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Water", TargetPokemonType = "Water", Power=0.5m });

            //Flying --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Flying", TargetPokemonType = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Flying", TargetPokemonType = "Fighting", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Flying", TargetPokemonType = "Grass", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Flying", TargetPokemonType = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Flying", TargetPokemonType = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Flying", TargetPokemonType = "Steel", Power=0.5m });

            //Grass --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Water", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Grass", TargetPokemonType = "Steel", Power=0.5m });

            //Poison --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Poison", TargetPokemonType = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Poison", TargetPokemonType = "Grass", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Poison", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Poison", TargetPokemonType = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Poison", TargetPokemonType = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Poison", TargetPokemonType = "Ghost", Power=0.5m });

            //Electric --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Electric", TargetPokemonType = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Electric", TargetPokemonType = "Water", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Electric", TargetPokemonType = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Electric", TargetPokemonType = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Electric", TargetPokemonType = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Electric", TargetPokemonType = "Ground", Power=0 });

            //Ground --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Electric", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Poison", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Steel", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ground", TargetPokemonType = "Flying", Power=0 });

            //Psychic --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Psychic", TargetPokemonType = "Fight", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Psychic", TargetPokemonType = "Poison", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Psychic", TargetPokemonType = "Psychic", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Psychic", TargetPokemonType = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Psychic", TargetPokemonType = "Dark", Power=0 });

            //Rock --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Rock", TargetPokemonType = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Rock", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Rock", TargetPokemonType = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Rock", TargetPokemonType = "Ice", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Rock", TargetPokemonType = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Rock", TargetPokemonType = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Rock", TargetPokemonType = "Steel", Power=0.5m });

            //Ice --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Ground", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Ice", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ice", TargetPokemonType = "Water", Power=0.5m });

            //Bug --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Ghost", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Bug", TargetPokemonType = "Steel", Power=0.5m });

            //Dragon --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dragon", TargetPokemonType = "Dragon", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dragon", TargetPokemonType = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dragon", TargetPokemonType = "Fairy", Power=0 });

            //Ghost --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ghost", TargetPokemonType = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ghost", TargetPokemonType = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ghost", TargetPokemonType = "Dark", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Ghost", TargetPokemonType = "Normal", Power=0 });

            //Dark --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dark", TargetPokemonType = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dark", TargetPokemonType = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dark", TargetPokemonType = "Dark", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dark", TargetPokemonType = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Dark", TargetPokemonType = "Fighting", Power=0.5m });

            //Steel --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Steel", TargetPokemonType = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Steel", TargetPokemonType = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Steel", TargetPokemonType = "Rock", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Steel", TargetPokemonType = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Steel", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Steel", TargetPokemonType = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Steel", TargetPokemonType = "Water", Power=0.5m });

            //Fairy --------------------------
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fairy", TargetPokemonType = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fairy", TargetPokemonType = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fairy", TargetPokemonType = "Fighting", Power=2 });

            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fairy", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fairy", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { UserPokemonType = "Fairy", TargetPokemonType = "Steel", Power=0.5m });

            //Return list --------------------------
            return listEffects;
        }
    }
}
