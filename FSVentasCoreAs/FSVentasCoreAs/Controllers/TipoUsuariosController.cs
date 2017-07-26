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
    public class TipoUsuariosController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public TipoUsuariosController(FSVentasCoreDb context)
        {
            _context = context;    
        }

        // GET: TipoUsuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoUsuarios.ToListAsync());
        }

        // GET: TipoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUsuarios = await _context.TipoUsuarios
                .SingleOrDefaultAsync(m => m.TipoId == id);
            if (tipoUsuarios == null)
            {
                return NotFound();
            }

            return View(tipoUsuarios);
        }

        // GET: TipoUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoId,Nombre")] TipoUsuarios tipoUsuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoUsuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoUsuarios);
        }

        // GET: TipoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUsuarios = await _context.TipoUsuarios.SingleOrDefaultAsync(m => m.TipoId == id);
            if (tipoUsuarios == null)
            {
                return NotFound();
            }
            return View(tipoUsuarios);
        }

        // POST: TipoUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoId,Nombre")] TipoUsuarios tipoUsuarios)
        {
            if (id != tipoUsuarios.TipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoUsuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUsuariosExists(tipoUsuarios.TipoId))
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
            return View(tipoUsuarios);
        }

        // GET: TipoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUsuarios = await _context.TipoUsuarios
                .SingleOrDefaultAsync(m => m.TipoId == id);
            if (tipoUsuarios == null)
            {
                return NotFound();
            }

            return View(tipoUsuarios);
        }

        // POST: TipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoUsuarios = await _context.TipoUsuarios.SingleOrDefaultAsync(m => m.TipoId == id);
            _context.TipoUsuarios.Remove(tipoUsuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TipoUsuariosExists(int id)
        {
            return _context.TipoUsuarios.Any(e => e.TipoId == id);
        }
    }
}
