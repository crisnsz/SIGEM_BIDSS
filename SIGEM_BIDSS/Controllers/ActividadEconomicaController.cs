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
    public class ActividadEconomicaController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: ActividadEconomica
        public ActionResult Index()
        {
            return View(db.tbActividadEconomica.ToList());
        }

        // GET: ActividadEconomica/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // GET: ActividadEconomica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActividadEconomica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica")] tbActividadEconomica tbActividadEconomica)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<Object> List = null;
                    string ErrorMessage = "";
                    tbActividadEconomica.acte_UsuarioCrea = EmployeeID;
                    List = db.UDP_Gral_tbActividadEconomica_Insert(tbActividadEconomica.acte_Descripcion, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Gral_tbActividadEconomica_Insert_Result acti in List)
                        ErrorMessage = acti.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("ActividadEconomica", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbActividadEconomica);
                    }
                    if (ErrorMessage.StartsWith("-2"))
                    {
                       
                        ModelState.AddModelError("", "Ya existe una Actividad Ecónomica con el mismo nombre.");
                        return View(tbActividadEconomica);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("ActividadEconomica", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbActividadEconomica);
            }
            return View(tbActividadEconomica);
        }

        // GET: ActividadEconomica/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // POST: ActividadEconomica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "acte_Id,acte_Descripcion,acte_UsuarioCrea,acte_FechaCrea,acte_UsuarioModifica,acte_FechaModifica")] tbActividadEconomica tbActividadEconomica)
        {
            string UserName = "";

            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                if (ModelState.IsValid)
                    if (db.tbActividadEconomica.Any(a => a.acte_Descripcion == tbActividadEconomica.acte_Descripcion && a.acte_Id != tbActividadEconomica.acte_Id))
                    {
                        string Error = "Ya existe una Actividad Ecónomica con el mismo nombre.";
                        Function.BitacoraErrores("ActividadEconomica", "EditPost", UserName, Error);
                        ModelState.AddModelError("", Error);
                        return View(tbActividadEconomica);
                    }
                IEnumerable<Object> List = null;
                string ErrorMessage = "";
                List = db.UDP_Gral_tbActividadEconomica_Update(tbActividadEconomica.acte_Id, tbActividadEconomica.acte_Descripcion, EmployeeID, Function.DatetimeNow());
                foreach (UDP_Gral_tbActividadEconomica_Update_Result activ in List)
                    ErrorMessage = activ.MensajeError;
                if (ErrorMessage.StartsWith("-1"))
                {
                    Function.BitacoraErrores("ActividadEconomica", "EditPost", UserName, ErrorMessage);
                    return View(tbActividadEconomica);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("ActividadEconomica", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbActividadEconomica);
            }
           
        }

        // GET: ActividadEconomica/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            if (tbActividadEconomica == null)
            {
                return HttpNotFound();
            }
            return View(tbActividadEconomica);
        }

        // POST: ActividadEconomica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbActividadEconomica tbActividadEconomica = db.tbActividadEconomica.Find(id);
            db.tbActividadEconomica.Remove(tbActividadEconomica);
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
