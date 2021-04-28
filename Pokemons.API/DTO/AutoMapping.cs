using System;
using AutoMapper;

using Pokemons.API.Models;
using Pokemons.API.Data.CsvStream.CsvData;

namespace Pokemons.API.DTO
{
    public class AutoMapping: Profile
    {
        public AutoMapping() {
            // DTO mapping -------------------------------------
            CreateMap<Pokemon, PokemonDTO>();
            CreateMap<PokemonDTO, Pokemon>();

            CreateMap<Pokemon, PokemonBaseDTO>();
            CreateMap<PokemonBaseDTO, Pokemon>();

            CreateMap<Typing, TypingDTO>();
            CreateMap<TypingDTO, Typing>();

            CreateMap<Typing, TypingBaseDTO>();
            CreateMap<TypingBaseDTO, Typing>();
            
            CreateMap<TypeEffect, TypeEffectDTO>();
            CreateMap<TypeEffectDTO, TypeEffect>();
            
            CreateMap<TypeEffect, TypeEffectDefenseDTO>();
            CreateMap<TypeEffect, TypeEffectOffenseDTO>();

            // Data mapping ------------------------------------
            CreateMap<Pokemon, PokemonData>();
            CreateMap<PokemonData, Pokemon>();

            CreateMap<Typing, TypingData>();
            CreateMap<TypingData, Typing>();

            CreateMap<TypeEffect, TypeEffectData>();
            CreateMap<TypeEffectData, TypeEffect>();
        }
    }
}
