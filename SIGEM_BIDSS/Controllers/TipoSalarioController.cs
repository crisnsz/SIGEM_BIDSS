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
    public class TipoSalarioController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        Models.Helpers Function = new Models.Helpers();
        // GET: TipoSalario
        public ActionResult Index()
        {
            try
            {
                return View(db.tbTipoSalario.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }
        // GET: TipoSalario/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
                if (tbTipoSalario == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoSalario);
            }

            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            
          }
        }
        
        // GET: TipoSalario/Create
            public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSalario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tpsal_id,tpsal_Descripcion,tpsal_UsuarioCrea,tpsal_FechaCrea,tpsal_UsuarioModifica,tpsal_FechaModifica")] tbTipoSalario tbTipoSalario)
        {
            {
                if (ModelState.IsValid)
                    try
                    {
                        IEnumerable<Object> List = null;
                        string Msj = "";
                        List = db.UDP_Gral_tbTipoSalario_Insert(tbTipoSalario.tpsal_id,tbTipoSalario.tpsal_Descripcion, 1, Function.DatetimeNow());
                        foreach (UDP_Gral_tbTipoSalario_Insert_Result Permiso in List)
                            Msj = Permiso.MensajeError;
                        if (Msj.StartsWith("-1"))
                        {
                            ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                            return View();
                        }
                        if (Msj.StartsWith("-2"))
                        {
                            ModelState.AddModelError("", "Ya existe un Tipo de salario con el mismo nombre.");
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

                return View(tbTipoSalario);


            }

        }
            // GET: TipoSalario/Edit/5
            public ActionResult Edit(int? id)
        {
            try { 

              if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
            if (tbTipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalario);
            }
            catch(Exception EX)
            {
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: TipoSalario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tpsal_id,tpsal_Descripcion,tpsal_UsuarioCrea,tpsal_FechaCrea,tpsal_UsuarioModifica,tpsal_FechaModifica")] tbTipoSalario tbTipoSalario)
        {

            {
                if (ModelState.IsValid)


                    if (db.tbTipoSalario.Any(a => a.tpsal_Descripcion == tbTipoSalario.tpsal_Descripcion && a.tpsal_id != tbTipoSalario.tpsal_id))
                    {
                        ModelState.AddModelError("", "Ya existe un Tipo de salario con el mismo nombre.");
                        return View(tbTipoSalario);
                    }
                try
                    {
                        IEnumerable<Object> List = null;
                        string Msj = "";
                        List = db.UDP_Gral_tbTipoSalario_Update(tbTipoSalario.tpsal_id,tbTipoSalario.tpsal_Descripcion, 1, Function.DatetimeNow());
                        foreach (UDP_Gral_tbTipoSalario_Update_Result Permiso in List)
                            Msj = Permiso.MensajeError;
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

                return View(tbTipoSalario);


            }


        }

        // GET: TipoSalario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
            if (tbTipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalario);
        }

        // POST: TipoSalario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
            db.tbTipoSalario.Remove(tbTipoSalario);
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
