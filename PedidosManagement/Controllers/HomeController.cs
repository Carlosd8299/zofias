using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using PedidosManagement.Data;
using System.Linq;

namespace PedidosManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly PedidosContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, PedidosContext contex)
        {
            _logger = logger;
            _context = contex;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ConsultasEntity()
        {
            /*
             * Clausula in
             * Select * from productos where Id in (4,6,8)
             * select * from productos where nombre like "%Sir%"
             * select * from productos where nombre like "%Sir%"
             * select * from productos where nombre like "%Sir%"
             */

            ViewBag.Productos = await _context.Productos.Where(x => new int[] { 4, 6, 8 }.Contains(x.ID)).ToListAsync();
            ViewBag.Productos = await _context.Productos.Where(x => x.Nombre.Contains("Sir")).ToListAsync();
            ViewBag.Productos = await _context.Productos.Where(x => x.Nombre.StartsWith("Sir")).ToListAsync();
            ViewBag.Productos = await _context.Productos.Where(x => x.Nombre.EndsWith("Sir")).ToListAsync();
            ViewBag.Productos = await _context.Productos.Where(x => x.Nombre.EndsWith("Sir") && x.Nombre.Contains("aDe")).ToListAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
