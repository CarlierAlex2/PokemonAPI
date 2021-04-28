using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Pokemons.API.Models;
using Pokemons.API.DTO;
using Pokemons.API.Configuration;

using AutoMapper;
using Pokemons.API.Data.CsvStream;
using Pokemons.API.Data.Seeding;
using System.Linq;

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
        private readonly IMapper _mapper;
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Typing> Typings { get; set; }
        public DbSet<TypeEffect> TypeEffects { get; set; }
        private readonly ConnectionStrings _connectionStrings;
        private readonly CsvSettings _csvSettings;

        public PokemonContext(
            DbContextOptions<PokemonContext> options, 
            IOptions<ConnectionStrings> connectionstrings, IOptions<CsvSettings> csvSettings,
            IMapper mapper) : base(options)
        {
            _connectionStrings = connectionstrings.Value;
            _csvSettings = csvSettings.Value;
            _mapper = mapper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            options.UseSqlServer(_connectionStrings.SQL);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetRelationShips(modelBuilder);
            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SetRelationShips(ModelBuilder modelBuilder)
        {
            #region Typing Relations
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

            #region Pokemon Relations
            modelBuilder.Entity<Pokemon>().HasIndex(p => new {p.PokedexEntry , p.Generation}).IsUnique();
            modelBuilder.Entity<PokemonTyping>().HasKey(cs => new { cs.PokemonId, cs.TypingId });
            
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
            // Read CSV ----------------------------------------------
            CsvContext csvContext = new CsvContextTyping(_csvSettings);
            var listTypingData = csvContext.ReadFromCsv();

            csvContext = new CsvContextTypeEffect(_csvSettings);
            var listTypeEffectData = csvContext.ReadFromCsv();
            
            csvContext = new CsvContextPokemon(_csvSettings);
            var listPokemonData = csvContext.ReadFromCsv();


            // Seeding ----------------------------------------------
            SeedingHelper seedingHelper = new SeedingTyping(modelBuilder, _mapper)
            {
                _listTypingData = listTypingData
            };
            seedingHelper.Seeding();

            seedingHelper = new SeedingTypeEffect(modelBuilder, _mapper)
            {
                _listTypingData = listTypingData,
                _listTypeEffectData = listTypeEffectData
            };
            seedingHelper.Seeding();

            seedingHelper = new SeedingPokemon(modelBuilder, _mapper)
            {
                _listTypingData = listTypingData,
                _listPokemonData = listPokemonData
            };
            seedingHelper.Seeding();
        }
    }
}
