using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using AutoMapper;
using CsvHelper;

using Pokemons.API.Models;
using Pokemons.API.DTO;
using Pokemons.API.Configuration;
using Pokemons.API.Helpers;

namespace Pokemons.API.Data.CsvStream
{
    public class CsvContextTypeEffect  : CsvContext
    {
        public CsvContextTypeEffect(CsvSettings csvSettings, IMapper mapper) 
        : base(csvSettings, mapper)
        {
            
        }

        //CSV Functions -------------------------------------------------------------------------------------------
        protected override List<ModelObject> DoReadFromCsv()
        {
            using (var reader = new StreamReader(_csvSettings.CsvTypeEffect))
            using (var csv = new CsvReader(reader, _csvConfiguration))
            {
                var records = csv.GetRecords<TypeEffectDTO>().ToList<TypeEffectDTO>();
                return records.Cast<ModelObject>().ToList();
            }   
        }

        protected override void DoWriteToCsv(List<ModelObject> listObject)
        {
            var records = listObject.Cast<TypeEffectDTO>().ToList();
            using (var writer = new StreamWriter(_csvSettings.CsvTypeEffect))
            using (var csv = new CsvWriter(writer, _csvConfiguration))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
