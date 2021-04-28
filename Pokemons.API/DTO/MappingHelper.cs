using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream.CsvData;

namespace Pokemons.API.DTO
{
    public class MappingHelper
    {
        public static string CompressTypesToString(List<string> typeNames)
        {
            return typeNames.Aggregate((a, b) => a + "," + b);
        } 

        public static List<string> ExtractTypesFromString(string typeNames)
        {
            typeNames = typeNames.Replace(" ", "");
            return typeNames.Split(',').ToList();
        } 
        
        public static List<PokemonTyping> ExtractPokemonTypings(Pokemon pokemon, List<string> listTypeNames, List<TypingData> listTypes)
        {
            var pokemonTypings = new List<PokemonTyping>();
            foreach(var typeName in listTypeNames)
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
            List<TypeEffect> listNew = new List<TypeEffect>();
            int id = 1;
            foreach (var dataObj in listTypeEffectData)
            {
                TypeEffect typeEffect = mapper.Map<TypeEffect>(dataObj);
                int offenseIndex = listTypingData.FindIndex(t => t.Name == dataObj.Attack);
                int defenseIndex = listTypingData.FindIndex(t => t.Name == dataObj.Defend);

                if(offenseIndex >= 0 && defenseIndex >= 0)
                {
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
