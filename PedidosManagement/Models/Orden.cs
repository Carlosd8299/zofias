using PedidosManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosManagement.Model
{
    public class Orden
    {
        public Orden()
        {
            this.Estado = true;
            this.FechaCreacionAuto = DateTime.Now;
            OrdenesDetalle = new HashSet<OrdenDetalle>();
        }
        [Key]
        public int ID { get; set; }

        public int IdCliente { get; set; }
        public int? Total { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha estimada de entrega")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public DateTime FechaOrden { get; set; }
        public DateTime FechaCreacionAuto { get; set; } = DateTime.Now;
        public string EstadoEntrega { get; set; }
        public bool Estado { get; set; } = true;
        [ForeignKey("IdCliente")]
        [InverseProperty("Ordenes")]
        public virtual Cliente Cliente { get; set; }

        [InverseProperty("Orden")]
        public virtual ICollection<OrdenDetalle> OrdenesDetalle { get; set; }

    }

}
