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
    public class VentasDetallesController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public VentasDetallesController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        public IActionResult Index()
        {
            return View(VentasDetallesBLL.GetLista());
        }

        // GET: VentasDetalles
        public IActionResult Consulta()
        {
            return View(VentasDetallesBLL.GetLista());
        }
        [HttpGet]
        public JsonResult BuscarF(int ventaId)
        {
            var venta = BLL.VentasDetallesBLL.Listar(ventaId);
            return Json(venta);
        }

        // GET: VentasDetalles/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasDetalles = await _context.VentasDetalles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ventasDetalles == null)
            {
                return NotFound();
            }

            return View(ventasDetalles);
        }

        // GET: VentasDetalles/Create
        public IActionResult Crear()
        {
            return View();
        }

        // POST: VentasDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Id,VentaId,ArticuloId,Precio,Cantidad")] VentasDetalles ventasDetalles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventasDetalles);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            return View(ventasDetalles);
        }

        // GET: VentasDetalles/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasDetalles = await _context.VentasDetalles.SingleOrDefaultAsync(m => m.Id == id);
            if (ventasDetalles == null)
            {
                return NotFound();
            }
            return View(ventasDetalles);
        }

        // POST: VentasDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,VentaId,ArticuloId,Precio,Cantidad")] VentasDetalles ventasDetalles)
        {
            if (id != ventasDetalles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventasDetalles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasDetallesExists(ventasDetalles.Id))
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
            return View(ventasDetalles);
        }

        // GET: VentasDetalles/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasDetalles = await _context.VentasDetalles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ventasDetalles == null)
            {
                return NotFound();
            }

            return View(ventasDetalles);
        }

        // POST: VentasDetalles/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventasDetalles = await _context.VentasDetalles.SingleOrDefaultAsync(m => m.Id == id);
            _context.VentasDetalles.Remove(ventasDetalles);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool VentasDetallesExists(int id)
        {
            return _context.VentasDetalles.Any(e => e.Id == id);
        }
    }
}
