using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class ArticulosBLL
    {
        public static bool Insertar(Articulos a)
        {
            bool resultado = false;
            using (var Conn = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(a.ArticuloId);
                    if (p == null)
                        Conn.Articulos.Add(a);
                    else
                        Conn.Entry(a).State = EntityState.Modified;
                    Conn.SaveChanges();
                    resultado = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }
        public static bool Eliminar(Articulos nuevo)
        {
            bool resultado = false;
            using (var Conn = new FSVentasCoreDb())
            {
                try
                {
                    Conn.Entry(nuevo).State = EntityState.Deleted;
                    Conn.SaveChanges();
                    resultado = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }
        public static Articulos Buscar(int Id)
        {
            var c = new Articulos();
            using (var Conn = new FSVentasCoreDb())
            {
                try
                {
                    c = Conn.Articulos.Find(Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return c;
        }
        public static List<Articulos> GetLista()
        {
            var lista = new List<Articulos>();
            using (var conexion = new FSVentasCoreDb())
            {
                try
                {
                    lista = conexion.Articulos.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Articulos> GetListaId(int Id)
        {
            List<Articulos> list = new List<Articulos>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Articulos.Where(p => p.ArticuloId == Id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }
        public static List<Articulos> ListarArticulos()
        {
            List<Articulos> listado = null;
            using (var conexion = new FSVentasCoreDb())
            {
                try
                {
                    listado = conexion.Articulos.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return listado;
        }
        public static List<Articulos> GetListaFecha(DateTime Desde, DateTime Hasta)
        {
            List<Articulos> lista = new List<Articulos>();

            var db = new FSVentasCoreDb();

            lista = db.Articulos.Where(p => p.Fecha >= Desde && p.Fecha <= Hasta).ToList();

            return lista;

        }
    }
}
