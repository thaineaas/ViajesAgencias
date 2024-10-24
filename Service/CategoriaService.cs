using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Data;
using Viajes.Models;
using Microsoft.EntityFrameworkCore;

namespace Viajes.Service
{
    public class CategoriaService
    {
         private readonly ILogger<CategoriaService> _logger;
        private readonly ApplicationDbContext _context;

        public CategoriaService(ILogger<CategoriaService> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Categoria>?> GetAll(){
            if(_context.DataCategoria == null )
                return null;
            var categorias = await _context.DataCategoria.ToListAsync();
            _logger.LogInformation("Categorias: {0}", categorias);
            return categorias;
        }

        public async Task<Categoria?> Get(long? id){
            if (id == null || _context.DataCategoria == null)
            {
                return null;
            }

            var categoria = await _context.DataCategoria.FindAsync(id);
            _logger.LogInformation("Categoria: {0}", categoria);
            if (categoria == null)
            {
                return null;
            }
            return categoria;
        }


    }
}