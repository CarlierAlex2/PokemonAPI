using System;
using System.Globalization;
using System.Collections.Generic;

using CsvHelper.Configuration;

using Pokemons.API.Configuration;
using Pokemons.API.Data.CsvStream.CsvData;

namespace Pokemons.API.Data.CsvStream
{
    public interface ICsvContext
    {
        List<CsvDataObject> ReadFromCsv();
        void WriteToCsv(List<CsvDataObject> listObjects);
    }


    public abstract class CsvContext : ICsvContext
    {
        // Variables //-------------------------------------------------------------------------------------------------------------------------------
        protected readonly CsvSettings _csvSettings;
        protected readonly CsvConfiguration _csvConfiguration;


        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public CsvContext(CsvSettings csvSettings)
        {
            _csvSettings = csvSettings;
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = "\t"
            };
        }


        // CSV Functions //-------------------------------------------------------------------------------------------------------------------------------
        protected abstract List<CsvDataObject> DoReadFromCsv(); // function to overwrite by derived
        public List<CsvDataObject> ReadFromCsv() // execute reading from CSV
        {
            return DoReadFromCsv();
        }


        protected abstract void DoWriteToCsv(List<CsvDataObject> listObject); //function to overwrite by derived
        public void WriteToCsv(List<CsvDataObject> listObjects) // execute writing to CSV
        {
            DoWriteToCsv(listObjects);
        }
    }
}
