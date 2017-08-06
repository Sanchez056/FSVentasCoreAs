using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class VentasBLL
    {
        
        public static bool Guardar(Clases venta)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    db.Ventas.Add(venta.Encabezado);
                    if (db.SaveChanges() > 0)
                    {
                        resultado = BLL.VentasDetallesBLL.Guardar(venta.Detalle);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }
        

        public static Ventas BuscarEncabezado(int?ventaId)
        {
            Ventas factura = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    factura = db.Ventas.Find(ventaId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return factura;
        }
        public static Ventas Buscar(int Id)
        {
            Ventas ID = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    ID = db.Ventas.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return ID;
        }
        public static Clases Buscarr(int? facturaId)
        {
            Clases venta = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    venta = new Clases()
                    {
                        Encabezado = db.Ventas.Find(facturaId)
                    };
                    if (venta.Encabezado != null)
                    {
                        venta.Detalle = BLL.VentasDetallesBLL.Listar(venta.Encabezado.VentaId);
                    }
                    else
                    {
                        venta = null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return venta;
        }
        public static bool Modificar(Clases venta)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    db.Entry(venta.Encabezado).State = EntityState.Modified;

                    if (db.SaveChanges() > 0)
                    {
                        resultado = BLL.VentasDetallesBLL.Modificar(venta.Detalle, venta.Encabezado.VentaId);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }
        public static bool Eliminar(Clases venta)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    venta = VentasBLL.Buscarr(venta.Encabezado.VentaId);
                    VentasBLL.Eliminar(venta.Encabezado);
                    VentasDetallesBLL.Eliminar(venta.Detalle);

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }

        public static bool Eliminar(Ventas nuevo)
        {
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    db.Entry(nuevo).State = EntityState.Deleted;
                    if (db.SaveChanges() > 0)
                        return true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return false;
        }
        public static List<Ventas> Listar()
        {
            List<Ventas> listado = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    listado = db.Ventas.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return listado;
        }

    }
}
