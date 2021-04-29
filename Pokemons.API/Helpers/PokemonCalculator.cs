using System;
using System.Collections.Generic;
using System.Linq;

using Pokemons.API.Models;

namespace Pokemons.API.Helpers
{
    public class PokemonCalculator
    {
        public static PokemonStatisticsList GetPokemonStatistics(List<Pokemon> listPokemon)
        {
            PokemonStatisticsList dictObj = new PokemonStatisticsList() { 
                Statistics = new Dictionary<string, PokemonStatistics>(),
                Names = listPokemon.Select(p => p.Name).ToList(),
            };

            dictObj.Statistics.Add("Hp", GetPokemonStatistics_Hp(listPokemon));

            dictObj.Statistics.Add("Attack", GetPokemonStatistics_Attack(listPokemon));
            dictObj.Statistics.Add("Defense", GetPokemonStatistics_Defense(listPokemon));

            dictObj.Statistics.Add("SpAtk", GetPokemonStatistics_SpAtk(listPokemon));
            dictObj.Statistics.Add("SpDef", GetPokemonStatistics_SpDef(listPokemon));

            dictObj.Statistics.Add("Speed", GetPokemonStatistics_Speed(listPokemon));

            return dictObj;
        }

        public static PokemonStatistics GetPokemonStatistics_Hp(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Hp).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Hp).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Hp).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics GetPokemonStatistics_Attack(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Attack).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Attack).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Attack).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics GetPokemonStatistics_Defense(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Defense).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Defense).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Defense).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics GetPokemonStatistics_SpAtk(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.SpAtk).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.SpAtk).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.SpAtk).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics GetPokemonStatistics_SpDef(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.SpDef).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.SpDef).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.SpDef).DefaultIfEmpty(0).Max(),
            };
        }

        public static PokemonStatistics GetPokemonStatistics_Speed(List<Pokemon> listPokemon)
        {
            return new PokemonStatistics(){
                Minimum = listPokemon.Select(p => p.Speed).DefaultIfEmpty(0).Min(),
                Average = listPokemon.Select(p => p.Speed).DefaultIfEmpty(0).Average(),
                Maximum = listPokemon.Select(p => p.Speed).DefaultIfEmpty(0).Max(),
            };
        }
    }
}
