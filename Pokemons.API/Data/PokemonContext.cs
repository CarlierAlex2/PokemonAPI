using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Pokemons.API.Models;
using Pokemons.API.Configuration;

using AutoMapper;
using Pokemons.API.Data.CsvStream;
using Pokemons.API.Data.Seeding;

namespace Pokemons.API.Data
{
    public interface IPokemonContext
    {
        DbSet<Pokemon> Pokemons { get; set; }

        DbSet<Typing> Typings { get; set; }
        DbSet<TypeEffect> TypeEffects { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }



    public class PokemonContext : DbContext, IPokemonContext
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Typing> Typings { get; set; }
        public DbSet<TypeEffect> TypeEffects { get; set; }

        private readonly ConnectionStrings _connectionStrings;
        private readonly CsvSettings _csvSettings;
        private readonly IMapper _mapper;


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public PokemonContext(
            DbContextOptions<PokemonContext> options, 
            IOptions<ConnectionStrings> connectionstrings, IOptions<CsvSettings> csvSettings,
            IMapper mapper) : base(options)
        {
            _connectionStrings = connectionstrings.Value;
            _csvSettings = csvSettings.Value;
            _mapper = mapper;
        }


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            options.UseSqlServer(_connectionStrings.SQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set model RelationShips
            SetRelationShips(modelBuilder);

            // Seed database with data
            SeedData(modelBuilder);

            // Execute base
            base.OnModelCreating(modelBuilder);
        }


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        private void SetRelationShips(ModelBuilder modelBuilder)
        {
            #region // Typing Relations //----------------------------------------------------------------------------------
            // many-to-many relationship between Typing - TypeEffect - Typing
            modelBuilder.Entity<Typing>()
                    .HasMany(t => t.TypeOffense)
                    .WithOne(e => e.OffenseTyping)
                    .HasForeignKey(e => e.OffenseTypingId)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Typing>()
                    .HasMany(t => t.TypeDefense)
                    .WithOne(e => e.DefenseTyping)
                    .HasForeignKey(e => e.DefenseTypingId)
                    .OnDelete(DeleteBehavior.NoAction);
            #endregion


            #region // Pokemon Relations //----------------------------------------------------------------------------------
            // Pokemon have unique key of (PokedexEntry, Generation) 
            // -> Easier to identify + in actual games, pokemon can only have one variant per generation
            modelBuilder.Entity<Pokemon>().HasIndex(p => new {p.PokedexEntry , p.Generation}).IsUnique();

            // Intermediate table key is combination of keys from references (PokemonId, TypingId)
            modelBuilder.Entity<PokemonTyping>().HasKey(cs => new { cs.PokemonId, cs.TypingId });
            
            // many-to-many relationship between Pokemon - PokemonTyping - Typing
            modelBuilder.Entity<Pokemon>()
                    .HasMany(pok => pok.PokemonTypings)
                    .WithOne(pokType => pokType.Pokemon)
                    .HasForeignKey(pokType => pokType.PokemonId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Typing>()
                    .HasMany(t => t.PokemonTypings)
                    .WithOne(pokType => pokType.Typing)
                    .HasForeignKey(pokType => pokType.TypingId)
                    .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }


        private void SeedData(ModelBuilder modelBuilder)
        {
            #region // Read CSV //----------------------------------------------------------------------------------  
            // Read CSV files from assets for seeding data
            CsvContext csvContext = new CsvContextTyping(_csvSettings); // Read Typing CSV
            var listTypingData = csvContext.ReadFromCsv();
            
            csvContext = new CsvContextTypeEffect(_csvSettings); // Read TypeEffect CSV
            var listTypeEffectData = csvContext.ReadFromCsv();
            
            csvContext = new CsvContextPokemon(_csvSettings); // Read Pokemon CSV
            var listPokemonData = csvContext.ReadFromCsv();
            #endregion


            #region // Seeding //----------------------------------------------------------------------------------  
            // Execute seeding with provided seeding data
            SeedingHelper seedingHelper = new SeedingTyping(modelBuilder, _mapper) { // Seed Typing
                _listTypingData = listTypingData
            };
            seedingHelper.Seeding();

            seedingHelper = new SeedingTypeEffect(modelBuilder, _mapper) { // Seed TypeEffect
                _listTypingData = listTypingData,
                _listTypeEffectData = listTypeEffectData
            };
            seedingHelper.Seeding();

            seedingHelper = new SeedingPokemon(modelBuilder, _mapper) { // Seed Pokemon + PokemonTyping
                _listTypingData = listTypingData,
                _listPokemonData = listPokemonData
            };
            seedingHelper.Seeding();
            #endregion
        }
    }
}
