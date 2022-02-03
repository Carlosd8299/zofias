using Microsoft.EntityFrameworkCore;
using PedidosManagement.Model;
using PedidosManagement.Models;

namespace PedidosManagement.Data
{
    public class PedidosContext : DbContext
    {
        public PedidosContext(DbContextOptions<PedidosContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalles { get; set; }

        
    }
}
