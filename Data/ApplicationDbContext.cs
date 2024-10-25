﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Viajes.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Viajes.Models.Cliente> DataCliente {get; set; }

    public DbSet<Viajes.Models.Contacto> DataContacto {get; set; }
    public DbSet<Viajes.Models.Producto> DataProducto {get; set; }

    public DbSet<Viajes.Models.Categoria> DataCategoria {get; set; }
}
