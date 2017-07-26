using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models.Dirreciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class DistritoMunicipalesBLL
    {
        public static bool Insertar(DistritosMunicipales a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.DistritoId);
                    if (p == null)
                        db.DistritosMunicipales.Add(a);
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
        public static bool Eliminar(DistritosMunicipales nuevo)
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
        public static DistritosMunicipales Buscar(int Id)
        {
            var c = new DistritosMunicipales();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.DistritosMunicipales.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<DistritosMunicipales> GetLista()
        {
            var lista = new List<DistritosMunicipales>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.DistritosMunicipales.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<DistritosMunicipales> GetListaId(int Id)
        {
            List<DistritosMunicipales> list = new List<DistritosMunicipales>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.DistritosMunicipales.Where(p => p.DistritoId == Id).ToList();
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
