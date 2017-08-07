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
        public int ClienteId { get; set; }
        public string Clientes { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int EmpleadoId { get; set; }
        public string Empleados { get; set; }
    }
}
