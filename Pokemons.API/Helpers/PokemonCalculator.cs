using System;
using System.Collections.Generic;
using System.Linq;

using Pokemons.API.Models;


namespace Pokemons.API.Helpers
{
    public class PokemonCalculator
    {
        // Get Pokemon Statistics List //-------------------------------------------------------------------------------------------------------------------------------
        public static PokemonStatisticsList Calculate_Statistics(List<Pokemon> listPokemon)
        {
            PokemonStatisticsList dictObj = new PokemonStatisticsList() { 
                Statistics = new Dictionary<string, PokemonStatistics>(),
                Names = listPokemon.Select(p => p.Name).ToList(),
            };

            dictObj.Statistics.Add("Hp", Calculate_Statistic_Hp(listPokemon));
            dictObj.Statistics.Add("Attack", Calculate_Statistic_Attack(listPokemon));
            dictObj.Statistics.Add("Defense", Calculate_Statistic_Defense(listPokemon));

            dictObj.Statistics.Add("SpAtk", Calculate_Statistic_SpAtk(listPokemon));
            dictObj.Statistics.Add("SpDef", Calculate_Statistic_SpDef(listPokemon));
            dictObj.Statistics.Add("Speed", Calculate_Statistic_Speed(listPokemon));

            return dictObj;
        }


        // Get Pokemon Statistics //-------------------------------------------------------------------------------------------------------------------------------
        public static PokemonStatistics Calculate_Statistic_Hp(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Hp).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Hp).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Hp).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics Calculate_Statistic_Attack(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Attack).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Attack).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Attack).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics Calculate_Statistic_Defense(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Defense).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Defense).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Defense).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics Calculate_Statistic_SpAtk(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.SpAtk).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.SpAtk).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.SpAtk).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics Calculate_Statistic_SpDef(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.SpDef).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.SpDef).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.SpDef).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics Calculate_Statistic_Speed(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Speed).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Speed).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Speed).DefaultIfEmpty(0).Max(),
            };
        }
    }
}
