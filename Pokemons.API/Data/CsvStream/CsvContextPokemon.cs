using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using CsvHelper;

using Pokemons.API.Configuration;
using Pokemons.API.Data.CsvStream.CsvData;

namespace Pokemons.API.Data.CsvStream
{
    public class CsvContextPokemon : CsvContext
    {
        public CsvContextPokemon(CsvSettings csvSettings) 
        : base(csvSettings)
        {
            
        }

        //CSV Functions -------------------------------------------------------------------------------------------
        protected override List<CsvDataObject> DoReadFromCsv()
        {
            using (var reader = new StreamReader(_csvSettings.CsvPokemon))
            using (var csv = new CsvReader(reader, _csvConfiguration))
            {
                var records = csv.GetRecords<PokemonData>().ToList<CsvDataObject>();
                return records;
            }   
        }

        protected override void DoWriteToCsv(List<CsvDataObject> listObject)
        {
            var records = listObject.Cast<PokemonData>().ToList();
            using (var writer = new StreamWriter(_csvSettings.CsvPokemon))
            using (var csv = new CsvWriter(writer, _csvConfiguration))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
