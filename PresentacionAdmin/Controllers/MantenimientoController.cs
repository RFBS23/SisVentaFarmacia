using Entidad;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentacionAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        public ActionResult Mantenimientos()
        {
            return View();
        }
        // GET: Mantenimiento
        public ActionResult Categorias()
        {
            return View();
        }
        
        public ActionResult Marcas()
        {
            return View();
        }
        
        public ActionResult Productos()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new N_Categorias().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategorias(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idcategoria == 0)
            {
                resultado = new N_Categorias().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Categorias().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategorias(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Categorias().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}