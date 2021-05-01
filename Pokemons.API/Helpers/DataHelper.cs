using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream.CsvData;

namespace Pokemons.API.Helpers
{
    public class DataHelper
    {
        // Compression functions //-------------------------------------------------------------------------------------------------------------------------------
        public static string CompressTypesToString(List<string> typeNames)
        {
            // Merge Typing names into a single string for CSV Data
            return typeNames.Aggregate((a, b) => a + "," + b);
        } 


        // Extraction functions //-------------------------------------------------------------------------------------------------------------------------------
        public static List<string> ExtractTypesFromString(string typeNames)
        {
            // Extract Types names from a string
            return typeNames.Replace(" ", "").Split(',').ToList();
        } 
        
        public static List<PokemonTyping> ExtractPokemonTypings(Pokemon pokemon, List<string> pokemonTypes, List<TypingData> listTypes)
        {
            // Create list of PokemonTyping for Pokemon and it's Typing's
            var pokemonTypings = new List<PokemonTyping>();
            foreach(var typeName in pokemonTypes)
            {
                var newPokemonTyping = new PokemonTyping(){ 
                    PokemonId = pokemon.PokemonId, 
                    TypingId = listTypes.Find(t => t.Name == typeName).TypingId};
                pokemonTypings.Add(newPokemonTyping);
            }
            return pokemonTypings;
        }

        public static List<TypeEffect> ExtractTypeEffects(List<TypingData> listTypingData, List<TypeEffectData> listTypeEffectData, IMapper mapper)
        {
            // Create list of TypeEffect for Typing
            List<TypeEffect> listNew = new List<TypeEffect>();
            int id = 1;
            
            foreach (var dataObj in listTypeEffectData)
            {
                // Check if Typings exist
                int offenseIndex = listTypingData.FindIndex(t => t.Name == dataObj.Attack);
                int defenseIndex = listTypingData.FindIndex(t => t.Name == dataObj.Defend);

                // Create TypeEffect object
                if(offenseIndex >= 0 && defenseIndex >= 0)
                {
                    TypeEffect typeEffect = mapper.Map<TypeEffect>(dataObj);
                    typeEffect.TypeEffectId = id;
                    typeEffect.OffenseTypingId = listTypingData[offenseIndex].TypingId;
                    typeEffect.DefenseTypingId = listTypingData[defenseIndex].TypingId;
                    listNew.Add(typeEffect);
                    id++;
                }
            }
            return listNew;
        }
    }
}
