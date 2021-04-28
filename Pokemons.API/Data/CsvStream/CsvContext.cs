using System;
using System.Globalization;
using System.Collections.Generic;

using CsvHelper.Configuration;

using Pokemons.API.Configuration;
using Pokemons.API.Data.CsvStream.CsvData;

namespace Pokemons.API.Data.CsvStream
{
    public abstract class CsvContext
    {
        protected readonly CsvSettings _csvSettings;
        protected readonly CsvConfiguration _csvConfiguration;
        
        public CsvContext(CsvSettings csvSettings)
        {
            _csvSettings = csvSettings;
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture){
                HasHeaderRecord = true,
                Delimiter = "\t"
            };
        }

        //CSV Functions -------------------------------------------------------------------------------------------
        protected abstract List<CsvDataObject> DoReadFromCsv();
        public List<CsvDataObject> ReadFromCsv()
        {
            return DoReadFromCsv();
        }

        protected abstract void DoWriteToCsv(List<CsvDataObject> listObject);
        public void WriteToCsv(List<CsvDataObject> listObjects)
        {
            DoWriteToCsv(listObjects);
        }
    }
}
