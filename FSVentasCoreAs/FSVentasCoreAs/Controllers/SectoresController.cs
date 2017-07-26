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

        // GET: Sectores
        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.Sectores.Include(s => s.DistritosMunicipales);
            return View(await fSVentasCoreDb.ToListAsync());
        }

        // GET: Sectores/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public IActionResult Create()
        {
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "DistritoId");
            return View();
        }

        // POST: Sectores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectorId,Nombre,DistritoId")] Sectores sectores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectores);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "DistritoId", sectores.DistritoId);
            return View(sectores);
        }

        // GET: Sectores/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "DistritoId", sectores.DistritoId);
            return View(sectores);
        }

        // POST: Sectores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectorId,Nombre,DistritoId")] Sectores sectores)
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
                return RedirectToAction("Index");
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "DistritoId", sectores.DistritoId);
            return View(sectores);
        }

        // GET: Sectores/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sectores = await _context.Sectores.SingleOrDefaultAsync(m => m.SectorId == id);
            _context.Sectores.Remove(sectores);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SectoresExists(int id)
        {
            return _context.Sectores.Any(e => e.SectorId == id);
        }
    }
}
