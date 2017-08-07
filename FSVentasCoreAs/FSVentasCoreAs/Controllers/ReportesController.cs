using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSVentasCoreAs.DAL;
using Microsoft.EntityFrameworkCore;

namespace FSVentasCoreAs.Controllers
{
    public class ReportesController : Controller
    {
        private readonly FSVentasCoreDb _context;

        public async Task<IActionResult> IndexUsuarios()
        {
            var fSVentasCoreDb = _context.Usuarios.Include(u => u.TipoUsuarios);
            return View(await fSVentasCoreDb.ToListAsync());
        }
    }
}