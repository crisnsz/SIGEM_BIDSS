using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class SolicitudReembolsoGastosDetallesController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: SolicitudReembolsoGastosDetalles
        public ActionResult Index()
        {
            var tbSolicitudReembolsoGastosDetalle = db.tbSolicitudReembolsoGastosDetalle.Include(t => t.tbSolicitudReembolsoGastos).Include(t => t.tbTipoViatico);
            return View(tbSolicitudReembolsoGastosDetalle.ToList());
        }


        [HttpPost]
        public JsonResult RemoveReembolso(tbSolicitudReembolsoGastosDetalle ReembolsoD)
        {
            var list = (List<tbSolicitudReembolsoGastosDetalle>)Session["ReemGa"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.tpv_Id == ReembolsoD.tpv_Id);
                list.Remove(itemToRemove);
                Session["ReemGa"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }
        // GET: SolicitudReembolsoGastosDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = db.tbSolicitudReembolsoGastosDetalle.Find(id);
            if (tbSolicitudReembolsoGastosDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        //GetDetalle
        [HttpPost]
        public JsonResult SaveReembolsoDetalle(tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle, HttpPostedFileBase ArchivoPath)
        {
            int datos = 0;

            //data_producto = SalidaDetalle.prod_Codigo;
            decimal ReemgaDet_MontoGasto = tbSolicitudReembolsoGastosDetalle.ReemgaDet_MontoGasto;
            List<tbSolicitudReembolsoGastosDetalle> sessionSolicitudReembolsoDetalle = new List<tbSolicitudReembolsoGastosDetalle>();
            var list = (List<tbSolicitudReembolsoGastosDetalle>)Session["ReembolsoDetalle"];

            if (list == null)
            {
                sessionSolicitudReembolsoDetalle.Add(tbSolicitudReembolsoGastosDetalle);
                Session["ReembolsoDetalle"] = sessionSolicitudReembolsoDetalle;
            }
            else
            {

                list.Add(tbSolicitudReembolsoGastosDetalle);
                Session["ReembolsoDetalle"] = list;
                return Json(tbSolicitudReembolsoGastosDetalle, JsonRequestBehavior.AllowGet);
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }



        // GET: SolicitudReembolsoGastosDetalles/Create
        public ActionResult Create()
        {
            int Id = Convert.ToInt32(Session["Reemga_Id"]);

            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = new tbSolicitudReembolsoGastosDetalle();
            tbSolicitudReembolsoGastosDetalle.Reemga_Id = Id;
            tbSolicitudReembolsoGastosDetalle.ReemgaDet_FechaGasto = Function.DatetimeNow();
            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Id");
            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            ViewBag.Archivo = db.tbSolicitudReembolsoGastosDetalle.Where(x => x.Reemga_Id == tbSolicitudReembolsoGastosDetalle.Reemga_Id).ToList();

            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // POST: SolicitudReembolsoGastosDetalles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReemgaDet_Id,Reemga_Id,ReemgaDet_FechaGasto,tpv_Id" +
            ",ReemgaDet_MontoGasto,ReemgaDet_Concepto,ReemgaDet_Archivo")] tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle, HttpPostedFileBase ArchivoPath)
        {
            string UserName = "", MsjError = "", ErrorEmail = "";
            var ReembolsoGastos = (List<tbSolicitudReembolsoGastosDetalle>)Session["NombreReembolsoGastos"];
            var path = "";
            IEnumerable<object> lista = null;
            try
            {
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitudReembolsoGastosDetalle.tpv_Id);
                ViewBag.Archivo = db.tbLiquidacionAnticipoViaticoDetalle.Where(x => x.Lianvi_Id == tbSolicitudReembolsoGastosDetalle.Reemga_Id).ToList();

                if (ArchivoPath != null)
                {
                    if (ArchivoPath.ContentLength > 0)
                    {
                        if (Path.GetExtension(ArchivoPath.FileName).ToLower() == ".jpg" || Path.GetExtension(ArchivoPath.FileName).ToLower() == ".png")
                        {
                            int ArchivoCorrelativo = db.tbLiquidacionAnticipoViaticoDetalle.Where(x => x.Lianvi_Id == tbSolicitudReembolsoGastosDetalle.Reemga_Id).Count();
                            string Extension = Path.GetExtension(ArchivoPath.FileName).ToLower();
                            string _NamePicture = tbSolicitudReembolsoGastosDetalle.ReemgaDet_Id + "." + ArchivoCorrelativo;
                            string Archivo = _NamePicture + Extension;
                            path = Path.Combine(Server.MapPath("~/Content/Profile_Pics"), Archivo);
                            ArchivoPath.SaveAs(path);
                            tbSolicitudReembolsoGastosDetalle.ReemgaDet_Archivo = "~/Content/Profile_Pics/" + Archivo;
                        }
                        else
                        {
                            string Error = "Formato de archivo incorrecto, favor adjuntar una fotografía con extensión .jpg";
                            ModelState.AddModelError("FotoPath", Error);
                            return View("Create");
                        }
                    }
                }
                int EmployeeID = Function.GetUser(out UserName);
                lista = db.UDP_Adm_tbSolicitudReembolsoGastosDetalle_Insert(tbSolicitudReembolsoGastosDetalle.Reemga_Id, tbSolicitudReembolsoGastosDetalle.ReemgaDet_FechaGasto, tbSolicitudReembolsoGastosDetalle.tpv_Id, tbSolicitudReembolsoGastosDetalle.ReemgaDet_MontoGasto, tbSolicitudReembolsoGastosDetalle.ReemgaDet_Concepto, tbSolicitudReembolsoGastosDetalle.ReemgaDet_Archivo);
                foreach (UDP_Adm_tbSolicitudReembolsoGastosDetalle_Insert_Result SolicitudDetalle in lista)
                    MsjError = SolicitudDetalle.MensajeError;
                if (MsjError.StartsWith("-1"))
                {
                    ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                    return View(tbSolicitudReembolsoGastosDetalle);
                }
                return RedirectToAction("Create");
            }
            catch (Exception)
            {
                TempData["swalfunction"] = GeneralFunctions._isCreated;
                ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo");
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbSolicitudReembolsoGastosDetalle);
            }
        }

        // GET: SolicitudReembolsoGastosDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = db.tbSolicitudReembolsoGastosDetalle.Find(id);
            if (tbSolicitudReembolsoGastosDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo", tbSolicitudReembolsoGastosDetalle.Reemga_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitudReembolsoGastosDetalle.tpv_Id);
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // POST: SolicitudReembolsoGastosDetalles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReemgaDet_Id,Reemga_Id,ReemgaDet_FechaGasto,tpv_Id,ReemgaDet_MontoGasto,ReemgaDet_Concepto,ReemgaDet_Archivo,ReemgaDet_TotalGastos")] tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudReembolsoGastosDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo", tbSolicitudReembolsoGastosDetalle.Reemga_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitudReembolsoGastosDetalle.tpv_Id);
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // GET: SolicitudReembolsoGastosDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = db.tbSolicitudReembolsoGastosDetalle.Find(id);
            if (tbSolicitudReembolsoGastosDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // POST: SolicitudReembolsoGastosDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = db.tbSolicitudReembolsoGastosDetalle.Find(id);
            db.tbSolicitudReembolsoGastosDetalle.Remove(tbSolicitudReembolsoGastosDetalle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
