using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosManagement.Model
{
    public class Cliente
    {
        public Cliente()
        {
            Ordenes = new HashSet<Orden>();
        }

        [Key]
        public int ID { get; set; }
        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Numero de identificacion")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string NumeroIdentificacion { get; set; }

        [Display(Name = "Usuario de Instagram")]
        public string? NickName { get; set; }

        [Display(Name = "Direccion de entrega")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string DireccionEntrega { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Departamento { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Ciudad { get; set; }

        [Display(Name = "Numero de contacto o Whatsapp")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string NumeroTelefono { get; set; }
        public DateTime FechaCreacionAuto { get; set; } = DateTime.Now;
        public bool Estado { get; set; } = true;

        [InverseProperty("Cliente")]
        public virtual ICollection<Orden> Ordenes { get; set; }
    }
}
