using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models.Dirreciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class ProvinciasBLL
    {
        public static bool Insertar(Provincias a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.ProvinciaId);
                    if (p == null)
                        db.Provincias.Add(a);
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
        public static bool Eliminar(Provincias nuevo)
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
        public static Provincias Buscar(int Id)
        {
            var c = new Provincias();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.Provincias.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<Provincias> GetLista()
        {
            var lista = new List<Provincias>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.Provincias.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Provincias> GetListaId(int Id)
        {
            List<Provincias> list = new List<Provincias>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Provincias.Where(p => p.ProvinciaId == Id).ToList();
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
