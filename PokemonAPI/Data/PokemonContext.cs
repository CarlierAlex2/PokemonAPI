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
            //https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table
            //https://forums.asp.net/t/2148073.aspx?What+is+the+alternative+to+WillCascadeOnDelete+in+EF+core
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.deletebehavior?view=efcore-5.0
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
            


            List<Typing> listTypings = SeedTypings(modelBuilder);
            SeedTypeEffect(modelBuilder, listTypings);
            SeedPokemons(modelBuilder, listTypings);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedPokemons(ModelBuilder modelBuilder, List<Typing> listTypings)
        {
            var listPokemon = new List<PokemonDTO>();
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 306, Name = "Aggron", Generation = 3,
                Types = new List<string>{"Steel","Rock"},
                Classification = "Iron Armor Pokemon", EggGroup = "Monster"
                });


            for(int index = 0; index < listPokemon.Count; index++)
            {
                var pokemonDTO = listPokemon[index];
                Pokemon pokemon = _mapper.Map<Pokemon>(pokemonDTO);
                pokemon.PokemonId = index + 1;
                var pokemonTypings = new List<PokemonTyping>();
                foreach(var t in pokemonDTO.Types)
                {
                    var newPokemonTyping = new PokemonTyping(){
                        PokemonId = pokemon.PokemonId,
                        TypingId = listTypings.Find(typing => typing.Name == t).TypingId
                        };
                    pokemonTypings.Add(newPokemonTyping);
                }
                //pokemon.PokemonTypings = pokemonTypings;


                modelBuilder.Entity<Pokemon>().HasData(pokemon);
                modelBuilder.Entity<PokemonTyping>().HasData(pokemonTypings);
            }
        }

        private List<Typing> SeedTypings(ModelBuilder modelBuilder)
        {
            var listTypings = new List<Typing>();
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

            foreach (var Typing in listTypings)
            {
                modelBuilder.Entity<Typing>().HasData(Typing);
            }

            return listTypings;
        }

        private void SeedTypeEffect(ModelBuilder modelBuilder, List<Typing> listTypings)
        {
            // Add effects --------------------------
            int id = 1;

            foreach (var effect in CreateEffectList())
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

        private List<TypeEffectDTO> CreateEffectList()
        {
            var listEffects = new List<TypeEffectDTO>();
            //Normal --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Normal", Defend = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Normal", Defend = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Normal", Defend = "Ghost", Power=0 });

            //Fire --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Ice", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fire", Defend = "Water", Power=2 });

            //Fighting --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Normal", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Steel", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Bug", Power=0 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Psychic", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Fighting", Defend = "Ghost", Power=0 });

            //Water --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Rock", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Water", Defend = "Water", Power=0.5m });

            //Flying --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Fighting", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Grass", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Flying", Defend = "Steel", Power=0.5m });

            //Grass --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Ground", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Water", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Grass", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Grass", Defend = "Steel", Power=0.5m });

            //Poison --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Grass", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Rock", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Poison", Defend = "Ghost", Power=0.5m });

            //Electric --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Water", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Dragon", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Electric", Defend = "Ground", Power=0 });

            //Ground --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Electric", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Poison", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Rock", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Steel", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Bug", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Grass", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ground", Defend = "Flying", Power=0 });

            //Psychic --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Fight", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Poison", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Psychic", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Psychic", Defend = "Dark", Power=0 });

            //Rock --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Bug", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Fire", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Ice", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Ground", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Rock", Defend = "Steel", Power=0.5m });

            //Ice --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Flying", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Ground", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Ice", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ice", Defend = "Water", Power=0.5m });

            //Bug --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Grass", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Fighting", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Flying", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Ghost", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Bug", Defend = "Steel", Power=0.5m });

            //Dragon --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Dragon", Defend = "Dragon", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Dragon", Defend = "Steel", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Dragon", Defend = "Fairy", Power=0 });

            //Ghost --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Dark", Power=0.5m });

            listEffects.Add(new TypeEffectDTO() { Attack = "Ghost", Defend = "Normal", Power=0 });

            //Dark --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Ghost", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Psychic", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Dark", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Fairy", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Dark", Defend = "Fighting", Power=0.5m });

            //Steel --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Fairy", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Ice", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Rock", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Electric", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Steel", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Steel", Defend = "Water", Power=0.5m });

            //Fairy --------------------------
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Dark", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Dragon", Power=2 });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Fighting", Power=2 });

            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Fire", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Poison", Power=0.5m });
            listEffects.Add(new TypeEffectDTO() { Attack = "Fairy", Defend = "Steel", Power=0.5m });

            //Return list --------------------------
            return listEffects;
        }
    }
}
