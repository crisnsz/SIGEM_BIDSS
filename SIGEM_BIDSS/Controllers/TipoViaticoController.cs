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
    [Authorize]
    public class TipoViaticoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        Models.Helpers Function = new Models.Helpers();
        // GET: TipoViatico
        public ActionResult Index()
        {
            try
            {
                return View(db.tbTipoViatico.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: TipoViatico/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
                if (tbTipoViatico == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoViatico);
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: TipoViatico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoViatico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tpv_Id,tpv_Descripcion,tpv_UsuarioCrea,tpv_FechaCrea,tpv_UsuarioModifica,tpv_FechaModifica")] tbTipoViatico tbTipoViatico)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoViatico_Insert(tbTipoViatico.tpv_Descripcion, 1, Function.DatetimeNow());
                    foreach (UDP_Gral_tbTipoViatico_Insert_Result tbViatico in List)
                        Msj = tbViatico.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        ModelState.AddModelError("", "Ya existe un tipo de viático con el mismo nombre.");
                        return View();
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View();
                }

            return View(tbTipoViatico);



        }

        // GET: TipoViatico/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
                if (tbTipoViatico == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoViatico);
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: TipoViatico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tpv_Id,tpv_Descripcion,tpv_UsuarioModifica,tpv_FechaModifica")] tbTipoViatico tbTipoViatico)
        {
            if (ModelState.IsValid)
                if (db.tbTipoViatico.Any(a => a.tpv_Descripcion == tbTipoViatico.tpv_Descripcion && a.tpv_Id != tbTipoViatico.tpv_Id))
                {
                    ModelState.AddModelError("", "Ya existe un tipo de viático con el mismo nombre.");
                    return View(tbTipoViatico);
                }

            try
            {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoViatico_Update(tbTipoViatico.tpv_Id, tbTipoViatico.tpv_Descripcion, 1, Function.DatetimeNow());
                    foreach (UDP_Gral_tbTipoViatico_Update_Result Viatico in List)
                        Msj = Viatico.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                      
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

            return View(tbTipoViatico);


        }


        // GET: TipoViatico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
            if (tbTipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoViatico);
        }

        // POST: TipoViatico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
            db.tbTipoViatico.Remove(tbTipoViatico);
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
