using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using CsvHelper;

using Pokemons.API.Configuration;
using Pokemons.API.Data.CsvStream.CsvData;


namespace Pokemons.API.Data.CsvStream
{
    public class CsvContextTyping : CsvContext
    {
        // Constructor //-------------------------------------------------------------------------------------------------------------------------------
        public CsvContextTyping(CsvSettings csvSettings) 
        : base(csvSettings)
        {
            
        }


        // CSV Functions //-------------------------------------------------------------------------------------------------------------------------------
        protected override List<CsvDataObject> DoReadFromCsv()
        {
            // Open stream
            using (var reader = new StreamReader(_csvSettings.CsvTyping))
            using (var csv = new CsvReader(reader, _csvConfiguration))
            {
                // Retrieve data from CSV
                var records = csv.GetRecords<TypingData>().ToList<CsvDataObject>();
                return records;
            }     
        }


        protected override void DoWriteToCsv(List<CsvDataObject> listObject)
        {
            // Cast data to corresponding Data type
            var records = listObject.Cast<TypingData>().ToList();

            // Open stream
            using (var writer = new StreamWriter(_csvSettings.CsvTyping))
            using (var csv = new CsvWriter(writer, _csvConfiguration))
            {
                // Write data to CSV
                csv.WriteRecords(records);
            }
        }
    }
}
