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
    public class SectoresController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public SectoresController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.Sectores.Include(s => s.DistritosMunicipales);
            return View(await fSVentasCoreDb.ToListAsync());
        }


        // GET: Sectores
        public async Task<IActionResult> Consulta()
        {
            var fSVentasCoreDb = _context.Sectores.Include(s => s.DistritosMunicipales);
            return View(await fSVentasCoreDb.ToListAsync());
        }

        // GET: Sectores/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectores = await _context.Sectores
                .Include(s => s.DistritosMunicipales)
                .SingleOrDefaultAsync(m => m.SectorId == id);
            if (sectores == null)
            {
                return NotFound();
            }

            return View(sectores);
        }

        // GET: Sectores/Create
        public IActionResult Crear()
        {
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "Nombre");
            return View();
        }

        // POST: Sectores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("SectorId,Nombre,DistritoId")] Sectores sectores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectores);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "Nombre", sectores.DistritoId);
            return View(sectores);
        }

        // GET: Sectores/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectores = await _context.Sectores.SingleOrDefaultAsync(m => m.SectorId == id);
            if (sectores == null)
            {
                return NotFound();
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "Nombre", sectores.DistritoId);
            return View(sectores);
        }

        // POST: Sectores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("SectorId,Nombre,DistritoId")] Sectores sectores)
        {
            if (id != sectores.SectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectoresExists(sectores.SectorId))
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
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "Nombre", sectores.DistritoId);
            return View(sectores);
        }

        // GET: Sectores/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sectores = await _context.Sectores
                .Include(s => s.DistritosMunicipales)
                .SingleOrDefaultAsync(m => m.SectorId == id);
            if (sectores == null)
            {
                return NotFound();
            }

            return View(sectores);
        }

        // POST: Sectores/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectores = await _context.Sectores.SingleOrDefaultAsync(m => m.SectorId == id);
            _context.Sectores.Remove(sectores);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool SectoresExists(int id)
        {
            return _context.Sectores.Any(e => e.SectorId == id);
        }
    }
}
