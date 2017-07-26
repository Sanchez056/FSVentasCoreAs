using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models.Dirreciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class SectoresBLL
    {
        public static bool Insertar(Sectores a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.SectorId);
                    if (p == null)
                        db.Sectores.Add(a);
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
        public static bool Eliminar(Sectores nuevo)
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
        public static Sectores Buscar(int Id)
        {
            var c = new Sectores();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.Sectores.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<Sectores> GetLista()
        {
            var lista = new List<Sectores>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.Sectores.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Sectores> GetListaId(int Id)
        {
            List<Sectores> list = new List<Sectores>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Sectores.Where(p => p.SectorId == Id).ToList();
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
