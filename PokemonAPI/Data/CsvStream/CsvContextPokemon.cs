using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using AutoMapper;
using CsvHelper;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Configuration;
using PokemonAPI.Helpers;

namespace PokemonAPI.Data.CsvStream
{
    public class CsvContextPokemon : CsvContext
    {
        public CsvContextPokemon(CsvSettings csvSettings, IMapper mapper) 
        : base(csvSettings, mapper)
        {
            
        }

        //CSV Functions -------------------------------------------------------------------------------------------
        protected override List<ModelObject> DoReadFromCsv()
        {
            using (var reader = new StreamReader(_csvSettings.CsvPokemon))
            using (var csv = new CsvReader(reader, _csvConfiguration))
            {
                var records = csv.GetRecords<PokemonData>().ToList<PokemonData>();
                var listDTO = PokemonDataHelper.DataToDto(records, _mapper);
                return listDTO.Cast<ModelObject>().ToList();
            }   
        }

        protected override void DoWriteToCsv(List<ModelObject> listObject)
        {
            var listDTO = listObject.Cast<PokemonDTO>().ToList();
            using (var writer = new StreamWriter(_csvSettings.CsvPokemon))
            using (var csv = new CsvWriter(writer, _csvConfiguration))
            {
                var listData = PokemonDataHelper.DtoToData(listDTO, _mapper);
                csv.WriteRecords(listData);
            }
        }
    }
}
