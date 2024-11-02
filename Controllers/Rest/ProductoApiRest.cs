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
    [Route("api/producto")]
    public class ProductoApiRest : ControllerBase
    {
        private readonly ILogger<ProductoApiRest> _logger;
        private readonly ProductoService _productoService;

        public ProductoApiRest(
            ILogger<ProductoApiRest> logger, 
            ProductoService productoService
            )
        {
            _logger = logger;
            _productoService = productoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Producto>>> GetProductos(){
            var productos = await _productoService.GetAll();
            _logger.LogInformation("GetProductos{0}", productos);
            if(productos == null)
                return NotFound();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Producto>> GetProducto(long? id)
        {
            var producto = await _productoService.Get(id);
            if(producto == null)
                return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto){
            if (producto == null)
            {
                return BadRequest();
            }
            await _productoService.CreateOrUpdate(producto);
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int? id)
        {
            await _productoService.Delete(id);
            return Ok();
        }
    }
}