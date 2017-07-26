using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.AspNetCore.Authorization;

namespace FSVentasCoreAs.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "CookiePolicy")]
    public class MarcasArticulosController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public MarcasArticulosController(FSVentasCoreDb context)
        {
            _context = context;    
        }

        // GET: MarcasArticulos
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarcasArticulos.ToListAsync());
        }

        // GET: MarcasArticulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcasArticulos = await _context.MarcasArticulos
                .SingleOrDefaultAsync(m => m.MarcaId == id);
            if (marcasArticulos == null)
            {
                return NotFound();
            }

            return View(marcasArticulos);
        }

        // GET: MarcasArticulos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarcasArticulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarcaId,Nombre")] MarcasArticulos marcasArticulos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marcasArticulos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(marcasArticulos);
        }

        // GET: MarcasArticulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcasArticulos = await _context.MarcasArticulos.SingleOrDefaultAsync(m => m.MarcaId == id);
            if (marcasArticulos == null)
            {
                return NotFound();
            }
            return View(marcasArticulos);
        }

        // POST: MarcasArticulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarcaId,Nombre")] MarcasArticulos marcasArticulos)
        {
            if (id != marcasArticulos.MarcaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marcasArticulos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcasArticulosExists(marcasArticulos.MarcaId))
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
            return View(marcasArticulos);
        }

        // GET: MarcasArticulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcasArticulos = await _context.MarcasArticulos
                .SingleOrDefaultAsync(m => m.MarcaId == id);
            if (marcasArticulos == null)
            {
                return NotFound();
            }

            return View(marcasArticulos);
        }

        // POST: MarcasArticulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marcasArticulos = await _context.MarcasArticulos.SingleOrDefaultAsync(m => m.MarcaId == id);
            _context.MarcasArticulos.Remove(marcasArticulos);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MarcasArticulosExists(int id)
        {
            return _context.MarcasArticulos.Any(e => e.MarcaId == id);
        }
    }
}
