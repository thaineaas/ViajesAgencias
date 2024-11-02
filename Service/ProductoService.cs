using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Data;
using Viajes.Models;
using Microsoft.EntityFrameworkCore;

namespace Viajes.Service
{
    public class ProductoService
    {
         private readonly ILogger<ProductoService> _logger;
        private readonly ApplicationDbContext _context;

        public ProductoService(ILogger<ProductoService> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Producto>?> GetAll(){
            if(_context.DataProducto == null )
                return null;
            var productos = await _context.DataProducto.ToListAsync();
            _logger.LogInformation("Productos: {0}", productos);
            return productos;
        }

        public async Task<Producto?> Get(long? id){
            if (id == null || _context.DataProducto == null)
            {
                return null;
            }

            var producto = await _context.DataProducto.FindAsync(id);
            _logger.LogInformation("Producto: {0}", producto);
            if (producto == null)
            {
                return null;
            }
            return producto;
        }

        public async Task<Producto> CreateOrUpdate(Producto producto){
            //Regla de Negocio 1
            if(producto.Precio < 1){
                throw new SystemException("No se puede ingresar datos con precio menor 1 sol");
            }
            //Regla de Negocio 2
            _context.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task Delete(int? id){
            var producto = await _context.DataProducto.FindAsync(id);
            if (producto != null)
            {
                _context.DataProducto.Remove(producto);
            }
            await _context.SaveChangesAsync();
        }

        public bool ProductoExists(int id)
        {
            return (_context.DataProducto?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}