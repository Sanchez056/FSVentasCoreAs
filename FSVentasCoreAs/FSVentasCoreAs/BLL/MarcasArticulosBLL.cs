using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class MarcasArticulosBLL
    {
        public static bool Insertar(MarcasArticulos a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.MarcaId);
                    if (p == null)
                        db.MarcasArticulos.Add(a);
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
        public static bool Eliminar(MarcasArticulos nuevo)
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
        public static MarcasArticulos Buscar(int Id)
        {
            var c = new MarcasArticulos();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.MarcasArticulos.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<MarcasArticulos> GetLista()
        {
            var lista = new List<MarcasArticulos>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.MarcasArticulos.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<MarcasArticulos> GetListaId(int Id)
        {
            List<MarcasArticulos> list = new List<MarcasArticulos>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.MarcasArticulos.Where(p => p.MarcaId == Id).ToList();
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
