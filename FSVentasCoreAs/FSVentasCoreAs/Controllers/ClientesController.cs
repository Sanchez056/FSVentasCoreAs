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
using FSVentasCoreAs.BLL;

namespace FSVentasCoreAs.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "CookiePolicy")]
    public class ClientesController : Controller
    {
        private readonly FSVentasCoreDb _context;
        private FSVentasCoreDb db = new FSVentasCoreDb();

        public ClientesController(FSVentasCoreDb context)
        {
            _context = context;    
        }
        public async Task<IActionResult> Index()
        {
            var fSVentasCoreDb = _context.Clientes.Include(c => c.DistritosMunicipales).Include(c => c.Municipios).Include(c => c.Provincias).Include(c => c.Sectores);
            return View(await fSVentasCoreDb.ToListAsync());
        }
        // GET: Clientes
        public async Task<IActionResult> Consulta()
        {
            var fSVentasCoreDb = _context.Clientes.Include(c => c.DistritosMunicipales).Include(c => c.Municipios).Include(c => c.Provincias).Include(c => c.Sectores);
            return View(await fSVentasCoreDb.ToListAsync());
        }
        // GET: Clientes/Details/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .Include(c => c.DistritosMunicipales)
                .Include(c => c.Municipios)
                .Include(c => c.Provincias)
                .Include(c => c.Sectores)
                .SingleOrDefaultAsync(m => m.ClienteId == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Crear()
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

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("ClienteId,Nombre,Sexo,Cedula,ProvinciaId,MunicipioId,DistritoId,SectorId,Direccion,Telefono,Celular,Fecha")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Consulta");
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "Nombre", clientes.DistritoId);
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "Nombre", clientes.MunicipioId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", clientes.ProvinciaId);
            ViewData["SectorId"] = new SelectList(_context.Sectores, "SectorId", "Nombre", clientes.SectorId);
            return View(clientes);
        }
        [HttpGet]
        public JsonResult Lista(int? id)
        {
            var listado = BLL.ClientesBLL.GetLista();

            return Json(listado);
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
                sectores.Insert(0, new Sectores { SectorId = 0, Nombre = "--Selecionar  Sectores " });
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

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes.SingleOrDefaultAsync(m => m.ClienteId == id);
            if (clientes == null)
            {
                return NotFound();
            }
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "Nombre", clientes.DistritoId);
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "Nombre", clientes.MunicipioId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", clientes.ProvinciaId);
            ViewData["SectorId"] = new SelectList(_context.Sectores, "SectorId", "Nombre", clientes.SectorId);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nombre,Sexo,Cedula,ProvinciaId,MunicipioId,DistritoId,SectorId,Direccion,Telefono,Celular,Fecha")] Clientes clientes)
        {
            if (id != clientes.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientes.ClienteId))
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
            ViewData["DistritoId"] = new SelectList(_context.DistritosMunicipales, "DistritoId", "Nombre", clientes.DistritoId);
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "Nombre", clientes.MunicipioId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincias, "ProvinciaId", "Nombre", clientes.ProvinciaId);
            ViewData["SectorId"] = new SelectList(_context.Sectores, "SectorId", "Nombre", clientes.SectorId);
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Clientes
                .Include(c => c.DistritosMunicipales)
                .Include(c => c.Municipios)
                .Include(c => c.Provincias)
                .Include(c => c.Sectores)
                .SingleOrDefaultAsync(m => m.ClienteId == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.Clientes.SingleOrDefaultAsync(m => m.ClienteId == id);
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Consulta");
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
