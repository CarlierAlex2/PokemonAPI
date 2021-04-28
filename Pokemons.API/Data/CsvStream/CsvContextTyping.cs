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
        public CsvContextTyping(CsvSettings csvSettings) 
        : base(csvSettings)
        {
            
        }

        protected override List<CsvDataObject> DoReadFromCsv()
        {
            using (var reader = new StreamReader(_csvSettings.CsvTyping))
            using (var csv = new CsvReader(reader, _csvConfiguration))
            {
                var records = csv.GetRecords<TypingData>().ToList<CsvDataObject>();
                return records;
            }     
        }

        protected override void DoWriteToCsv(List<CsvDataObject> listObject)
        {
            var records = listObject.Cast<TypingData>().ToList();
            using (var writer = new StreamWriter(_csvSettings.CsvTyping))
            using (var csv = new CsvWriter(writer, _csvConfiguration))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
