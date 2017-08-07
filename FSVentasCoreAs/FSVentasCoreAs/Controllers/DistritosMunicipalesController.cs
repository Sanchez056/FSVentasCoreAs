using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models.Dirreciones;
using Microsoft.AspNetCore.Authorization;
using FSVentasCoreAs.BLL;

namespace FSVentasCoreAs.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "CookiePolicy")]
    public class DistritosMunicipalesController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public DistritosMunicipalesController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.DistritosMunicipales.Include(d => d.Municipios);
            return View(await fSVentasCoreDb.ToListAsync());
        }
        // GET: DistritosMunicipales
        public async Task<IActionResult> Consulta()
        {
            var fSVentasCoreDb = _context.DistritosMunicipales.Include(d => d.Municipios);
            return View(await fSVentasCoreDb.ToListAsync());
        }

        // GET: DistritosMunicipales/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distritosMunicipales = await _context.DistritosMunicipales
                .Include(d => d.Municipios)
                .SingleOrDefaultAsync(m => m.DistritoId == id);
            if (distritosMunicipales == null)
            {
                return NotFound();
            }

            return View(distritosMunicipales);
        }

        // GET: DistritosMunicipales/Create
        public IActionResult Crear()
        {
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "Nombre");
            return View();
        }

        // POST: DistritosMunicipales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("DistritoId,Nombre,MunicipioId")] DistritosMunicipales distritosMunicipales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distritosMunicipales);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "Nombre", distritosMunicipales.MunicipioId);
            return View(distritosMunicipales);
        }

        // GET: DistritosMunicipales/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distritosMunicipales = await _context.DistritosMunicipales.SingleOrDefaultAsync(m => m.DistritoId == id);
            if (distritosMunicipales == null)
            {
                return NotFound();
            }
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "Nombre", distritosMunicipales.MunicipioId);
            return View(distritosMunicipales);
        }

        // POST: DistritosMunicipales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("DistritoId,Nombre,MunicipioId")] DistritosMunicipales distritosMunicipales)
        {
            if (id != distritosMunicipales.DistritoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distritosMunicipales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistritosMunicipalesExists(distritosMunicipales.DistritoId))
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
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "Nombre", distritosMunicipales.MunicipioId);
            return View(distritosMunicipales);
        }

        // GET: DistritosMunicipales/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distritosMunicipales = await _context.DistritosMunicipales
                .Include(d => d.Municipios)
                .SingleOrDefaultAsync(m => m.DistritoId == id);
            if (distritosMunicipales == null)
            {
                return NotFound();
            }

            return View(distritosMunicipales);
        }

        // POST: DistritosMunicipales/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distritosMunicipales = await _context.DistritosMunicipales.SingleOrDefaultAsync(m => m.DistritoId == id);
            _context.DistritosMunicipales.Remove(distritosMunicipales);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool DistritosMunicipalesExists(int id)
        {
            return _context.DistritosMunicipales.Any(e => e.DistritoId == id);
        }
    }
}
