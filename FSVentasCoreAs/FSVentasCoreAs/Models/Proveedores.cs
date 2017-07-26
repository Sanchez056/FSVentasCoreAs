using FSVentasCoreAs.Models.Dirreciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models
{
    public class Proveedores
    {
        [Key]
        public int ProveedorId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Su Nombre Completo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "la Marca de Articulo")]
        [ForeignKey("MarcasArticulos")]
        public int MarcaId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Su Provincia")]
        [ForeignKey("Provincias")]
        public int ProvinciaId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = " Su Municipio")]
        [ForeignKey("Municipios")]
        public int MunicipioId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = " Su Distrito Municipal")]
        [ForeignKey("DistritosMunicipales")]
        public int DistritoId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = " Su Paraje")]
        [ForeignKey("Sectores")]
        public int SectorId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Su Direccion")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Su Numero Telefono ")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Su Numero de Fax")]
        public string Fax { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = "Su Correo Eletronico")]
        public string Correo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy - mm - dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public virtual MarcasArticulos MarcasArticulos { get; set; }
        public virtual Provincias Provincias { get; set; }
        public virtual Municipios Municipios { get; set; }
        public virtual DistritosMunicipales DistritosMunicipales { get; set; }
        public virtual Sectores Sectores { get; set; }
    }
}
