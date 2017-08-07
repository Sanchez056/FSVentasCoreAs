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
    public class ArticulosController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public ArticulosController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.Articulos.Include(a => a.CategoriasArticulos).Include(a => a.MarcasArticulos).Include(a => a.Proveedores);
            return View(await fSVentasCoreDb.ToListAsync());
        }


        // GET: Articulos
        public async Task<IActionResult> Consulta()
        {
            var fSVentasCoreDb = _context.Articulos.Include(a => a.CategoriasArticulos).Include(a => a.MarcasArticulos).Include(a => a.Proveedores);
            return View(await fSVentasCoreDb.ToListAsync());
        }

        // GET: Articulos/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos
                .Include(a => a.CategoriasArticulos)
                .Include(a => a.MarcasArticulos)
                .Include(a => a.Proveedores)
                .SingleOrDefaultAsync(m => m.ArticuloId == id);
            if (articulos == null)
            {
                return NotFound();
            }

            return View(articulos);
        }

        // GET: Articulos/Create
        public IActionResult Crear()
        {
            ViewData["CategoriaId"] = new SelectList(_context.CategoriasArticulos, "CategoriaId", "Nombre");
            ViewData["MarcaId"] = new SelectList(_context.MarcasArticulos, "MarcaId", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "Nombre");
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("ArticuloId,Nombre,Descripcion,MarcaId,ProveedorId,CategoriaId,Cantidad,Descuento,PrecioCompra,Precio,Importe,Fecha")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            ViewData["CategoriaId"] = new SelectList(_context.CategoriasArticulos, "CategoriaId", "Nombre", articulos.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.MarcasArticulos, "MarcaId", "Nombre", articulos.MarcaId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "Nombre", articulos.ProveedorId);
            return View(articulos);
        }

        // GET: Articulos/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos.SingleOrDefaultAsync(m => m.ArticuloId == id);
            if (articulos == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.CategoriasArticulos, "CategoriaId", "Nombre", articulos.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.MarcasArticulos, "MarcaId", "Nombre", articulos.MarcaId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "Nombre", articulos.ProveedorId);
            return View(articulos);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("ArticuloId,Nombre,Descripcion,MarcaId,ProveedorId,CategoriaId,Cantidad,Descuento,PrecioCompra,Precio,Importe,Fecha")] Articulos articulos)
        {
            if (id != articulos.ArticuloId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BLL.ArticulosBLL.Insertar(articulos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticulosExists(articulos.ArticuloId))
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
            ViewData["CategoriaId"] = new SelectList(_context.CategoriasArticulos, "CategoriaId", "Nombre", articulos.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.MarcasArticulos, "MarcaId", "Nombre", articulos.MarcaId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "Nombre", articulos.ProveedorId);
            return View(articulos);
        }

        // GET: Articulos/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulos = await _context.Articulos
                .Include(a => a.CategoriasArticulos)
                .Include(a => a.MarcasArticulos)
                .Include(a => a.Proveedores)
                .SingleOrDefaultAsync(m => m.ArticuloId == id);
            if (articulos == null)
            {
                return NotFound();
            }

            return View(articulos);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articulos = await _context.Articulos.SingleOrDefaultAsync(m => m.ArticuloId == id);
            _context.Articulos.Remove(articulos);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool ArticulosExists(int id)
        {
            return _context.Articulos.Any(e => e.ArticuloId == id);
        }
    }
}
