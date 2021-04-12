using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class PokemonController : ControllerBase
    {
        #region Global Variables + Constructor
        //-----------------------------------------------------------------------------------------------------
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonService _pokemonService;

        public PokemonController(ILogger<PokemonController> logger, IPokemonService serviceTypes)
        {
            _pokemonService = serviceTypes;

            _logger = logger;
            _logger.LogInformation("ctor");
        }
        #endregion


        #region Controller Typing Methods
        //-----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<List<Typing>>> GetTypings()
        {
            try{
                List<Typing> results = await _pokemonService.GetTypings();
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("type/{typeName}")]
        public async Task<ActionResult<TypingDTO>> GetTypingByName(string typeName)
        {
            try{
                TypingDTO results = await _pokemonService.GetTypingByName(typeName);
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }
        #endregion

        #region Controller Pokemon Methods
        //-----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("pokemons")]
        public async Task<ActionResult<List<PokemonDTO>>> GetPokemons(string typeName = "")
        {
            try{
                //Type specified
                if(typeName!= null & typeName.Length > 0)
                {
                    List<PokemonDTO> results = await _pokemonService.GetPokemonByType(typeName);
                    return new OkObjectResult(results);
                }
                //Default
                else
                {
                    List<PokemonDTO> results = await _pokemonService.GetPokemons();
                    return new OkObjectResult(results);
                }
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("pokemon/id/{pokemonId}")]
        public async Task<ActionResult<PokemonDTO>> GetPokemonById(Guid pokemonId)
        {
            try{
                PokemonDTO results = await _pokemonService.GetPokemonById(pokemonId);
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("pokemon/entry/{pokedexEntry}")]
        public async Task<ActionResult<PokemonDTO>> GetPokemonByEntry(int pokedexEntry)
        {
            try{
                PokemonDTO results = await _pokemonService.GetPokemonByEntry(pokedexEntry);
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("pokemon")]
        public async Task<ActionResult<Pokemon>> AddPokemon(PokemonDTO pokemonDTO){
            try{
                var result = await _pokemonService.AddPokemon(pokemonDTO);
                _logger.LogInformation($"Pokemon was added - {result}");
                return new OkObjectResult(result);
            }
            catch (Exception ex){
                _logger.LogWarning($"Warning {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
        #endregion
    }
}
