using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Pokemons.API.DTO;

namespace Pokemons.API.Data.CsvStream
{
    public class PokemonDataHelper
    {
        public static List<PokemonDTO> DataToDto(List<PokemonData> listObjects, IMapper mapper)
        {
            var listNew = new List<PokemonDTO>();
            foreach(var obj in listObjects)
            {
                var dto = mapper.Map<PokemonDTO>(obj);
                obj.Types = obj.Types.Replace(" ", "");
                dto.Types = obj.Types.Split(',').ToList();
                listNew.Add(dto);
            }
            return listNew;
        } 

        public static List<PokemonData> DtoToData(List<PokemonDTO> listObjects, IMapper mapper)
        {
            var listNew = new List<PokemonData>();
            foreach(var obj in listObjects)
            {
                var d = mapper.Map<PokemonData>(obj);
                d.Types = obj.Types.Aggregate((a, b) => a + "," + b);
                listNew.Add(d);
            }
            return listNew;
        } 
    }
}
