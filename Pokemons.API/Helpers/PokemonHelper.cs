using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Pokemons.API.DTO;

namespace Pokemons.API.Helpers
{
    public class PokemonHelper
    {
        // Cleanup functions //-------------------------------------------------------------------------------------------------------------------------------
        public static PokemonDTO CleanupPokemonDTO(PokemonDTO pokemonDTO)
        {
            // Cleanup PokemonDTO for user input
            pokemonDTO.Name = pokemonDTO.Name.Replace(" ", "");
            pokemonDTO.EggGroup = pokemonDTO.EggGroup.Replace(" ", "");
            pokemonDTO.Types = pokemonDTO.Types.Select(t => t.Replace(" ", "")).ToList();
            return pokemonDTO;
        }


        // Verify functions //-------------------------------------------------------------------------------------------------------------------------------
        public static Tuple<bool, string> VerifyPokemonDTO(PokemonDTO pokemonDTO, List<string> listTypes)
        {
            // Check if PokemonDTO Types are possible
            foreach (var typeName in pokemonDTO.Types)
            {
                // If impossible type is find, return FAIL + failure message
                if (listTypes.Contains(typeName) == false)
                    return new Tuple<bool, string> (false, $"Typing not possible - '{typeName}' does not exist");
            }

            return new Tuple<bool, string> (true, "OK");
        }
    }
}
