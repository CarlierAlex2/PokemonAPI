using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using PokemonAPI.Models;
using PokemonAPI.DTO;
using PokemonAPI.Services;


namespace PokemonAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api")]
    [ApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
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
        [MapToApiVersion("1.0")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
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
        [MapToApiVersion("1.0")]
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
        /// Get Pokemons by Pokedex entry.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/pokemons/entry/25
        ///
        /// </remarks>
        /// <param name="pokedexEntry">Pokemon's Pokedex entry</param>  
        [HttpGet]
        [Route("pokemons/entry/{pokedexEntry}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<List<PokemonDTO>>> GetPokemonByEntry(int pokedexEntry)
        {
            try{
                if(pokedexEntry <= 0)
                    return new BadRequestObjectResult("Pokedex Entry cannot be less than 1");

                List<PokemonDTO> results = await _pokemonService.GetPokemonByEntry(pokedexEntry);
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get a Pokemon by Pokedex entry and generation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/pokemon/entry/350/gen/3
        ///
        /// </remarks>
        /// <param name="pokedexEntry">Pokemon's Pokedex entry</param>  
        /// <param name="generation">Generation</param>  
        [HttpGet]
        [Route("pokemon/entry/{pokedexEntry}/gen/{generation}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PokemonDTO>> GetPokemonByEntryAndGen(int pokedexEntry, int generation)
        {
            try{
                if(pokedexEntry <= 0)
                    return new BadRequestObjectResult("Pokedex Entry cannot be less than 1");
                if(generation <= 0)
                    return new BadRequestObjectResult("Generation cannot be less than 1");

                PokemonDTO results = await _pokemonService.GetPokemonByEntryAndGen(pokedexEntry, generation);
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
        [MapToApiVersion("1.0")]
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

        /// <summary>
        /// Remove a Pokemon from the API
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/pokemons/entry/350
        ///
        ///     or
        ///
        ///     DELETE api/pokemons/entry/350?generation=3
        ///
        /// </remarks>
        /// <param name="pokedexEntry">Pokedex Entry</param>  
        /// <param name="generation">Generation</param>  
        [HttpDelete]
        [Route("pokemons/entry/{pokedexEntry}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> DeletePokemon(int pokedexEntry, int generation = -1){
            try{
                if(generation > 0)
                {
                    await _pokemonService.DeletePokemonByEntryAndGen(pokedexEntry, generation);
                    _logger.LogInformation($"Pokemons were removed - PokedexEntry:{pokedexEntry} - Generation{generation}");
                    return new OkResult();
                }
                else
                {
                    await _pokemonService.DeletePokemonByEntry(pokedexEntry);
                    _logger.LogInformation($"Pokemons were removed - PokedexEntry:{pokedexEntry}");
                    return new OkResult();
                }
            }
            catch (Exception ex){
                _logger.LogWarning($"Warning {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
        #endregion
    }
}
