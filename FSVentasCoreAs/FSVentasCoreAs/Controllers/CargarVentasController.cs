using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSVentasCoreAs.BLL;
using FSVentasCoreAs.Models;

namespace FSVentasCoreAs.Controllers
{
    public class CargarVentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Buscar(int ventaId)
        {
            Ventas ventas = VentasBLL.Buscar(ventaId);
            return Json(ventas);
        }
        [HttpGet]
        public JsonResult BuscarF(int ventaId)
        {
            var venta = VentasBLL.Listar();
            return Json(venta);
        }
        [HttpGet]
        public JsonResult BuscarClientes(int? clienteId)
        {
            var cliente = BLL.ClientesBLL.Buscar(clienteId);
            return Json(cliente);
        }
        [HttpGet]
        public JsonResult ArticulosVentas(int id)
        {
            var listado = ArticulosBLL.GetListaId(id);

            return Json(listado);
        }
        [HttpGet]
        public JsonResult ListaArticuloVentas(int id)
        {
            var listado = ArticulosBLL.GetLista();

            return Json(listado);
        }
        [HttpGet]
        public JsonResult ListaClientesVentas(int id)
        {
            var listado = BLL.ClientesBLL.GetLista();

            return Json(listado);
        }
        [HttpGet]
        public JsonResult ListaEmpleadosVentas(int id)
        {
            var listado = EmpleadosBLL.GetLista();

            return Json(listado);
        }
        [HttpPost]
        public JsonResult GuardarVentas(ClasesDetalle nueva)
        {
            bool resultado = false;
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                int y, m, d, h, min, s;
                y = nueva.Encabezado.Fecha.Year;
                m = nueva.Encabezado.Fecha.Month;
                d = nueva.Encabezado.Fecha.Day;
                h = now.Hour;
                min = now.Minute;
                s = now.Second;
                nueva.Encabezado.Fecha = new DateTime(y, m, d, h, min, s);

                resultado = VentasBLL.Guardar(nueva);
            }
            return Json(resultado);
        }
        [HttpPost]
        public JsonResult GuardarDetalleVentas([FromBody]List<VentasDetalles> detalles)
        {
            bool resultado = VentasDetallesBLL.Guardar(detalles);

            return Json(resultado);
        }
    }
}