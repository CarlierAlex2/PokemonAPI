using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using Pokemons.API.Models;
using Pokemons.API.DTO;
using Pokemons.API.Services;
using Pokemons.API.Helpers;

namespace Pokemons.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class PokemonController
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
        public async Task<ActionResult<List<Typing>>> GetTypings_V1()
        {
            try{
                List<Typing> results = await _pokemonService.GetTypings_V1();
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

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
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<List<TypingBaseDTO>>> GetTypings_V2()
        {
            try{
                List<TypingBaseDTO> results = await _pokemonService.GetTypings_V2();
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get a list of all available Pokemon Types.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/types/list
        ///
        /// </remarks>
        [HttpGet]
        [Route("types/list")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<TypingList>> GetTypings_List()
        {
            try{
                TypingList results = await _pokemonService.GetTypings_List();
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
        [MapToApiVersion("2.0")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<TypingDTO>> GetTyping_ByName(string typeName)
        {
            try{
                TypingDTO results = await _pokemonService.GetTyping_ByName(typeName);
                if(results == null)
                    return new BadRequestObjectResult($"Could not find type named: {typeName}");

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
        /// Get a list of Pokemon with some details, with the option to specify a Pokemon Type.
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
        public async Task<ActionResult<List<PokemonDTO>>> GetPokemons_V1(string typeName = "")
        {
            try{
                //Type specified
                if(typeName!= null & typeName.Length > 0)
                {
                    List<PokemonDTO> results = await _pokemonService.GetPokemon_ByType_V1(typeName);
                    if(results == null)
                        return new BadRequestObjectResult("No pokemon were found");
                        
                    return new OkObjectResult(results);
                }
                //Default
                else
                {
                    List<PokemonDTO> results = await _pokemonService.GetPokemons_V1();
                    return new OkObjectResult(results);
                }
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Get a list of Pokemon with some details, with the option to specify a Pokemon Type.
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
        [MapToApiVersion("2.0")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<List<PokemonBaseDTO>>> GetPokemons_V2(string typeName = "")
        {
            try{
                //Type specified
                if(typeName.Length > 0)
                {
                    var results = await _pokemonService.GetPokemon_ByType_V2(typeName);
                    if(results == null)
                        return new BadRequestObjectResult("No pokemon were found");

                    return new OkObjectResult(results);
                }
                //Default
                else
                {
                    var results = await _pokemonService.GetPokemons_V2();
                    return new OkObjectResult(results);
                }
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }


        /// <summary>
        /// Get a list of Pokemon names, with the option to specify a Pokemon Type.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/pokemons/list
        ///
        ///     or
        ///
        ///     GET api/pokemons/list?typeName=Water
        ///
        /// </remarks>
        /// <param name="typeName">Name of the type</param>  
        [HttpGet]
        [Route("pokemons/list")]
        [MapToApiVersion("2.0")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<PokemonList>> GetPokemons_List(string typeName = "")
        {
            try{
                //Type specified
                if(typeName.Length > 0)
                {
                    var results = await _pokemonService.GetPokemons_List_ByType(typeName);
                    if(results == null)
                        return new BadRequestObjectResult("No pokemon were found");

                    return new OkObjectResult(results);
                }
                //Default
                else
                {
                    var results = await _pokemonService.GetPokemons_List();
                    return new OkObjectResult(results);
                }
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }


        /// <summary>
        /// Get Pokemons by name.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/pokemons/name/25
        ///
        /// </remarks>
        /// <param name="name">Pokemon's name</param>  
        [HttpGet]
        [Route("pokemons/name/{name}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<List<PokemonDTO>>> GetPokemon_ByName(string name)
        {
            try{
                if(name.Length < 3)
                    return new BadRequestObjectResult("Pokedex name not long enough");

                PokemonDTO results = await _pokemonService.GetPokemon_ByName(name);
                if(results == null)
                    return new BadRequestObjectResult($"No pokemon were found with given name - {name}");
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
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<List<PokemonDTO>>> GetPokemons_ByEntry(int pokedexEntry)
        {
            try{
                if(pokedexEntry <= 0)
                    return new BadRequestObjectResult("Pokedex Entry cannot be less than 1");

                List<PokemonDTO> results = await _pokemonService.GetPokemons_ByEntry(pokedexEntry);
                if(results == null)
                    return new BadRequestObjectResult("No pokemon were found with given parameters");
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
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<PokemonDTO>> GetPokemon_ByEntryAndGen(int pokedexEntry, int generation)
        {
            try{
                if(pokedexEntry <= 0)
                    return new BadRequestObjectResult("Pokedex Entry cannot be less than 1");
                if(generation <= 0)
                    return new BadRequestObjectResult("Generation cannot be less than 1");

                PokemonDTO results = await _pokemonService.GetPokemon_ByEntryAndGen(pokedexEntry, generation);
                if(results == null)
                    return new BadRequestObjectResult("No pokemon were found with given parameters");
                return new OkObjectResult(results);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }


        [HttpPost]
        [Route("pokemons/statistics")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<PokemonStatisticsList>> GetPokemons_Statistics([FromBody] List<string> names = null)
        {
            try{
                if(names == null)
                    return new BadRequestObjectResult("No list of Pokemon names were given - No list found");
                if(names.Count <= 0)
                    return new BadRequestObjectResult("No list of Pokemon names were given - List is empty");

                var results = await _pokemonService.GetPokemons_Statistics(names);
                if(results == null)
                    return new BadRequestObjectResult("No pokemon were found with given parameters");
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
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<Pokemon>> AddPokemon(PokemonDTO pokemonDTO){
            try{
                // Clean + verify
                pokemonDTO = PokemonHelper.CleanupPokemonDTO(pokemonDTO);
                var verify = await _pokemonService.VerifyPokemonDTO(pokemonDTO);
                if(verify.Item1 == false)
                    return new BadRequestObjectResult($"The provided values cannot be used to create a Pokemon - {verify.Item2}");
                
                // Add pokemon
                var result = await _pokemonService.AddPokemon(pokemonDTO);
                if(result == null)
                    return new BadRequestObjectResult("Pokemon with given entry and generation already exists in database");
                
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
        [MapToApiVersion("2.0")]
        public async Task<ActionResult> DeletePokemon(int pokedexEntry, int generation = -1){
            try{
                if(generation > 0)
                {
                    await _pokemonService.DeletePokemon_ByEntryAndGen(pokedexEntry, generation);
                    _logger.LogInformation($"Pokemons were removed - PokedexEntry:{pokedexEntry} - Generation{generation}");
                    return new OkResult();
                }
                else
                {
                    await _pokemonService.DeletePokemon_ByEntry(pokedexEntry);
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
