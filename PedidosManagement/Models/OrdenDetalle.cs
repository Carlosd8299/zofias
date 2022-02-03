using PedidosManagement.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosManagement.Models
{
    public class OrdenDetalle
    {

        [Key]
        public int ID { get; set; }
        [ForeignKey("IdOrden")]
        [InverseProperty("OrdenesDetalle")]
        public int IdOrden { get; set; }
        public virtual Orden Orden { get; set; }

        public int IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }
        //
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Cantidad { get; set; }
        //
        [Required(ErrorMessage = "Este campo es requerido")]
        public decimal PrecioProducto { get; set; }
        //
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Color { get; set; }
        //
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Talla { get; set; }
        //
        public DateTime FechaCreacionAuto { get; set; } = DateTime.Now;
        public bool Estado { get; set; } = true;


    }
}
