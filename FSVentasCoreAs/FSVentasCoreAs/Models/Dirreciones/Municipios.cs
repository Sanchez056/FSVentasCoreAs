using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models.Dirreciones
{
    public class Municipios
    {
        [Key]
        public int MunicipioId { get; set; }
        public string Nombre { get; set; }

        [ForeignKey("Provincias")]
        public int ProvinciaId { get; set; }
        public virtual Provincias Provincias { get; set; }
    }
}
