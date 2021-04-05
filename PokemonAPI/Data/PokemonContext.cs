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
            modelBuilder.Entity<TypeEffect>().HasKey(cs => new { cs.PokemonTypeId, cs.TargetPokemonTypeId });
            //modelBuilder.Entity<TypeEffect>().HasOne<PokemonType>().WithMany().HasForeignKey(e => e.TargetPokemonTypeId);
            //modelBuilder.Entity<TypeEffect>().HasOne(t => t.TargetPokemonType).WithOne();

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
                int userIndex = listPokemonType.FindIndex(t => t.Name == effect.PokemonType);
                int targetIndex = listPokemonType.FindIndex(t => t.Name == effect.TargetPokemonType);
                decimal power = effect.Power;

                if(userIndex >= 0 && targetIndex >= 0 && power >= 0)
                {
                    var newEffect = new TypeEffect(){
                        PokemonTypeId = listPokemonType[userIndex].PokemonTypeId, 
                        TargetPokemonTypeId=listPokemonType[targetIndex].PokemonTypeId, 
                        //TargetPokemonType=listPokemonType[targetIndex],
                        Power=power};
                    modelBuilder.Entity<TypeEffect>().HasData(newEffect);
                }
            }
        }

        private List<TypeEffectDTO> CreateEffectList()
        {
            var listEffects = new List<TypeEffectDTO>();
            //Normal --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Normal", TargetPokemonType = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Normal", TargetPokemonType = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Normal", TargetPokemonType = "Ghost", Power=0 });

            //Fire --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fire", TargetPokemonType = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fire", TargetPokemonType = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fire", TargetPokemonType = "Ice", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fire", TargetPokemonType = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fire", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fire", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fire", TargetPokemonType = "Water", Power=2 });

            //Fighting --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Normal", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Steel", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Bug", Power=0 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Psychic", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fighting", TargetPokemonType = "Ghost", Power=0 });

            //Water --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Water", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Water", TargetPokemonType = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Water", TargetPokemonType = "Rock", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Water", TargetPokemonType = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Water", TargetPokemonType = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Water", TargetPokemonType = "Water", Power=0.5m });

            //Flying --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Flying", TargetPokemonType = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Flying", TargetPokemonType = "Fighting", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Flying", TargetPokemonType = "Grass", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Flying", TargetPokemonType = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Flying", TargetPokemonType = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Flying", TargetPokemonType = "Steel", Power=0.5m });

            //Grass --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Water", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Grass", TargetPokemonType = "Steel", Power=0.5m });

            //Poison --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Poison", TargetPokemonType = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Poison", TargetPokemonType = "Grass", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Poison", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Poison", TargetPokemonType = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Poison", TargetPokemonType = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Poison", TargetPokemonType = "Ghost", Power=0.5m });

            //Electric --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Electric", TargetPokemonType = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Electric", TargetPokemonType = "Water", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Electric", TargetPokemonType = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Electric", TargetPokemonType = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Electric", TargetPokemonType = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Electric", TargetPokemonType = "Ground", Power=0 });

            //Ground --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Electric", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Poison", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Steel", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ground", TargetPokemonType = "Flying", Power=0 });

            //Psychic --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Psychic", TargetPokemonType = "Fight", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Psychic", TargetPokemonType = "Poison", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Psychic", TargetPokemonType = "Psychic", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Psychic", TargetPokemonType = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Psychic", TargetPokemonType = "Dark", Power=0 });

            //Rock --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Rock", TargetPokemonType = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Rock", TargetPokemonType = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Rock", TargetPokemonType = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Rock", TargetPokemonType = "Ice", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Rock", TargetPokemonType = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Rock", TargetPokemonType = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Rock", TargetPokemonType = "Steel", Power=0.5m });

            //Ice --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Ground", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Ice", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ice", TargetPokemonType = "Water", Power=0.5m });

            //Bug --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Ghost", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Bug", TargetPokemonType = "Steel", Power=0.5m });

            //Dragon --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dragon", TargetPokemonType = "Dragon", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dragon", TargetPokemonType = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dragon", TargetPokemonType = "Fairy", Power=0 });

            //Ghost --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ghost", TargetPokemonType = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ghost", TargetPokemonType = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ghost", TargetPokemonType = "Dark", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Ghost", TargetPokemonType = "Normal", Power=0 });

            //Dark --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dark", TargetPokemonType = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dark", TargetPokemonType = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dark", TargetPokemonType = "Dark", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dark", TargetPokemonType = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Dark", TargetPokemonType = "Fighting", Power=0.5m });

            //Steel --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Steel", TargetPokemonType = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Steel", TargetPokemonType = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Steel", TargetPokemonType = "Rock", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Steel", TargetPokemonType = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Steel", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Steel", TargetPokemonType = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Steel", TargetPokemonType = "Water", Power=0.5m });

            //Fairy --------------------------
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fairy", TargetPokemonType = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fairy", TargetPokemonType = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fairy", TargetPokemonType = "Fighting", Power=2 });

            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fairy", TargetPokemonType = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fairy", TargetPokemonType = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { PokemonType = "Fairy", TargetPokemonType = "Steel", Power=0.5m });

            //Return list --------------------------
            return listEffects;
        }
    }
}
