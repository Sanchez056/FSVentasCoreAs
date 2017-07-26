using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models.Dirreciones
{
    public class DistritosMunicipales
    {
        [Key]
        public int DistritoId { get; set; }
        public string Nombre { get; set; }
        [ForeignKey("Municipios")]
        public int MunicipioId { get; set; }
        public Municipios Municipios { get; set; }
    }
}
