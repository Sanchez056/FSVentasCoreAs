using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        [Required]
        public int ArticuloId { get; set; }
        [Required]
        public string articulo { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public decimal Total { get; set; }

        public Ventas()
        {

        }
    }
}
