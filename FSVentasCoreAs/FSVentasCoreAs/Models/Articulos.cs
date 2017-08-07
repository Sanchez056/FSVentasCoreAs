using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.Models
{
    public class Articulos
    {
        [Key]
        public int ArticuloId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        //-----------
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        //-----------
        [Display(Name = " Descripcion ")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        //-----------
        [Display(Name = " Marca")]
        [ForeignKey("MarcasArticulos")]
        public int MarcaId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        //-----------
        [Display(Name = "Proveedor")]
        [ForeignKey("Proveedores")]
        public int ProveedorId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        //--------------
        [Display(Name = "Categorias")]
        [ForeignKey("Categorias")]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        //------------
        [Display(Name = "Cantidad Dispodible")]
        public int Cantidad { get; set; }
        [Display(Name = "Descuento el Articulo")]
        public double Descuento { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        //--------
        [Display(Name = "  Precio de Compra")]
        public decimal PrecioCompra { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = " Precio de Venta")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "Este Campo es Requerido")]
        [Display(Name = " Importe")]
        public double Importe { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy - mm - dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }


        public virtual CategoriasArticulos CategoriasArticulos { get; set; }
        public virtual Proveedores Proveedores { get; set; }
        public virtual MarcasArticulos MarcasArticulos { get; set; }

        public virtual ICollection<VentasDetalles> VentasDetalles { get; set; }

    }
}
