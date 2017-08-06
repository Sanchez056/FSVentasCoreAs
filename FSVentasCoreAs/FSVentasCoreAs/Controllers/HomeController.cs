using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FSVentasCoreAs.DAL;
using FSVentasCoreAs.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using Rotativa;
using Syncfusion.JavaScript;

namespace FSVentasCoreAs.Controllers
{
    public class HomeController : Controller
    {
        private FSVentasCoreDb db = new FSVentasCoreDb();
        public IActionResult Index()
        {
          return View();
        }
      

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        public ActionResult Login()
        {
            ViewData["TipoId"] = new SelectList(db.TipoUsuarios, "TipoId", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios user)
        {
            var account = db.Usuarios.Where(u => u.Nombres == user.Nombres && u.Contraseña == user.Contraseña && u.TipoId == user.TipoId).FirstOrDefault();
            ViewData["TipoId"] = new SelectList(db.TipoUsuarios, "TipoId", "Nombre");
            if (account != null)
            {
                HttpContext.Session.SetString("UsuarioId", account.UsuarioId.ToString());
                HttpContext.Session.SetString("Nombres", account.Nombres);
                HttpContext.Session.SetString("TipoId", account.TipoId.ToString());


                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.Nombres),
                    new Claim(ClaimTypes.Name, account.TipoId.ToString())

                };

                var Identity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(Identity);

                HttpContext.Authentication.SignInAsync("CookiePolicy", principal);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "El usuario o contraseña incorrecto!!!.");
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync("CookiePolicy");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
