using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSVentasCoreAs.BLL
{
    public class VentasDetallesBLL
    {
        public static bool Guardar(VentasDetalles detalle)
        {
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    db.VentasDetalles.Add(detalle);
                    if (db.SaveChanges() > 0)
                    {

                        return true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return false;
        }

        public static bool Modificar(VentasDetalles detalle)
        {
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    if (Buscar(detalle.Id) != null)
                    {
                        db.Entry(detalle).State = EntityState.Modified;
                        if (db.SaveChanges() > 0)
                            return true;
                    }
                    else
                    {
                        return Guardar(detalle);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return false;
        }
        public static VentasDetalles Buscar(int IdVent)
        {
            VentasDetalles Id = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    Id = db.VentasDetalles.Find(IdVent);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Id;
        }
        public static List<VentasDetalles> Listar(int? Id)
        {
            List<VentasDetalles> listado = null;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    listado = db.VentasDetalles.
                        Where(d => d.VentaId == Id).
                        OrderBy(d => d.Id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return listado;
        }
        public static bool Eliminar(VentasDetalles detalle)
        {
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    db.Entry(detalle).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return false;
        }
        public static List<VentasDetalles> GetLista()
        {
            var lista = new List<VentasDetalles>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    lista = db.VentasDetalles.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;

        }
        public static List<VentasDetalles> GetListaId(int Id)
        {
            List<VentasDetalles> list = new List<VentasDetalles>();
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    list = db.VentasDetalles.Where(p => p.Id == Id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return list;
        }

        public static bool Guardar(List<VentasDetalles> detalles)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    foreach (VentasDetalles detail in detalles)
                    {
                        db.VentasDetalles.Add(detail);
                        if (db.SaveChanges() > 0)
                        {

                            resultado = true;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return resultado;
        }
        public static bool Modificar(List<VentasDetalles> detalles, int ventId)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    //si no esta vacio
                    List<VentasDetalles> Iventas = db.VentasDetalles.Where(d => d.VentaId == ventId).ToList();
                    List<VentasDetalles> estaEnArreglo = new List<VentasDetalles>();
                    if (Iventas.Count > 0)
                    {
                        foreach (VentasDetalles detail in detalles)
                        {

                            foreach (var f in Iventas)
                            {
                                if (detail.Id == f.Id)
                                {
                                    estaEnArreglo.Add(f);
                                }
                            }
                        }
                        foreach (var d in estaEnArreglo)
                        {


                            db.Entry(d).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                        foreach (var e in Iventas)
                        {
                            bool found = false;

                            foreach (var inf in detalles)
                            {
                                if (e.Id == inf.Id)
                                {
                                    found = true;
                                    break;
                                }

                            }
                            if (!found)
                            {
                                db.VentasDetalles.Remove(e);
                                db.SaveChanges();
                            }
                        }

                        resultado = true;

                    }
                    else
                    {
                        foreach (var d in detalles)
                        {
                            db.VentasDetalles.Add(d);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return resultado;
            }
        }
        public static bool Eliminar(List<VentasDetalles> detalles)
        {
            bool resultado = false;
            using (var db = new FSVentasCoreDb())
            {
                try
                {
                    foreach (var detail in detalles)
                    {
                        db.Entry(detail).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return resultado;
            }


        }

    }
}
