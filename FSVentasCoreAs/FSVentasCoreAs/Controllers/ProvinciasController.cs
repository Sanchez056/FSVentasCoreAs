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
    public class ProvinciasController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public ProvinciasController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        public IActionResult Index()
        {
            return View(ProvinciasBLL.GetLista());
        }
            // GET: Provincias
            public IActionResult Consulta()
        {
            return View(ProvinciasBLL.GetLista());
        }

        // GET: Provincias/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincias = await _context.Provincias
                .SingleOrDefaultAsync(m => m.ProvinciaId == id);
            if (provincias == null)
            {
                return NotFound();
            }

            return View(provincias);
        }

        // GET: Provincias/Create
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Provincias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("ProvinciaId,Nombre")] Provincias provincias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincias);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            return View(provincias);
        }

        // GET: Provincias/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincias = await _context.Provincias.SingleOrDefaultAsync(m => m.ProvinciaId == id);
            if (provincias == null)
            {
                return NotFound();
            }
            return View(provincias);
        }

        // POST: Provincias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("ProvinciaId,Nombre")] Provincias provincias)
        {
            if (id != provincias.ProvinciaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provincias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciasExists(provincias.ProvinciaId))
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
            return View(provincias);
        }

        // GET: Provincias/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincias = await _context.Provincias
                .SingleOrDefaultAsync(m => m.ProvinciaId == id);
            if (provincias == null)
            {
                return NotFound();
            }

            return View(provincias);
        }

        // POST: Provincias/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provincias = await _context.Provincias.SingleOrDefaultAsync(m => m.ProvinciaId == id);
            _context.Provincias.Remove(provincias);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool ProvinciasExists(int id)
        {
            return _context.Provincias.Any(e => e.ProvinciaId == id);
        }
    }
}
