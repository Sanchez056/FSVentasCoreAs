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
using FSVentasCoreAs.Models.Dirreciones;

namespace FSVentasCoreAs.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "CookiePolicy")]
    public class ProveedoresController : Controller
    {
        private readonly FSVentasCoreDb _context;
        private FSVentasCoreDb db = new FSVentasCoreDb();

        public ProveedoresController(FSVentasCoreDb context)
        {
            _context = context;    
        }


        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.Proveedores.Include(p => p.DistritosMunicipales).Include(p => p.MarcasArticulos).Include(p => p.Municipios).Include(p => p.Provincias).Include(p => p.Sectores);
            return View(await fSVentasCoreDb.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores
                .Include(p => p.DistritosMunicipales)
                .Include(p => p.MarcasArticulos)
                .Include(p => p.Municipios)
                .Include(p => p.Provincias)
                .Include(p => p.Sectores)
                .SingleOrDefaultAsync(m => m.ProveedorId == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return View(proveedores);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            List<Provincias> lstProvincia = db.Provincias.ToList();
            lstProvincia.Insert(0, new Provincias { ProvinciaId = 0, Nombre = "--Select Provincia--" });
            ViewBag.ProvinciaId = new SelectList(lstProvincia, "ProvinciaId", "Nombre");
            List<Municipios> lstMunicipios = db.Municipios.ToList();
            ViewBag.MunicipioId = new SelectList(lstMunicipios, "MunicipioId", "Nombre");
            List<DistritosMunicipales> lstDistrito = db.DistritosMunicipales.ToList();
            ViewBag.DistritoId = new SelectList(lstDistrito, "DistritoId", "Nombre");
            List<Sectores> lstSectores = db.Sectores.ToList();
            ViewBag.SectorId = new SelectList(lstSectores, "SectorId", "Nombre");
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProveedorId,Nombre,MarcaId,ProvinciaId,MunicipioId,DistritoId,SectorId,Direccion,Telefono,Fax,Correo,Fecha")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedores);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "DistritoId", proveedores.DistritoId);
            ViewData["MarcaId"] = new SelectList(_context.MarcasArticulos, "MarcaId", "MarcaId", proveedores.MarcaId);
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "MunicipioId", proveedores.MunicipioId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "ProvinciaId", proveedores.ProvinciaId);
            ViewData["SectorId"] = new SelectList(_context.Sectores, "SectorId", "SectorId", proveedores.SectorId);
            return View(proveedores);
        }
        public JsonResult GetMunicipiosByProvinciaId(int id)
        {
            List<Municipios> municipios = new List<Municipios>();
            if (id > 0)
            {
                municipios = db.Municipios.Where(p => p.ProvinciaId == id).ToList();


            }
            else
            {
                municipios.Insert(0, new Municipios { MunicipioId = 0, Nombre = "--Selecionar Municipios --" });
            }
            var result = (from r in municipios
                          select new
                          {
                              municipioId = r.MunicipioId,
                              nombre = r.Nombre
                          }).ToList();

            return Json(result);

            // return Json(result);

        }

        public JsonResult GetdistritoIdByMunicipioId(int id)
        {
            List<DistritosMunicipales> distrito = new List<DistritosMunicipales>();
            if (id > 0)
            {
                distrito = db.DistritosMunicipales.Where(p => p.MunicipioId == id).ToList();

            }
            else
            {
                distrito.Insert(0, new DistritosMunicipales { DistritoId = 0, Nombre = "--Seleccionar Distritos Municipales--" });
            }
            var result = (from r in distrito
                          select new
                          {
                              distritoId = r.DistritoId,
                              nombre = r.Nombre
                          }).ToList();

            return Json(result);
            // return Json(result);

        }
        public JsonResult GetSectoresIdByDistritoId(int id)
        {
            List<Sectores> sectores = new List<Sectores>();
            if (id > 0)
            {
                sectores = db.Sectores.Where(p => p.DistritoId == id).ToList();

            }
            else
            {
                sectores.Insert(0, new Sectores { SectorId = 0, Nombre = "--Selecionar  sectores " });
            }
            var result = (from r in sectores
                          select new
                          {
                              sectorId = r.SectorId,
                              nombre = r.Nombre
                          }).ToList();

            return Json(result);
            // return Json(result);

        }


        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores.SingleOrDefaultAsync(m => m.ProveedorId == id);
            if (proveedores == null)
            {
                return NotFound();
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "DistritoId", proveedores.DistritoId);
            ViewData["MarcaId"] = new SelectList(_context.MarcasArticulos, "MarcaId", "MarcaId", proveedores.MarcaId);
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "MunicipioId", proveedores.MunicipioId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "ProvinciaId", proveedores.ProvinciaId);
            ViewData["SectorId"] = new SelectList(_context.Sectores, "SectorId", "SectorId", proveedores.SectorId);
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProveedorId,Nombre,MarcaId,ProvinciaId,MunicipioId,DistritoId,SectorId,Direccion,Telefono,Fax,Correo,Fecha")] Proveedores proveedores)
        {
            if (id != proveedores.ProveedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoresExists(proveedores.ProveedorId))
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
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "DistritoId", proveedores.DistritoId);
            ViewData["MarcaId"] = new SelectList(_context.MarcasArticulos, "MarcaId", "MarcaId", proveedores.MarcaId);
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "MunicipioId", proveedores.MunicipioId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "ProvinciaId", proveedores.ProvinciaId);
            ViewData["SectorId"] = new SelectList(_context.Sectores, "SectorId", "SectorId", proveedores.SectorId);
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await _context.Proveedores
                .Include(p => p.DistritosMunicipales)
                .Include(p => p.MarcasArticulos)
                .Include(p => p.Municipios)
                .Include(p => p.Provincias)
                .Include(p => p.Sectores)
                .SingleOrDefaultAsync(m => m.ProveedorId == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedores = await _context.Proveedores.SingleOrDefaultAsync(m => m.ProveedorId == id);
            _context.Proveedores.Remove(proveedores);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProveedoresExists(int id)
        {
            return _context.Proveedores.Any(e => e.ProveedorId == id);
        }
    }
}
