using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class ClientesBLL
    {
        public static bool Insertar(Clientes a)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.ClienteId);
                    if (p == null)
                        db.Clientes.Add(a);
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
        public static bool Eliminar(Clientes nuevo)
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
        public static Clientes Buscar(int Id)
        {
            var c = new Clientes();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    c = db.Clientes.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<Clientes> GetLista()
        {
            var lista = new List<Clientes>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.Clientes.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Clientes> GetListaId(int Id)
        {
            List<Clientes> list = new List<Clientes>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Clientes.Where(p => p.ClienteId == Id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }
        public static List<Clientes> GetListaFecha(DateTime Desde, DateTime Hasta)
        {
            List<Clientes> lista = new List<Clientes>();

            var db = new FSVentasCoreDb();

            lista = db.Clientes.Where(p => p.Fecha >= Desde && p.Fecha <= Hasta).ToList();

            return lista;

        }


    }
}
