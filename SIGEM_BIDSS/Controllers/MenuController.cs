using SIGEM_BIDSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    public class MenuController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Catalogo()
        {
            TempData["getswalfunction"] = TempData["swalfunction"];
            return View();
        }
        public ActionResult Empleados()
        {
            return View();
        }
        public ActionResult Solicitud()
        {
            try { ViewBag.smserror = TempData["smserror"].ToString(); } catch { }
            return View();
        }

        public ActionResult Configuracion()
        {
            return View();
        }

        public ActionResult Planilla()
        {
            return View();
        }
        public ActionResult Inventario()
        {
            return View();
        }
    }
}