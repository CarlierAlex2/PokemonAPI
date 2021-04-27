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
    public class CsvContextTyping : CsvContext
    {
        public CsvContextTyping(CsvSettings csvSettings, IMapper mapper) 
        : base(csvSettings, mapper)
        {
            
        }

        protected override List<ModelObject> DoReadFromCsv()
        {
            using (var reader = new StreamReader(_csvSettings.CsvTyping))
            using (var csv = new CsvReader(reader, _csvConfiguration))
            {
                var records = csv.GetRecords<Typing>().ToList<Typing>();
                return records.Cast<ModelObject>().ToList();
            }     
        }

        protected override void DoWriteToCsv(List<ModelObject> listObject)
        {
            var records = listObject.Cast<Typing>().ToList();
            using (var writer = new StreamWriter(_csvSettings.CsvTyping))
            using (var csv = new CsvWriter(writer, _csvConfiguration))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
