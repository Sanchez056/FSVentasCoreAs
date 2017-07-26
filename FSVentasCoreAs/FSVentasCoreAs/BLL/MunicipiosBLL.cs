using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models.Dirreciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class MunicipiosBLL
    {
        public static bool Insertar(Municipios a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.MunicipioId);
                    if (p == null)
                        db.Municipios.Add(a);
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
        public static bool Eliminar(Municipios nuevo)
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
        public static Municipios Buscar(int Id)
        {
            var c = new Municipios();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.Municipios.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<Municipios> GetLista()
        {
            var lista = new List<Municipios>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.Municipios.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Municipios> GetListaId(int Id)
        {
            List<Municipios> list = new List<Municipios>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Municipios.Where(p => p.MunicipioId == Id).ToList();
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
