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

namespace PokemonAPI.Data
{
    public interface IPokemonContext
    {
        DbSet<Pokemon> Pokemons { get; set; }

        DbSet<Typing> Typings { get; set; }
        DbSet<TypeEffect> TypeEffects { get; set; }
    }

    public class PokemonContext : DbContext, IPokemonContext
    {
        private readonly IMapper _mapper;
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Typing> Typings { get; set; }
        public DbSet<TypeEffect> TypeEffects { get; set; }
        private readonly ConnectionStrings _connectionStrings;

        public PokemonContext(
            DbContextOptions<PokemonContext> options, 
            IOptions<ConnectionStrings> connectionstrings,
            IMapper mapper) : base(options)
        {
            _connectionStrings = connectionstrings.Value;
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
            #region TypeEffect Relations
            modelBuilder.Entity<TypeEffect>()
                    .HasOne(effect => effect.OffenseTyping)
                    .WithMany(pokemon => pokemon.TypeOffense)
                    .HasForeignKey(effect => effect.OffenseTypingId)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TypeEffect>()
                    .HasOne(effect => effect.DefenseTyping)
                    .WithMany(pokemon => pokemon.TypeDefense)
                    .HasForeignKey(effect => effect.DefenseTypingId)
                    .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region PokemonTyping Relations
            modelBuilder.Entity<PokemonTyping>().HasKey(cs => new { cs.PokemonId, cs.TypingId });
            
            modelBuilder.Entity<PokemonTyping>()
                    .HasOne(pokType => pokType.Pokemon)
                    .WithMany(pok => pok.PokemonTypings)
                    .HasForeignKey(pokType => pokType.PokemonId)
                    .OnDelete(DeleteBehavior.NoAction);
            

            modelBuilder.Entity<PokemonTyping>()
                    .HasOne(pokType => pokType.Typing)
                    .WithMany(typing => typing.PokemonTypings)
                    .HasForeignKey(pokType => pokType.TypingId)
                    .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var listTypings =  SeedingTyping.Seeding(modelBuilder, _mapper);
            SeedingPokemon.Seeding(modelBuilder, _mapper, listTypings);
        }
    }
}
