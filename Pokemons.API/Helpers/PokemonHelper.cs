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
            var intermediate = pokemonDTO.Types.Select(t => t.Replace(" ", "").ToLower()) // convert all to lower + remove spacing
                                                .Where(t => !string.IsNullOrWhiteSpace(t)) // check if there is a value
                                                .Distinct().ToList(); // get distinct
            pokemonDTO.Types = intermediate.Select(t => char.ToUpper(t[0]) + t.Substring(1)).ToList(); // convert first char to uppercase
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
