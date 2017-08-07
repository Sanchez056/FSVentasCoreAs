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
    public class MunicipiosController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public MunicipiosController(FSVentasCoreDb context)
        {
            _context = context;    
        }

        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.Municipios.Include(m => m.Provincias);
            return View(await fSVentasCoreDb.ToListAsync());
        }
        // GET: Municipios
        public async Task<IActionResult> Consulta()
        {
            var fSVentasCoreDb = _context.Municipios.Include(m => m.Provincias);
            return View(await fSVentasCoreDb.ToListAsync());
        }
        // GET: Municipios/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipios = await _context.Municipios
                .Include(m => m.Provincias)
                .SingleOrDefaultAsync(m => m.MunicipioId == id);
            if (municipios == null)
            {
                return NotFound();
            }

            return View(municipios);
        }

        // GET: Municipios/Create
        public IActionResult Crear()
        {
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre");
            return View();
        }

        // POST: Municipios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("MunicipioId,Nombre,ProvinciaId")] Municipios municipios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(municipios);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", municipios.ProvinciaId);
            return View(municipios);
        }

        // GET: Municipios/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipios = await _context.Municipios.SingleOrDefaultAsync(m => m.MunicipioId == id);
            if (municipios == null)
            {
                return NotFound();
            }
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", municipios.ProvinciaId);
            return View(municipios);
        }

        // POST: Municipios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("MunicipioId,Nombre,ProvinciaId")] Municipios municipios)
        {
            if (id != municipios.MunicipioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(municipios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunicipiosExists(municipios.MunicipioId))
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
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", municipios.ProvinciaId);
            return View(municipios);
        }

        // GET: Municipios/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipios = await _context.Municipios
                .Include(m => m.Provincias)
                .SingleOrDefaultAsync(m => m.MunicipioId == id);
            if (municipios == null)
            {
                return NotFound();
            }

            return View(municipios);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var municipios = await _context.Municipios.SingleOrDefaultAsync(m => m.MunicipioId == id);
            _context.Municipios.Remove(municipios);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool MunicipiosExists(int id)
        {
            return _context.Municipios.Any(e => e.MunicipioId == id);
        }
    }
}
