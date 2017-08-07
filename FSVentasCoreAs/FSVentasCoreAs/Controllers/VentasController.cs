using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using FSVentasCoreAs.BLL;

namespace FSVentasCoreAs.Controllers
{
    public class VentasController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public VentasController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        [HttpPost]
        public JsonResult Modificar(ClasesDetalle venta)
        {
            var existe = (BLL.VentasBLL.Buscarr(venta.Encabezado.VentaId) != null);
            if (existe)
            {
                existe = BLL.VentasBLL.Modificar(venta);
                return Json(existe);
            }
            else
            {
                return Json(null);
            }
        }
        [HttpGet]
        public JsonResult LastIndex()
        {
            int id = VentasBLL.Identity();
            if (id > 1 || VentasBLL.Listar().Count > 0)
                ++id;
            return Json(id);
        }


        [HttpPost]
        public JsonResult Eliminar(ClasesDetalle venta)
        {
            var existe = (BLL.VentasBLL.BuscarEncabezado(venta.Encabezado.VentaId) != null);

            if (existe)
            {
                existe = BLL.VentasBLL.Eliminar(venta);
                return Json(existe);
            }
            else
            {
                return Json(null);
            }
        }
        [HttpGet]
        public ActionResult Buscar(int Id)
        {
            Ventas var = BLL.VentasBLL.Buscar(Id);

            return Json(var);
        }
        [HttpGet]
        public JsonResult  BuscarV(int ventaId)
        {
            Ventas venta = VentasBLL.Buscar(ventaId);
            return Json(venta);
        }
        public IActionResult Index()
        {
            return View(BLL.VentasBLL.Listar());
        }
        // GET: Facturas
        public IActionResult Consulta()
        {
            return View(BLL.VentasBLL.Listar());
        }

       

        // GET: Ventas/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .SingleOrDefaultAsync(m => m.VentaId == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // GET: Ventas/Create
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("VentaId,ArticuloId,articulo,Cantidad,Fecha,Total")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventas);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            return View(ventas);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas.SingleOrDefaultAsync(m => m.VentaId == id);
            if (ventas == null)
            {
                return NotFound();
            }
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("VentaId,ArticuloId,articulo,Cantidad,Fecha,Total")] Ventas ventas)
        {
            if (id != ventas.VentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasExists(ventas.VentaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Consulta");
            }
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .SingleOrDefaultAsync(m => m.VentaId == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventas = await _context.Ventas.SingleOrDefaultAsync(m => m.VentaId == id);
            _context.Ventas.Remove(ventas);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool VentasExists(int id)
        {
            return _context.Ventas.Any(e => e.VentaId == id);
        }
    }
}
