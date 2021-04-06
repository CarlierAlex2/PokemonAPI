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

            //Normal --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 143, Name = "Snorlax", Generation = 1, Classification = "Sleeping Pokemon", EggGroup = "Monster",
                Types = new List<string>{"Normal"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 598, Name = "Obstagoon", Generation = 8, Classification = "Blocking Pokemon", EggGroup = "Field",
                Types = new List<string>{"Dark", "Normal"}});

            //Fire --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 4, Name = "Charmander", Generation = 1, Classification = "Lizard Pokemon", EggGroup = "Monster, Dragon",
                Types = new List<string>{"Fire"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 637, Name = "Volcarona", Generation = 5, Classification = "Sun Pokemon", EggGroup = "Bug",
                Types = new List<string>{"Bug", "Fire"}});

            //Fighting --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 67, Name = "Machoke", Generation = 1, Classification = "Superpower Pokemon", EggGroup = "Human-Like",
                Types = new List<string>{"Fighting"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 214, Name = "Heracross", Generation = 2, Classification = "Single Horn Pokemon", EggGroup = "Bug",
                Types = new List<string>{"Bug", "Fighting"}});

            //Water --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 260, Name = "Swampert", Generation = 3, Classification = "Mud Fish Pokemon", EggGroup = "Monster, Water 1",
                Types = new List<string>{"Water", "Ground"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 637, Name = "Skrelp", Generation = 6, Classification = "Mock Kelp Pokemon", EggGroup = "Bug",
                Types = new List<string>{"Poison", "Water"}});

            //Flying --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 823, Name = "Corviknight", Generation = 8, Classification = "Raven Pokemon", EggGroup = "Flying",
                Types = new List<string>{"Flying", "Steel"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 425, Name = "Drifloon", Generation = 4, Classification = "Balloon Pokemon", EggGroup = "Amorphous",
                Types = new List<string>{"Ghost", "Flying"}});

            //Grass --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 753, Name = "Fomantis", Generation = 7, Classification = "Sickle Grass Pokemon", EggGroup = "Grass",
                Types = new List<string>{"Grass"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 345, Name = "Lileep", Generation = 3, Classification = "Sea Lily Pokemon", EggGroup = "Water 3",
                Types = new List<string>{"Rock", "Grass"}});

            //Poison --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 804, Name = "Naganadel", Generation = 7, Classification = "Poison Pin Pokemon", EggGroup = "Undiscovered",
                Types = new List<string>{"Poison", "Dragon"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 407, Name = "Roserade", Generation = 4, Classification = "Bouquet Pokemon", EggGroup = "Fairy, Grass",
                Types = new List<string>{"Grass", "Poison"}});

            //Electric --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 462, Name = "Magnezone", Generation = 4, Classification = "Magnet Area Pokemon", EggGroup = "Mineral",
                Types = new List<string>{"Electric", "Steel"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 644, Name = "Zekrom", Generation = 5, Classification = "Deep Black Pokemon", EggGroup = "Undiscovered",
                Types = new List<string>{"Dragon", "Electric"}});

            //Ground --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 622, Name = "Golett", Generation = 5, Classification = "Automaton Pokemon", EggGroup = "Mineral",
                Types = new List<string>{"Ground", "Ghost"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 718, Name = "Zygarde", Generation = 6, Classification = "Order Pokemon", EggGroup = "Undiscovered",
                Types = new List<string>{"Dragon", "Ground"}});

            //Psychic --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 63, Name = "Abra", Generation = 1, Classification = "Psi Pokemon", EggGroup = "Human-Like",
                Types = new List<string>{"Psychic"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 437, Name = "Bronzong", Generation = 4, Classification = "Bronze Bell Pokemon", EggGroup = "Mineral",
                Types = new List<string>{"Steel", "Psychic"}});

            //Rock --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 409, Name = "Rampardos", Generation = 4, Classification = "Head Butt Pokemon", EggGroup = "Monster",
                Types = new List<string>{"Rock"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 699, Name = "Aurorus", Generation = 6, Classification = "Tundra Pokemon", EggGroup = "Monster",
                Types = new List<string>{"Rock", "Ice"}});

            //Ice --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 361, Name = "Snorunt", Generation = 3, Classification = "Snow Hat Pokemon", EggGroup = "Fairy, Mineral",
                Types = new List<string>{"Ice"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 872, Name = "Snom", Generation = 8, Classification = "Worm Pokemon", EggGroup = "Bug",
                Types = new List<string>{"Ice", "Bug"}});

            //Bug --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 541, Name = "Swadloon", Generation = 5, Classification = "Leaf-Wrapped Pokemon", EggGroup = "Bug",
                Types = new List<string>{"Bug", "Grass"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 544, Name = "Whirlipede", Generation = 5, Classification = "Curlipede Pokemon", EggGroup = "Bug",
                Types = new List<string>{"Bug", "Poison"}});

            //Dragon --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 148, Name = "Dragonair", Generation = 1, Classification = "Dragon Pokemon", EggGroup = "Water 1, Dragon",
                Types = new List<string>{"Dragon"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 887, Name = "Dragapult", Generation = 8, Classification = "Stealth Pokemon", EggGroup = "Amorphous, Dragon",
                Types = new List<string>{"Dragon", "Ghost"}});

            //Ghost --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 200, Name = "Misdreavus", Generation = 2, Classification = "Screech Pokemon", EggGroup = "Amorphous",
                Types = new List<string>{"Ghost"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 429, Name = "Mismagius", Generation = 4, Classification = "Magical Pokemon", EggGroup = "Amorphous",
                Types = new List<string>{"Ghost"}});

            //Dark --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 306, Name = "Nuzleaf", Generation = 3, Classification = "Wily Pokemon", EggGroup = "Field, Grass",
                Types = new List<string>{"Grass","Dark"}
                });
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 435, Name = "Skuntank", Generation = 4, Classification = "Skunk Pokemon", EggGroup = "Field",
                Types = new List<string>{"Poison","Dark"}
                });

            //Steel --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 306, Name = "Aggron", Generation = 3, Classification = "Iron Armor Pokemon", EggGroup = "Monster",
                Types = new List<string>{"Steel","Rock"}
                });
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 598, Name = "Ferrothorn", Generation = 5, Classification = "Thorn Pod Pokemon", EggGroup = "Grass, Mineral",
                Types = new List<string>{"Grass","Steel"}
                });

            //Fairy --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 39, Name = "Jigglypuff", Generation = 1, Classification = "Balloon Pokemon", EggGroup = "Fairy",
                Types = new List<string>{"Normal","Fairy"}
                });
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 282, Name = "Gardevoir", Generation = 3, Classification = "Embrace Pokemon", EggGroup = "Human-Like, Amorphous",
                Types = new List<string>{"Psychic","Fairy"}
                });


            //Seeding ---------------------------------------------------------------------------
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
