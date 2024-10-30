using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Viajes.Data;
using Viajes.Models;
using Viajes.Helper;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Viajes.Controllers
{
 
    public class CarritoController : Controller
    {
        private readonly ILogger<CarritoController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;


        public CarritoController(ILogger<CarritoController> logger,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var userIDSession = _userManager.GetUserName(User);
            if(userIDSession == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                return RedirectToAction("Index","Catalogo");
            }
            Console.WriteLine("Encontro usuario");
            var items = from o in _context.DataItemCarrito select o;
            items = items.Include(p => p.Producto).
                    Where(w => w.UserID.Equals(userIDSession) &&
                        w.Status.Equals("PENDIENTE"));
            var itemsCarrito = items.ToList();
            var total = itemsCarrito.Sum(c => c.Cantidad * c.Precio);

            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.elementosCarrito = itemsCarrito;
            return View(model);
        }

        public async Task<IActionResult> Add(long? id){
            var userName = _userManager.GetUserName(User);
            if(userName == null){
                            Console.WriteLine("No usuario");

               _logger.LogInformation("No existe usuario");
               ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
               return RedirectToAction("Index","Catalogo");
            }else{
                Console.WriteLine("Encontro usuario");

                //obtengo el carrito de memoria
                List<ItemCarrito> carrito = Helper.SessionExtensions.Get<List<ItemCarrito>>(HttpContext.Session, "carritoSesion");
                if(carrito == null){
                    carrito = new List<ItemCarrito>();
                }
                //obtengo los datos del producto
                var producto = await _context.DataProducto.FindAsync(id);
                ItemCarrito itemCarrito = new ItemCarrito();
                itemCarrito.Producto = producto;
                itemCarrito.UserID = userName;
                itemCarrito.Cantidad = 1;
                itemCarrito.Precio=producto.Precio*1;
                _context.DataItemCarrito.Add(itemCarrito);
                await _context.SaveChangesAsync();

                carrito.Add(itemCarrito);
                //seteo el carrito en memoria
                Helper.SessionExtensions.Set<List<ItemCarrito>>(HttpContext.Session, "carritoSesion",carrito);
                 ViewData["Message"] = "Se Agrego al carrito";
                _logger.LogInformation("Se agrego un producto al carrito");
                return RedirectToAction("Index","Catalogo");
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCarrito = await _context.DataItemCarrito.FindAsync(id);
            _context.DataItemCarrito.Remove(itemCarrito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCarrito = await _context.DataItemCarrito.FindAsync(id);
            if (itemCarrito == null)
            {
                return NotFound();
            }
            return View(itemCarrito);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,Precio,UserID")] ItemCarrito itemCarrito)
        {
            if (id != itemCarrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCarrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DataItemCarrito.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemCarrito);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}