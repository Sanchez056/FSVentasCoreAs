using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class TipoUsuariosBLL
    {
        public static bool Insertar(TipoUsuarios a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.TipoId);
                    if (p == null)
                        db.TipoUsuarios.Add(a);
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
        public static bool Eliminar(TipoUsuarios nuevo)
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
        public static TipoUsuarios Buscar(int Id)
        {
            var c = new TipoUsuarios();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.TipoUsuarios.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<TipoUsuarios> GetLista()
        {
            var lista = new List<TipoUsuarios>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.TipoUsuarios.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<TipoUsuarios> GetListaId(int Id)
        {
            List<TipoUsuarios> list = new List<TipoUsuarios>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.TipoUsuarios.Where(p => p.TipoId == Id).ToList();
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
