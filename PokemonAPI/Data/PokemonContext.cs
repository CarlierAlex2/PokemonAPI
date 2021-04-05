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
    }

    public class PokemonContext : DbContext, IPokemonContext
    {
        public DbSet<PokemonType> PokemonTypes { get; set; }
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
            SeedPokemonTypes(modelBuilder);
        }

        private void SeedPokemonTypes(ModelBuilder modelBuilder)
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
        }
    }
}
