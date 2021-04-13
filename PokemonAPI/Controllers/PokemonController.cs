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
        
        /// <summary>
        /// Get a list of all available Pokemon Types.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/types
        ///
        /// </remarks>
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

        /// <summary>
        /// Get a detailed summary of one specified Pokemon Type.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/types/Water
        ///
        /// </remarks>
        /// <param name="typeName">Name of the type</param>   
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
        
        /// <summary>
        /// Get a list of Pokemon, with the option to specify a Pokemon Type.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/pokemons
        ///
        ///     or
        ///
        ///     GET api/pokemons?typeName=Water
        ///
        /// </remarks>
        /// <param name="typeName">Name of the type</param>  
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

        /// <summary>
        /// Get a Pokemon by ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/pokemons/id/9ae91a19-ba47-4506-ab97-fe20718b9bea
        ///
        /// </remarks>
        /// <param name="pokemonId">Pokemon ID within the database</param>  
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

        /// <summary>
        /// Get a Pokemon by Pokedex entry.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/pokemons/entry/25
        ///
        /// </remarks>
        /// <param name="pokedexEntry">Pokemon's Pokedex entry</param>  
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

        /// <summary>
        /// Add a Pokemon to the API
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/pokemon
        ///     {
        ///     "pokedexEntry": 350,
        ///     "name": "Milotic",
        ///     "generation": 3,
        ///     "types": [
        ///         "Water"
        ///     ],
        ///     "classification": "Tender Pokemon",
        ///     "eggGroup": "Water 1, Dragon"
        ///
        /// </remarks>
        /// <param name="pokemonDTO">Pokemon to add</param>  
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
