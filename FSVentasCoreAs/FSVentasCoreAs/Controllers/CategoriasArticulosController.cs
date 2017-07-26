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
    public class CategoriasArticulosController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public CategoriasArticulosController(FSVentasCoreDb context)
        {
            _context = context;    
        }

        // GET: CategoriasArticulos
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriasArticulos.ToListAsync());
        }

        // GET: CategoriasArticulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriasArticulos = await _context.CategoriasArticulos
                .SingleOrDefaultAsync(m => m.CategoriaId == id);
            if (categoriasArticulos == null)
            {
                return NotFound();
            }

            return View(categoriasArticulos);
        }

        // GET: CategoriasArticulos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriasArticulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaId,Nombre")] CategoriasArticulos categoriasArticulos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriasArticulos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categoriasArticulos);
        }

        // GET: CategoriasArticulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriasArticulos = await _context.CategoriasArticulos.SingleOrDefaultAsync(m => m.CategoriaId == id);
            if (categoriasArticulos == null)
            {
                return NotFound();
            }
            return View(categoriasArticulos);
        }

        // POST: CategoriasArticulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,Nombre")] CategoriasArticulos categoriasArticulos)
        {
            if (id != categoriasArticulos.CategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriasArticulos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasArticulosExists(categoriasArticulos.CategoriaId))
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
            return View(categoriasArticulos);
        }

        // GET: CategoriasArticulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriasArticulos = await _context.CategoriasArticulos
                .SingleOrDefaultAsync(m => m.CategoriaId == id);
            if (categoriasArticulos == null)
            {
                return NotFound();
            }

            return View(categoriasArticulos);
        }

        // POST: CategoriasArticulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriasArticulos = await _context.CategoriasArticulos.SingleOrDefaultAsync(m => m.CategoriaId == id);
            _context.CategoriasArticulos.Remove(categoriasArticulos);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CategoriasArticulosExists(int id)
        {
            return _context.CategoriasArticulos.Any(e => e.CategoriaId == id);
        }
    }
}
