using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PedidosManagement.Data;
using PedidosManagement.Models;

namespace PedidosManagement.Controllers
{
    public class OrdenDetallesController : Controller
    {
        private readonly PedidosContext _context;

        public OrdenDetallesController(PedidosContext context)
        {
            _context = context;
        }

        // GET: OrdenDetalles
        public async Task<IActionResult> Index()
        {
            var pedidosContext = _context.OrdenDetalles.Include(o => o.Orden).Include(o => o.Producto);
            return View(await pedidosContext.ToListAsync());
        }

        // GET: OrdenDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDetalle = await _context.OrdenDetalles
                .Include(o => o.Orden)
                .Include(o => o.Producto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordenDetalle == null)
            {
                return NotFound();
            }

            return View(ordenDetalle);
        }

        // GET: OrdenDetalles/Create
        public IActionResult Create()
        {
            ViewData["IdOrden"] = new SelectList(_context.Ordenes, "ID", "ID");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "Nombre");
            return View();
        }

        // POST: OrdenDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdOrden,IdProducto,Cantidad,PrecioProducto,Color,Talla,FechaCreacionAuto,Estado")] OrdenDetalle ordenDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOrden"] = new SelectList(_context.Ordenes, "ID", "ID", ordenDetalle.IdOrden);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "Nombre", ordenDetalle.IdProducto);
            return View(ordenDetalle);
        }

        // GET: OrdenDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDetalle = await _context.OrdenDetalles.FindAsync(id);
            if (ordenDetalle == null)
            {
                return NotFound();
            }
            ViewData["IdOrden"] = new SelectList(_context.Ordenes, "ID", "ID", ordenDetalle.IdOrden);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "Nombre", ordenDetalle.IdProducto);
            ViewBag.IdOrdenDetalle = ordenDetalle.IdOrden;
            return View(ordenDetalle);
        }

        // POST: OrdenDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdOrden,IdProducto,Cantidad,PrecioProducto,Color,Talla,FechaCreacionAuto,Estado")] OrdenDetalle ordenDetalle)
        {
            if (id != ordenDetalle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenDetalleExists(ordenDetalle.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Ordens", new {/* routeValues, for example: */ id = ordenDetalle.IdOrden } );
            }
            ViewData["IdOrden"] = new SelectList(_context.Ordenes, "ID", "ID", ordenDetalle.IdOrden);
            ViewData["IdProducto"] = _context.Productos.Find(ordenDetalle.IdProducto).Nombre;
            ViewBag.IdOrdenDetalle = ordenDetalle.IdOrden;
            return View(ordenDetalle);
        }

        // GET: OrdenDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDetalle = await _context.OrdenDetalles
                .Include(o => o.Orden)
                .Include(o => o.Producto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordenDetalle == null)
            {
                return NotFound();
            }

            return View(ordenDetalle);
        }

        // POST: OrdenDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenDetalle = await _context.OrdenDetalles.FindAsync(id);
            _context.OrdenDetalles.Remove(ordenDetalle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Ordens", new {/* routeValues, for example: */ id = ordenDetalle.IdOrden });
        }

        private bool OrdenDetalleExists(int id)
        {
            return _context.OrdenDetalles.Any(e => e.ID == id);
        }
    }
}
