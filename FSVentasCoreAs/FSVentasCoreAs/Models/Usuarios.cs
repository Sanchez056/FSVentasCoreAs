using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models
{
    public class Usuarios
    {
        [Key]
        [Remote("UsuarioDisponible", "Usuarios")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Nombre")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
        [Display(Name = "Tipo de Usuarios")]
        [ForeignKey("TipoUsuarios")]
        public int TipoId { get; set; }
        public virtual TipoUsuarios TipoUsuarios { get; set; }
    }
}
