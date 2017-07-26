using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class VentasBLL
    {
        public static bool Insertar(Ventas a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.VentaId);
                    if (p == null)
                        db.Ventas.Add(a);
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
        public static bool Eliminar(Ventas nuevo)
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
        public static Ventas Buscar(int Id)
        {
            var c = new Ventas();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.Ventas.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<Ventas> GetLista()
        {
            var lista = new List<Ventas>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.Ventas.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Ventas> GetListaId(int Id)
        {
            List<Ventas> list = new List<Ventas>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Ventas.Where(p => p.VentaId == Id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }
        public static List<Ventas> GetListaFecha(DateTime Desde, DateTime Hasta)
        {
            List<Ventas> lista = new List<Ventas>();

            var db = new FSVentasCoreDb();

            lista = db.Ventas.Where(p => p.Fecha >= Desde && p.Fecha <= Hasta).ToList();

            return lista;

        }
    }
}
