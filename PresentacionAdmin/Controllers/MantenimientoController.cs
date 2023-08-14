using Entidad;
using Negocios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
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

        //categorias
        #region categoria
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

        #endregion

        // marcas
        #region marca
        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new N_Marcas().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarcas(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.idmarca == 0)
            {
                resultado = new N_Marcas().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new N_Marcas().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarcas(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Marcas().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //producto
        #region producto
        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new N_productos().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProductos(string objeto, HttpPostedFileBase archivoimg)
        {
            string mensaje = string.Empty;

            bool operacionexitosa = true;
            bool guardarimg = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;
            if (decimal.TryParse(oProducto.precioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oProducto.precio = precio;
            } else
            {
                return Json(new { operacionExitosa =false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            }

            if (oProducto.idproducto == 0)
            {
                int idprodgenerado = new N_productos().Registrar(oProducto, out mensaje);
                if (idprodgenerado != 0)
                {
                    oProducto.idproducto = idprodgenerado;
                } else
                {
                    operacionexitosa = false;
                }
            }
            else
            {
                operacionexitosa = new N_productos().Editar(oProducto, out mensaje);
            }

            if(operacionexitosa)
            {
                if(archivoimg != null)
                {
                    string rutaguardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoimg.FileName);
                    string nombreimg = string.Concat(oProducto.idproducto.ToString(), extension);

                    try
                    {
                        archivoimg.SaveAs(Path.Combine(rutaguardar, nombreimg));
                    } catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarimg = false;
                    }

                    if (guardarimg)
                    {
                        oProducto.rutaimg = rutaguardar;
                        oProducto.nombreimg = nombreimg;
                        bool rpta = new N_productos().GuardarImg(oProducto, out mensaje);
                    } else
                    {
                        mensaje = "Se guardo el producto pero se encontraron problemas en la imagen 🌄";
                    }
                }
            }

            return Json(new { operacionExitosa = operacionexitosa, idgenerado = oProducto.idproducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult imgProductos(int id)
        {
            bool conversion;
            Producto oProducto = new N_productos().Listar().Where(p => p.idproducto == id).FirstOrDefault();

            string textoBase64 = N_Recursos.ConvertirBase64(Path.Combine(oProducto.rutaimg, oProducto.nombreimg), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oProducto.nombreimg)
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarProductos(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_productos().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}