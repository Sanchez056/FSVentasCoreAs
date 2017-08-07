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
    public class TipoUsuariosController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public TipoUsuariosController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        public IActionResult Index()
        {
            return View(TipoUsuariosBLL.GetLista());
        }

        // GET: TipoUsuarios
        public  IActionResult Consulta()
        {
            return View(TipoUsuariosBLL.GetLista());
        }

        // GET: TipoUsuarios/Details/5
        public async Task<IActionResult> Detalle(int? id)
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
        public IActionResult Crear()
        {
            return View();
        }

        // POST: TipoUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("TipoId,Nombre")] TipoUsuarios tipoUsuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoUsuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            return View(tipoUsuarios);
        }

        // GET: TipoUsuarios/Edit/5
        public async Task<IActionResult> Editar(int? id)
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
        public async Task<IActionResult> Editar(int id, [Bind("TipoId,Nombre")] TipoUsuarios tipoUsuarios)
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
                return RedirectToAction("Consulta");
            }
            return View(tipoUsuarios);
        }

        // GET: TipoUsuarios/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
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
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoUsuarios = await _context.TipoUsuarios.SingleOrDefaultAsync(m => m.TipoId == id);
            _context.TipoUsuarios.Remove(tipoUsuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool TipoUsuariosExists(int id)
        {
            return _context.TipoUsuarios.Any(e => e.TipoId == id);
        }
    }
}
