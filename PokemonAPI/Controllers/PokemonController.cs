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
        private readonly IPokemonService _serviceTypes;

        public PokemonController(ILogger<PokemonController> logger, IPokemonService serviceTypes)
        {
            _serviceTypes = serviceTypes;

            _logger = logger;
            _logger.LogInformation("ctor");
        }
        #endregion


        #region Controller Methods
        //-----------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<List<PokemonType>>> GetPokemonTypes()
        {
            try{
                List<PokemonType> list = await _serviceTypes.GetPokemonTypes();
                return new OkObjectResult(list);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("type/{typeName}")]
        public async Task<ActionResult<PokemonTypeDTO>> GetPokemonTypeDetail(string typeName)
        {
            try{
                PokemonTypeDTO pokemonType = await _serviceTypes.GetPokemonTypeDetail(typeName);
                return new OkObjectResult(pokemonType);
            }
            catch(Exception ex){
                _logger.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }
        #endregion
    }
}
