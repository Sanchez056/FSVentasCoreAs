using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models
{
    public class ClasesDetalle
    {
        public Ventas Encabezado { get; set; }

        public List<VentasDetalles> Detalle { get; set; }

        public ClasesDetalle()
        {
            Detalle = new List<VentasDetalles>();
        }
    }
}
