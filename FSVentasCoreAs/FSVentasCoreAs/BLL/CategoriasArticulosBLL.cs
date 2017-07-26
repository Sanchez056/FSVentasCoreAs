using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class CategoriasArticulosBLL
    {
        public static bool Insertar(CategoriasArticulos a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.CategoriaId);
                    if (p == null)
                        db.CategoriasArticulos.Add(a);
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
        public static bool Eliminar(CategoriasArticulos nuevo)
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
        public static CategoriasArticulos Buscar(int Id)
        {
            var c = new CategoriasArticulos();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.CategoriasArticulos.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<CategoriasArticulos> GetLista()
        {
            var lista = new List<CategoriasArticulos>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.CategoriasArticulos.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<CategoriasArticulos> GetListaId(int Id)
        {
            List<CategoriasArticulos> list = new List<CategoriasArticulos>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.CategoriasArticulos.Where(p => p.CategoriaId == Id).ToList();
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
