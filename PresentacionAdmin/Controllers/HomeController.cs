using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ClosedXML.Excel;

//referencias
using Entidad;
using Negocios;

namespace PresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        
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

        [HttpPost]
        public JsonResult EliminarUsuarios(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new N_Usuarios().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult vistaReportes()
        {
            Reportes objeto = new N_Reportes().verReportes();
            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listaReportes(string fechainicio, string fechafin, string idtransaccion)
        {
            List<ReportesVenta> oLista = new List<ReportesVenta>();
            oLista = new N_Reportes().Ventas(fechainicio, fechafin, idtransaccion);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportarVentaExcel(string fechainicio, string fechafin, string idtransaccion)
        {
            List<ReportesVenta> oLista = new List<ReportesVenta>();
            oLista = new N_Reportes().Ventas(fechainicio, fechafin, idtransaccion);

            DataTable dt = new DataTable();
            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("FechaVenta", typeof(string));
            dt.Columns.Add("Clientes", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("precio", typeof(decimal));
            dt.Columns.Add("cantidad", typeof(int));
            dt.Columns.Add("total", typeof(decimal));
            dt.Columns.Add("idtransaccion", typeof(string));

            foreach (ReportesVenta rv in oLista)
            {
                dt.Rows.Add(new object[]
                {
                    rv.FechaVenta,
                    rv.Clientes,
                    rv.Producto,
                    rv.precio,
                    rv.cantidad,
                    rv.total,
                    rv.idtransaccion
                });
            }

            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

    }
}