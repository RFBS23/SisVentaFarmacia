﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentacionAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
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
    }
}