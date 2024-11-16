using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Viajes.Data;
using Viajes.Models;

namespace Viajes.Controllers;

public class EstadisticasController : Controller
{
    private readonly ILogger<EstadisticasController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;
    public EstadisticasController(ILogger<EstadisticasController> logger,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        /*
        var itemsCarrito = from o in _context.DataItemCarrito select o;
            itemsCarrito = itemsCarrito.Where(s => s.Status.Equals("PENDIENTE"));*/
        var groupedItems=await _context.DataItemCarrito
        .GroupBy(i => i.Status)
        .Select(g => new { Status = g.Key, Count = g.Count() })
        .ToListAsync();
        //var allPedidos=_context.Data.ToListAsync();

        ViewData["GroupedItems"] = groupedItems;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
