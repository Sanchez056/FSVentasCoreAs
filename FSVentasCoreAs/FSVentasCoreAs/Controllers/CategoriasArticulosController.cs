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
using FSVentasCoreAs.BLL;

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
        public IActionResult Index()
        {
            return View(CategoriasArticulosBLL.GetLista());
        }

        // GET: CategoriasArticulos
        public IActionResult Consulta()
        {
            return View(CategoriasArticulosBLL.GetLista());
        }

        // GET: CategoriasArticulos/Details/5
        public async Task<IActionResult> Detalle(int? id)
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
        public IActionResult Crear()
        {
            return View();
        }

        // POST: CategoriasArticulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("CategoriaId,Nombre")] CategoriasArticulos categoriasArticulos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriasArticulos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            return View(categoriasArticulos);
        }

        // GET: CategoriasArticulos/Edit/5
        public async Task<IActionResult> Editar(int? id)
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
        public async Task<IActionResult> Editar(int id, [Bind("CategoriaId,Nombre")] CategoriasArticulos categoriasArticulos)
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
                return RedirectToAction("Consulta");
            }
            return View(categoriasArticulos);
        }

        // GET: CategoriasArticulos/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
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
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriasArticulos = await _context.CategoriasArticulos.SingleOrDefaultAsync(m => m.CategoriaId == id);
            _context.CategoriasArticulos.Remove(categoriasArticulos);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool CategoriasArticulosExists(int id)
        {
            return _context.CategoriasArticulos.Any(e => e.CategoriaId == id);
        }
    }
}
