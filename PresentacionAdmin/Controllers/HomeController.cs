using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//referencias
using Entidad;
using Negocios;

namespace PresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();
            oLista = new N_Usuarios().Listar();
            return Json(new {data = oLista}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuarios(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idusuario == 0)
            {
                resultado = new N_Usuarios().Registrar(objeto, out mensaje);
            } else
            {
                resultado = new N_Usuarios().Editar(objeto, out mensaje) ;
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}