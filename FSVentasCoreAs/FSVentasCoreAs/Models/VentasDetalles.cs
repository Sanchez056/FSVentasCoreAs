using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models
{
    public class VentasDetalles
    {
        [Key]
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ArticuloId { get; set; }
        public string Articulos { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public double Descuento { get; set; }
        public decimal SubTotal { get; set; }
    }
}
