using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Rotativa;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using FSVentasCoreAs.BLL;

namespace FSVentasCoreAs.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "CookiePolicy")]
    public class UsuariosController : Controller
    {
        private readonly FSVentasCoreDb _context;
        private FSVentasCoreDb db = new FSVentasCoreDb();
        public UsuariosController(FSVentasCoreDb context)
        {
            _context = context;
        }
        public JsonResult UsuarioDisponible(string Nombre)
        {
            return Json(Nombre);
        }
        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.Usuarios.Include(u => u.TipoUsuarios);
            return View(await fSVentasCoreDb.ToListAsync());
        }


        // GET: Usuarios
        public async Task<IActionResult> Consulta()
        {
            var fSVentasCoreDb = _context.Usuarios.Include(u => u.TipoUsuarios);
            return View(await fSVentasCoreDb.ToListAsync());
        }
        public async Task<IActionResult> ReportUsuarios()
        {
            var fSVentasCoreDb = _context.Usuarios.Include(u => u.TipoUsuarios);
            return View(await fSVentasCoreDb.ToListAsync());
        }



        // GET: Usuarios/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.TipoUsuarios)
                .SingleOrDefaultAsync(m => m.UsuarioId == id);
            if (usuarios == null)
            {
                return NotFound();
            }

           return View(usuarios);
        }


        // GET: Usuarios/Create
        public IActionResult Crear()
        {
            ViewData["TipoId"] = new SelectList(_context.TipoUsuarios, "TipoId", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("UsuarioId,Nombres,Contraseña,TipoId")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            ViewData["TipoId"] = new SelectList(_context.TipoUsuarios, "TipoId", "Nombre", usuarios.TipoId);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.SingleOrDefaultAsync(m => m.UsuarioId == id);
            if (usuarios == null)
            {
                return NotFound();
            }
            ViewData["TipoId"] = new SelectList(_context.TipoUsuarios, "TipoId", "Nombre", usuarios.TipoId);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("UsuarioId,Nombres,Contraseña,TipoId")] Usuarios usuarios)
        {
            if (id != usuarios.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.UsuarioId))
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
            ViewData["TipoId"] = new SelectList(_context.TipoUsuarios, "TipoId", "Nombre", usuarios.TipoId);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.TipoUsuarios)
                .SingleOrDefaultAsync(m => m.UsuarioId == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.Usuarios.SingleOrDefaultAsync(m => m.UsuarioId == id);
            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
