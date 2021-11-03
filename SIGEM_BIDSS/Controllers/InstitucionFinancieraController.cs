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
    public class InstitucionFinancieraController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: InstitucionFinanciera
        public ActionResult Index()
        {
            return View(db.tbInstitucionFinanciera.ToList());
        }

        // GET: InstitucionFinanciera/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            if (tbInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionFinanciera);
        }


        //INACTIVAR EMPLEADO
        [HttpPost]
        public JsonResult InactivarInstitucionFinanciera(int insf_Id)
        {
            IEnumerable<Object> list = null;
            try
            {
                tbInstitucionFinanciera institucionfinanciera = db.tbInstitucionFinanciera.Find(insf_Id);
                list = db.UDP_Plani_tbInstitucionFinanciera_Update(insf_Id, institucionfinanciera.insf_Nombre,
                                                                         institucionfinanciera.insf_Contacto,
                                                                         institucionfinanciera.insf_Telefono,
                                                                         institucionfinanciera.insf_Correo, 1,
                                                                            Function.DatetimeNow(),
                                                                         GeneralFunctions.instfinDenegar);
            }
            catch (Exception Ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActivarInstitucionFinanciera(int insf_Id)
        {
            IEnumerable<Object> list = null;
            try
            {
                tbInstitucionFinanciera institucionfinanciera = db.tbInstitucionFinanciera.Find(insf_Id);
                list = db.UDP_Plani_tbInstitucionFinanciera_Update(insf_Id, institucionfinanciera.insf_Nombre,
                                                                         institucionfinanciera.insf_Contacto,
                                                                         institucionfinanciera.insf_Telefono,
                                                                         institucionfinanciera.insf_Correo, 1,
                                                                            Function.DatetimeNow(),
                                                                         GeneralFunctions.instfinAceptar);
            }
            catch (Exception Ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: InstitucionFinanciera/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstitucionFinanciera/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "insf_Id,insf_Nombre,insf_Contacto,insf_Telefono,insf_Correo,insf_Activo,insf_UsuarioCrea,insf_FechaCrea,insf_UsuarioModifica,insf_FechaModifica")] tbInstitucionFinanciera tbInstitucionFinanciera)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Plani_tbInstitucionFinanciera_Insert(tbInstitucionFinanciera.insf_Id, tbInstitucionFinanciera.insf_Nombre,
                                                                         tbInstitucionFinanciera.insf_Contacto,
                                                                         tbInstitucionFinanciera.insf_Telefono,
                                                                         tbInstitucionFinanciera.insf_Correo, 
                                                                         EmployeeID,
                                                                         Function.DatetimeNow(),
                                                                         GeneralFunctions.Activo
                                                                    );
                    foreach (UDP_Plani_tbInstitucionFinanciera_Insert_Result TipoSangre in List)
                        Msj = TipoSangre.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("InstitucionFinanciera", "CreatePost", UserName, Msj);
                        return View(tbInstitucionFinanciera);
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        ModelState.AddModelError("", "Ya existe una Institución con el mismo nombre.");
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
                Function.BitacoraErrores("InstitucionFinanciera", "CreatePost", UserName, Ex.Message.ToString());
               
                return View();
            }
            return View();
        }

        // GET: InstitucionFinanciera/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            if (tbInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionFinanciera);
        }

        // POST: InstitucionFinanciera/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "insf_Id,insf_Nombre,insf_Contacto,insf_Telefono,insf_Correo,insf_Activo")] tbInstitucionFinanciera tbInstitucionFinanciera)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                
                if (ModelState.IsValid)
                {
                    if (db.tbInstitucionFinanciera.Any(a => a.insf_Nombre == tbInstitucionFinanciera.insf_Nombre && a.insf_Id != tbInstitucionFinanciera.insf_Id))
                    {
                        ModelState.AddModelError("", "Ya existe una Institución con el mismo nombre.");
                        return View(tbInstitucionFinanciera);
                    }


                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Plani_tbInstitucionFinanciera_Update(tbInstitucionFinanciera.insf_Id, tbInstitucionFinanciera.insf_Nombre,
                                                                     tbInstitucionFinanciera.insf_Contacto,
                                                                     tbInstitucionFinanciera.insf_Telefono,
                                                                     tbInstitucionFinanciera.insf_Correo, EmployeeID,
                                                                        Function.DatetimeNow(),
                                                                     GeneralFunctions.Activo);
                    foreach (UDP_Plani_tbInstitucionFinanciera_Update_Result inst in List)
                        Msj = inst.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("InstitucionFinanciera", "EditPost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                     
                        return View(tbInstitucionFinanciera);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(tbInstitucionFinanciera);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("InstitucionFinanciera", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbInstitucionFinanciera);
            }

        }

        // GET: InstitucionFinanciera/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            if (tbInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionFinanciera);
        }

        // POST: InstitucionFinanciera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            db.tbInstitucionFinanciera.Remove(tbInstitucionFinanciera);
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
