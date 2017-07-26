using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class ProveedoresBLL
    {
        public static bool Insertar(Proveedores a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.ProveedorId);
                    if (p == null)
                        db.Proveedores.Add(a);
                    else
                        db.Entry(a).State = EntityState.Modified;
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
        public static bool Eliminar(Proveedores nuevo)
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
        public static Proveedores Buscar(int Id)
        {
            var c = new Proveedores();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.Proveedores.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<Proveedores> GetLista()
        {
            var lista = new List<Proveedores>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.Proveedores.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Proveedores> GetListaId(int Id)
        {
            List<Proveedores> list = new List<Proveedores>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Proveedores.Where(p => p.ProveedorId == Id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }
        public static List<Proveedores> GetListaFecha(DateTime Desde, DateTime Hasta)
        {
            List<Proveedores> lista = new List<Proveedores>();

            var db = new FSVentasCoreDb();

            lista = db.Proveedores.Where(p => p.Fecha >= Desde && p.Fecha <= Hasta).ToList();

            return lista;

        }


    }
}
