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
    public class EstadoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: Estado
        public ActionResult Index()
        {
            try
            {
                return View(db.tbEstado.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Estado/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbEstado tbEstado = db.tbEstado.Find(id);
                if (tbEstado == null)
                {
                    return HttpNotFound();
                }
                return View(tbEstado);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "est_Id,est_Descripcion")] tbEstado tbEstado)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbEstado_Insert(tbEstado.est_Descripcion, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Gral_tbEstado_Insert_Result Estado in List)
                        Msj = Estado.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Estado", "CreatePost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        Function.BitacoraErrores("Estado", "CreatePost", UserName, Msj);
                        ModelState.AddModelError("", "Ya existe un estado con el mismo nombre.");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Estado", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
            }
            return View();

        }


        // GET: Estado/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbEstado tbEstado = db.tbEstado.Find(id);
                if (tbEstado == null)
                {
                    return HttpNotFound();
                }
                return View(tbEstado);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }
        // POST: Estado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "est_Id,est_Descripcion,est_UsuarioCrea,est_FechaCrea,est_UsuarioModifica,est_FechaModifica")] tbEstado tbEstado)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)

                    if (db.tbEstado.Any(a => a.est_Descripcion == tbEstado.est_Descripcion && a.est_Id != tbEstado.est_Id))
                    {
                        string Error = "Ya existe una estado con el mismo nombre.";
                        Function.BitacoraErrores("Estado", "EditPost", UserName, Error); 
                        ModelState.AddModelError("", Error);
                        return View(tbEstado);
                    }

                IEnumerable<Object> List = null;
                string Msj = "";
                List = db.UDP_Gral_tbEstado_Update(tbEstado.est_Id, tbEstado.est_Descripcion, EmployeeID, Function.DatetimeNow());
                foreach (UDP_Gral_tbEstado_Update_Result estado in List)
                    Msj = estado.MensajeError;
                if (Msj.StartsWith("-1"))
                {
                    Function.BitacoraErrores("Estado", "EditPost", UserName, Msj);
                    return View(tbEstado);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Estado", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbEstado);
            }



        }


        // GET: Estado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstado tbEstado = db.tbEstado.Find(id);
            if (tbEstado == null)
            {
                return HttpNotFound();
            }
            return View(tbEstado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEstado tbEstado = db.tbEstado.Find(id);
            db.tbEstado.Remove(tbEstado);
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
