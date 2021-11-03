using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class TipoTransporteController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions(); 
        // GET: TipoTransporte
        public ActionResult Index()
        {
            return View(db.tbTipoTransporte.ToList());
        }

        // GET: TipoTransporte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoTransporte tbTipoTransporte = db.tbTipoTransporte.Find(id);
            if (tbTipoTransporte == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoTransporte);
        }

        // GET: TipoTransporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoTransporte/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tptran_Id,tptran_Descripcion,tptran_UsuarioCrea,tptran_FechaCrea,tptran_UsuarioModifica,tptran_FechaModifica")] tbTipoTransporte tbTipoTransporte)
        {
            if (ModelState.IsValid)
            {
                string UserName = "";

                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    int EmpleId = Function.GetUser(out UserName);

                    tbTipoTransporte.tptran_UsuarioCrea = EmpleId;
                    List = db.UDP_Gral_tbTipoTransporte_Insert(tbTipoTransporte.tptran_Descripcion, EmpleId, Function.DatetimeNow());
                    foreach (UDP_Gral_tbTipoTransporte_Insert_Result trans in List)
                        Msj = trans.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {

                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    if (Msj.StartsWith("-2"))
                    {

                        ModelState.AddModelError("", "Ya existe un Tipo Transporte con el mismo nombre.");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View();
                }
            }
            return View();
        }

        // GET: TipoTransporte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoTransporte tbTipoTransporte = db.tbTipoTransporte.Find(id);
            if (tbTipoTransporte == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoTransporte);
        }

        // POST: TipoTransporte/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tptran_Id,tptran_Descripcion,tptran_UsuarioCrea,tptran_FechaCrea,tptran_UsuarioModifica,tptran_FechaModifica")] tbTipoTransporte tbTipoTransporte)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                if (ModelState.IsValid)
                {
                    if (db.tbTipoTransporte.Any(a => a.tptran_Descripcion == tbTipoTransporte.tptran_Descripcion && a.tptran_Id != tbTipoTransporte.tptran_Id))
                    {
                        ModelState.AddModelError("", "Ya existe un Tipo de Transporte con el mismo nombre.");
                        return View(tbTipoTransporte);
                    }

                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoTransporte_Update(tbTipoTransporte.tptran_Id, tbTipoTransporte.tptran_Descripcion, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Gral_tbTipoTransporte_Update_Result trans in List)
                        Msj = trans.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("TipoTransporte", "EditPost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }

                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                return View(tbTipoTransporte);
            }

            catch (Exception Ex)
            {
                Function.BitacoraErrores("TipoTransporte", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
            }


        }

        // GET: TipoTransporte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoTransporte tbTipoTransporte = db.tbTipoTransporte.Find(id);
            if (tbTipoTransporte == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoTransporte);
        }

        // POST: TipoTransporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoTransporte tbTipoTransporte = db.tbTipoTransporte.Find(id);
            db.tbTipoTransporte.Remove(tbTipoTransporte);
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
