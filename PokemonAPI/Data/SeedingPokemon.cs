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
    public class SeedingPokemon
    {
        public static void Seeding(
            ModelBuilder modelBuilder, 
            IMapper mapper, 
            CsvSettings csvSettings,
            List<Typing> listTypings)
        {
            //var listPokemon = CreateList();
            //WriteToRegistrationsCSV(listPokemon, mapper, csvSettings);
            var listPokemon = ReadCSVPokemonDTO(mapper, csvSettings);
            SeedPokemons(modelBuilder, mapper, listTypings, listPokemon);
        }

        private static void WriteToRegistrationsCSV(List<PokemonDTO> listPokemonDTO, IMapper mapper, CsvSettings csvSettings)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var writer = new StreamWriter(csvSettings.CsvPokemon))
            using (var csv = new CsvWriter(writer, config))
            {
                var listData = new List<PokemonData>();
                foreach(var dto in listPokemonDTO)
                {
                    var p = mapper.Map<PokemonData>(dto);
                    p.Types = dto.Types.Aggregate((a, b) => a + "," + b);
                    listData.Add(p);
                }

                csv.WriteRecords(listData);
            }
        }

        private static List<PokemonDTO> ReadCSVPokemonDTO(IMapper mapper, CsvSettings csvSettings)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            
            using (var reader = new StreamReader(csvSettings.CsvPokemon))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<PokemonData>().ToList<PokemonData>();

                var listData = new List<PokemonDTO>();
                foreach(var r in records)
                {
                    var dto = mapper.Map<PokemonDTO>(r);
                    r.Types = r.Types.Replace(" ", "");
                    dto.Types = r.Types.Split(',').ToList();
                    listData.Add(dto);
                }

                return listData;
            }   
        }

        private static List<PokemonDTO> CreateList()
        {
            var listPokemon = new List<PokemonDTO>();

            #region Create Pokemon List
            //Normal --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 143, Name = "Snorlax", Generation = 1, Classification = "Sleeping Pokemon", EggGroup = "Monster",
                Types = new List<string>{"Normal"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 598, Name = "Obstagoon", Generation = 8, Classification = "Blocking Pokemon", EggGroup = "Field",
                Types = new List<string>{"Dark", "Normal"}});

            //Fire --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 4, Name = "Charmander", Generation = 1, Classification = "Lizard Pokemon", EggGroup = "Monster,Dragon",
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
                PokedexEntry = 260, Name = "Swampert", Generation = 3, Classification = "Mud Fish Pokemon", EggGroup = "Monster,Water 1",
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
                PokedexEntry = 407, Name = "Roserade", Generation = 4, Classification = "Bouquet Pokemon", EggGroup = "Fairy,Grass",
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
                PokedexEntry = 361, Name = "Snorunt", Generation = 3, Classification = "Snow Hat Pokemon", EggGroup = "Fairy,Mineral",
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
                PokedexEntry = 148, Name = "Dragonair", Generation = 1, Classification = "Dragon Pokemon", EggGroup = "Water 1,Dragon",
                Types = new List<string>{"Dragon"}});
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 887, Name = "Dragapult", Generation = 8, Classification = "Stealth Pokemon", EggGroup = "Amorphous,Dragon",
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
                PokedexEntry = 274, Name = "Nuzleaf", Generation = 3, Classification = "Wily Pokemon", EggGroup = "Field,Grass",
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
                PokedexEntry = 598, Name = "Ferrothorn", Generation = 5, Classification = "Thorn Pod Pokemon", EggGroup = "Grass,Mineral",
                Types = new List<string>{"Grass","Steel"}
                });

            //Fairy --------------------------
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 39, Name = "Jigglypuff", Generation = 1, Classification = "Balloon Pokemon", EggGroup = "Fairy",
                Types = new List<string>{"Normal","Fairy"}
                });
            listPokemon.Add(new PokemonDTO() { 
                PokedexEntry = 282, Name = "Gardevoir", Generation = 3, Classification = "Embrace Pokemon", EggGroup = "Human-Like,Amorphous",
                Types = new List<string>{"Psychic","Fairy"}
                });
            #endregion

            return listPokemon;
        }

        private static void SeedPokemons(ModelBuilder modelBuilder, IMapper mapper, List<Typing> listTypings, List<PokemonDTO> listPokemon)
        {
            //Seeding ---------------------------------------------------------------------------
            for(int index = 0; index < listPokemon.Count; index++)
            {
                var pokemonDTO = listPokemon[index];
                Pokemon pokemon = mapper.Map<Pokemon>(pokemonDTO);
                pokemon.PokemonId = Guid.NewGuid();
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
    }
}
