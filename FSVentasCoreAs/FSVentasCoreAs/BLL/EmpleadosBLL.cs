using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class EmpleadosBLL
    {
        public static bool Insertar(Empleados e)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    var p = Buscar(e.Empleadod);
                    if (p == null)
                        db.Empleados.Add(e);
                    else
                        db.Entry(e).State = EntityState.Modified;
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
        public static bool Eliminar(Empleados nuevo)
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
        public static Empleados Buscar(int? empleadoId)
        {
            Empleados empleado = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    empleado = db.Empleados.Find(empleadoId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return empleado;
        }
        public static List<Empleados> GetLista()
        {
            var lista = new List<Empleados>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.Empleados.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<Empleados> GetListaId(int Id)
        {
            List<Empleados> list = new List<Empleados>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.Empleados.Where(p => p.Empleadod == Id).ToList();
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
