using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Viajes.Service;

namespace Viajes.Controllers.Rest
{

    [ApiController]
    [Route("api/categoria")]
    public class CategoriaApiRest : ControllerBase
    {
        private readonly ILogger<CategoriaApiRest> _logger;
        private readonly CategoriaService _categoriaService;

        public CategoriaApiRest(
            ILogger<CategoriaApiRest> logger, 
            CategoriaService categoriaService
            )
        {
            _logger = logger;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Categoria>>> GetProductos(){
            var categorias = await _categoriaService.GetAll();
            _logger.LogInformation("GetCategoriass{0}", categorias);
            if(categorias == null)
                return NotFound();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> GetProducto(long? id)
        {
            var categoria = await _categoriaService.Get(id);
            if(categoria == null)
                return NotFound();
            return Ok(categoria);
        }

  
    }
}