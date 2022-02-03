using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Omu.AwesomeMvc;
using PedidosManagement.Data;
using PedidosManagement.Model;
using PedidosManagement.Models;
using PedidosManagement.Models.ViewModel;
namespace PedidosManagement.Controllers
{
    public class OrdensController : Controller
    {
        private readonly PedidosContext _context;
        Response Response = new Response();

        public OrdensController(PedidosContext context)
        {
            _context = context;
        }

        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            var ListaOrdenes = await _context.Ordenes.Include("OrdenesDetalle").Include("Cliente").ToListAsync();
            List<VMProductosXPedido> ListaProductos = new List<VMProductosXPedido>();
            foreach (var item in ListaOrdenes)
            {
                string productos = "";
                foreach (var ordenDetalles in item.OrdenesDetalle)
                {
                    var producto = _context.Productos.Find(ordenDetalles.IdProducto);
                    productos = producto.Nombre + "" + productos;
                }

                ListaProductos.Add(new VMProductosXPedido
                {
                    IdPedido = item.ID,
                    NombresDeProductos = productos

                });

            }
            ViewBag.ListaProductosPedido = ListaProductos;
            return View(ListaOrdenes);
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.OrdenDetalles.Where(r => r.IdOrden == id).Include("Producto").ToListAsync();
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordens/Create
        public async Task<IActionResult> Create(int? id)
        {
            var IdCompra = id ?? 0;
            var _Compra = await _context.Ordenes.FindAsync(IdCompra);
            var Compra = new Orden();
            if (_Compra != null)
            {
                Compra = _Compra;
            }
            ViewBag.listaProductos = _context.Productos.Where(r => r.Estado == true).ToList();
            ViewBag.ListaClientes = _context.Clientes.Select(x => new { x.ID, Nombre = x.Nombre + " - " + x.NumeroTelefono + " - " + x.NickName, x.Estado }).Where(r => r.Estado == true).ToList();
            // Mandando datos al autocomplete

            //
            Tuple<Orden, OrdenDetalle> model = new Tuple<Orden, OrdenDetalle>(new Orden(), new OrdenDetalle());
            ViewBag.DetalleCompra = await _context.OrdenDetalles.Include(x => x.Producto).Where(x => x.IdOrden == IdCompra).ToListAsync();
            return View();
        }

        public IActionResult GetClientes(string v)// v is the entered text
        {
            v = (v ?? "").ToLower().Trim();

            var items = _context.Clientes.Where(o => o.Nombre.ToLower().Contains(v));

            return Json(items.Take(10).Select(o => new KeyContent(o.ID, o.Nombre)));
        }
        public async Task<IActionResult> SetEncabezado([Bind(Prefix = "Item1")] Orden Orden)
        {

            if (Orden.IdCliente == 0 || Orden.FechaOrden.ToString() == null)
            {
                Response.Estado = false;
                Response.Mensaje = "Rellene los campos solicitados";
                return Json(Response);
            }
            var _orden = await _context.Ordenes.FindAsync(Orden.ID);

            if (_orden == null)
            {
                _context.Ordenes.Add(Orden);
                await _context.SaveChangesAsync();
            }
            else
            {
                _orden.IdCliente = Orden.IdCliente;
                _orden.FechaOrden = Orden.FechaOrden;
                _orden.ID = Orden.ID;
                await _context.SaveChangesAsync();
            }

            Response.Resultado = Orden.ID;
            Response.Estado = true;
            return Json(Response);

        }

        public async void RemoveTrans(int id)
        {
            if (id != 0)
            {
                var lista = _context.OrdenDetalles.Where(r => r.IdOrden == id).ToList();
                foreach (var item in lista)
                {
                    // _context.OrdenDetalles.Remove(item);
                    _context.Entry(item).State = EntityState.Deleted;
                }
                _context.Entry(_context.Ordenes.Find(id)).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IActionResult> SetDetalle([Bind(Prefix = "Item2")] OrdenDetalle ordenDetalle)
        {
            if (!ModelState.IsValid)
            {
                Response.Estado = false;
                Response.Mensaje = "Rellene los campos solicitados";
                return Json(Response);
            }
            try
            {
                _context.OrdenDetalles.Add(ordenDetalle);

                await _context.SaveChangesAsync();
                ordenDetalle.Producto = await _context.Productos.FindAsync(ordenDetalle.IdProducto);
                Response.Estado = true;
                Response.Resultado = ordenDetalle;
                Response.Mensaje = "El producto ha sido agregado";
                return Json(Response);
            }
            catch (Exception ex)
            {
                return NotFound(Response);
            }

        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create()
        //{

        //}

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordenes.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdCliente,Total,FechaOrden,FechaCreacionAuto,estadoEntrega,Estado")] Orden orden)
        {
            if (id != orden.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.ID))
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
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordenes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.Ordenes.FindAsync(id);
            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
            return _context.Ordenes.Any(e => e.ID == id);
        }
    }
}
