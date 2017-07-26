using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models.Dirreciones
{
    public class Provincias
    {
        [Key]
        public int ProvinciaId { get; set; }
        public string Nombre { get; set; }
        [NotMapped]
        public int MunicipioId { get; set; }
    }
}
