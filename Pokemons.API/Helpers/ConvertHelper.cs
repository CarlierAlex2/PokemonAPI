using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Pokemons.API.Models;
using Pokemons.API.DTO;


namespace Pokemons.API.Helpers
{
    public interface IConvertHelper
    {
        PokemonBaseDTO PokemonBase_To_DTO(Pokemon pokemon);
        List<PokemonBaseDTO> PokemonBase_To_DTOList(List<Pokemon> pokemons);
        PokemonDTO Pokemon_To_DTO(Pokemon pokemon);
        List<PokemonDTO> Pokemon_To_DTOList(List<Pokemon> pokemons);
        List<TypingBaseDTO> TypingBase_To_DTO_List(List<Typing> listTyping);
        TypingDTO Typing_To_DTO(Typing typing);
    }

    public class ConvertHelper : IConvertHelper
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        protected readonly IMapper _mapper;


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public ConvertHelper(IMapper mapper)
        {
            _mapper = mapper;
        }


        #region // Convert functions - Typing //-------------------------------------------------------------------------------------------------------------------------------
        public TypingDTO Typing_To_DTO(Typing typing)
        {
            TypingDTO typingDTO = _mapper.Map<TypingDTO>(typing);

            for (int index = 0; index < typing.TypeOffense.Count; index++)
            {
                typingDTO.TypeOffense[index] = _mapper.Map<TypeEffectOffenseDTO>(typing.TypeOffense[index]);
                typingDTO.TypeOffense[index].Defend = typing.TypeOffense[index].DefenseTyping.Name; //offense -> defense, current type attacks
            }
            for (int index = 0; index < typing.TypeDefense.Count; index++)
            {
                typingDTO.TypeDefense[index] = _mapper.Map<TypeEffectDefenseDTO>(typing.TypeDefense[index]);
                typingDTO.TypeDefense[index].Attack = typing.TypeDefense[index].OffenseTyping.Name; //defense -> offense, current type defends
            }

            return typingDTO;
        }

        public List<TypingBaseDTO> TypingBase_To_DTO_List(List<Typing> listTyping)
        {
            // Convert list TypingBaseDTO to TypingDto
            List<TypingBaseDTO> dto = _mapper.Map<List<TypingBaseDTO>>(listTyping);
            return dto;
        }
        #endregion



        #region // Convert functions - Pokemon //-------------------------------------------------------------------------------------------------------------------------------
        public PokemonDTO Pokemon_To_DTO(Pokemon pokemon)
        {
            // Convert Pokemon to PokemonDTO
            PokemonDTO dto = _mapper.Map<PokemonDTO>(pokemon);
            if (pokemon.PokemonTypings != null)
                dto.Types = pokemon.PokemonTypings.Select(r => r.Typing.Name).ToList();

            return dto;
        }

        public PokemonBaseDTO PokemonBase_To_DTO(Pokemon pokemon)
        {
            // Convert Pokemon to PokemonBase
            PokemonBaseDTO dto = _mapper.Map<PokemonBaseDTO>(pokemon);
            if (pokemon.PokemonTypings != null)
                dto.Types = pokemon.PokemonTypings.Select(r => r.Typing.Name).ToList();

            return dto;
        }

        public List<PokemonDTO> Pokemon_To_DTOList(List<Pokemon> pokemons)
        {
            // Convert list Pokemon to PokemonDTO
            List<PokemonDTO> resultDTO = new List<PokemonDTO>();
            foreach (var pok in pokemons)
            {
                var dto = Pokemon_To_DTO(pok);
                if (dto != null)
                    resultDTO.Add(dto);
            }
            return resultDTO;
        }

        public List<PokemonBaseDTO> PokemonBase_To_DTOList(List<Pokemon> pokemons)
        {
            // Convert list Pokemon to PokemonBaseDTO
            List<PokemonBaseDTO> resultDTO = new List<PokemonBaseDTO>();
            foreach (var pok in pokemons)
            {
                var dto = PokemonBase_To_DTO(pok);
                if (dto != null)
                    resultDTO.Add(dto);
            }
            return resultDTO;
        }
        #endregion
    }
}
