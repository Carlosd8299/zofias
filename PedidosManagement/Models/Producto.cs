using PedidosManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosManagement.Model
{
    public class Producto
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y maximo {1} caracteres", MinimumLength = 5)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //public string? Talla { get; set; }
        //public string? Color { get; set; }
        //[Required(ErrorMessage = "Este campo es requerido")]
        //public double Precio { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public bool Outlet { get; set; }

        //[DefaultValue(true)]
        public DateTime FechaCreacionAuto { get; set; } = DateTime.Now;
        public bool Estado { get; set; } = true;

    }
}
