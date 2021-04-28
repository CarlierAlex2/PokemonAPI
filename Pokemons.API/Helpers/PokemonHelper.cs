using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Pokemons.API.DTO;

namespace Pokemons.API.Helpers
{
    public class PokemonHelper
    {
        public static PokemonDTO CleanupPokemonDTO(PokemonDTO pokemonDTO)
        {
            // Cleanup
            pokemonDTO.Name = pokemonDTO.Name.Replace(" ", "");
            pokemonDTO.EggGroup = pokemonDTO.EggGroup.Replace(" ", "");
            pokemonDTO.Types = pokemonDTO.Types.Select(t => t.Replace(" ", "")).ToList();
            return pokemonDTO;
        }

        public static Tuple<bool, string> VerifyPokemonDTO(PokemonDTO pokemonDTO, List<string> listTypes)
        {
            // Verify if values are correct
            // already done by attributes
            /*
            if (pokemonDTO.PokedexEntry <= 0)
                return new Tuple<bool, string> (false, "PokedexEntry not possible - Must be 1 or higher");
            else if (pokemonDTO.Name.Length < 2)
                return new Tuple<bool, string> (false, "Name not possible - Must be at least 3 characters long");
            else if (pokemonDTO.Generation <= 0)
                return new Tuple<bool, string> (false, "Generation not possible - Must be 1 or higher");
            */

            foreach (var typeName in pokemonDTO.Types)
            {
                if (listTypes.Contains(typeName) == false)
                    return new Tuple<bool, string> (false, $"Typing not possible - '{typeName}' does not exist");
            }

            return new Tuple<bool, string> (true, "OK");
        }
    }
}
