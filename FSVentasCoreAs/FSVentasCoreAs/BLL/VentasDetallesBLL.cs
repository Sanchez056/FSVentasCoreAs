using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class VentasDetallesBLL
    {
        public static bool Guardar(List<VentasDetalles> detalles)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    foreach (VentasDetalles detail in detalles)
                    {
                        db.VentasDetalles.Add(detail);
                        db.SaveChanges();
                        resultado = true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }

        public static bool Eliminar(VentasDetalles nuevo)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    db.Entry(nuevo).State = EntityState.Deleted;
                    db.SaveChanges();
                    resultado = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }
        public static VentasDetalles Buscar(int Id)
        {
            var c = new VentasDetalles();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.VentasDetalles.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<VentasDetalles> GetLista()
        {
            var lista = new List<VentasDetalles>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.VentasDetalles.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }

        public static List<VentasDetalles> Listar()
        {
            List<VentasDetalles> listado = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    listado = db.VentasDetalles.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return listado;
        }

        public static List<VentasDetalles> Listar(int? id)
        {
            List<VentasDetalles> listado = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    listado = db.VentasDetalles.Where(d => d.Id == id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return listado;
        }
        public static List<VentasDetalles> GetListaId(int id)
        {
            List<VentasDetalles> list = new List<VentasDetalles>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.VentasDetalles.Where(p => p.Id == id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }
    }
}
