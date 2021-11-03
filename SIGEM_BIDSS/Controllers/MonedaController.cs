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
    public class MonedaController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: Moneda
        public ActionResult Index()
        {
            try
            {
                return View(db.tbMoneda.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Moneda/Details/5
        public ActionResult Details(short? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbMoneda tbMoneda = db.tbMoneda.Find(id);
                if (tbMoneda == null)
                {
                    return HttpNotFound();
                }
                return View(tbMoneda);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Moneda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Moneda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tmo_Id,tmo_Abreviatura,tmo_Nombre")] tbMoneda tbMoneda)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbMoneda_Insert(tbMoneda.tmo_Id, tbMoneda.tmo_Abreviatura, tbMoneda.tmo_Nombre, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Gral_tbMoneda_Insert_Result Moneda in List)
                        Msj = Moneda.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Moneda", "CreatePost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbMoneda);
                    }
                    if (Msj.StartsWith("-2"))
                    {

                        ModelState.AddModelError("", "Ya existe una Moneda con el mismo nombre.");
                        return View(tbMoneda);
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                return View(tbMoneda);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Moneda", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
            }

        }


        // GET: Moneda/Edit/5
        public ActionResult Edit(short? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbMoneda tbMoneda = db.tbMoneda.Find(id);
                if (tbMoneda == null)
                {
                    return HttpNotFound();
                }
                return View(tbMoneda);
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");

            }
        }
        // POST: Moneda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tmo_Id,tmo_Abreviatura,tmo_Nombre,tmo_UsuarioCrea,tmo_FechaCrea,tmo_UsuarioModifica,tmo_FechaModifica")] tbMoneda tbMoneda)
        {

            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    if (db.tbMoneda.Any(a => a.tmo_Nombre == tbMoneda.tmo_Nombre && a.tmo_Id != tbMoneda.tmo_Id))
                    {
                        ModelState.AddModelError("", "Ya existe una Moneda con el mismo nombre.");
                        return View(tbMoneda);
                    }

                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbMoneda_Update(tbMoneda.tmo_Id, tbMoneda.tmo_Abreviatura, tbMoneda.tmo_Nombre, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Gral_tbMoneda_Update_Result Moneda in List)
                        Msj = Moneda.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Moneda", "EditPost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbMoneda);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(tbMoneda);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Moneda", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbMoneda);
            }
        }

        // GET: Moneda/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // POST: Moneda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            db.tbMoneda.Remove(tbMoneda);
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
