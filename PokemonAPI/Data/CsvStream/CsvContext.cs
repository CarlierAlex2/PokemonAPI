using System;
using System.Globalization;
using System.Collections.Generic;

using AutoMapper;
using CsvHelper.Configuration;

using PokemonAPI.Configuration;
using PokemonAPI.Helpers;

namespace PokemonAPI.Data.CsvStream
{
    public abstract class CsvContext
    {
        protected readonly CsvSettings _csvSettings;
        protected readonly IMapper _mapper;
        protected readonly CsvConfiguration _csvConfiguration;
        
        public CsvContext(CsvSettings csvSettings, IMapper mapper)
        {
            _csvSettings = csvSettings;
            _mapper = mapper;
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = ";"
            };
        }

        //CSV Functions -------------------------------------------------------------------------------------------
        protected abstract List<ModelObject> DoReadFromCsv();
        public List<ModelObject> ReadFromCsv()
        {
            return DoReadFromCsv();
        }

        protected abstract void DoWriteToCsv(List<ModelObject> listObject);
        public void WriteToCsv(List<ModelObject> listObjects)
        {
            DoWriteToCsv(listObjects);
        }
    }
}
