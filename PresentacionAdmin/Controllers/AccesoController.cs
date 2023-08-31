using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocios;

namespace PresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult CambiarClave()
        {
            return View();
        }
        
        public ActionResult RestablecerClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new N_Usuarios().Listar().Where(u => u.correo == correo && u.clave == N_Recursos.ConvertitSha256(clave)).FirstOrDefault();
            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña no son validos";
                return View();
            } else
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}