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
    public class DeduccionInstitucionFinancieraController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: DeduccionInstitucionFinanciera
        public ActionResult Index()
        {
            var tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Include(t => t.tbEmpleado).Include(t => t.tbInstitucionFinanciera);
            return View(tbDeduccionInstitucionFinanciera.ToList());
        }

        // GET: DeduccionInstitucionFinanciera/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            if (tbDeduccionInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbDeduccionInstitucionFinanciera);
        }
        [HttpPost]
        public JsonResult InactivarInstitucionFinanciera(int insf_Id)
        {

            string UserName = "";
            IEnumerable<Object> list = null;
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(insf_Id);
                list = db.UDP_Plani_tbDeduccionInstitucionFinanciera_Update(tbDeduccionInstitucionFinanciera.deif_IdDeduccionInstFinanciera,
                                                                            tbDeduccionInstitucionFinanciera.insf_Id,
                                                                            tbDeduccionInstitucionFinanciera.emp_Id,
                                                                            tbDeduccionInstitucionFinanciera.deif_Monto,
                                                                            tbDeduccionInstitucionFinanciera.deif_Comentarios,
                                                                           EmployeeID,
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

            string UserName = "";
            IEnumerable<Object> list = null;
            try
            {

                int EmployeeID = Function.GetUser(out UserName);
                tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(insf_Id);
                list = db.UDP_Plani_tbDeduccionInstitucionFinanciera_Update(tbDeduccionInstitucionFinanciera.deif_IdDeduccionInstFinanciera,
                                                                            tbDeduccionInstitucionFinanciera.insf_Id,
                                                                            tbDeduccionInstitucionFinanciera.emp_Id,
                                                                            tbDeduccionInstitucionFinanciera.deif_Monto,
                                                                            tbDeduccionInstitucionFinanciera.deif_Comentarios,
                                                                           EmployeeID,
                                                                            Function.DatetimeNow(),
                                                                         GeneralFunctions.instfinAceptar);
            }
            catch (Exception Ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: DeduccionInstitucionFinanciera/Create
        public ActionResult Create()
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.insf_Id = new SelectList(db.tbInstitucionFinanciera, "insf_Id", "insf_Nombre");
            ViewBag.Empleado = db.tbEmpleado.Where(x => x.est_Id == 5).ToList();
            return View();
        }

        // POST: DeduccionInstitucionFinanciera/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deif_IdDeduccionInstFinanciera,emp_Id,insf_Id,deif_Monto,deif_Comentarios,deif_UsuarioCrea,deif_FechaCrea,deif_UsuarioModifica,deif_FechaModifica,deif_Activo")] tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera)
        {

            string UserName = "";
            try
            {
        
        
                ViewBag.insf_Id= new SelectList(db.tbInstitucionFinanciera, "insf_Id", "insf_Nombre");
                ViewBag.Empleado = db.tbEmpleado.Where(x => x.est_Id == 5).ToList();
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<object> _List = null;
                    string ErrorMessage = "";
                    _List = db.UDP_Plani_tbDeduccionInstitucionFinanciera_Insert(tbDeduccionInstitucionFinanciera.insf_Id,tbDeduccionInstitucionFinanciera.emp_Id ,tbDeduccionInstitucionFinanciera.deif_Monto,tbDeduccionInstitucionFinanciera.deif_Comentarios, EmployeeID, Function.DatetimeNow(), GeneralFunctions.Activo);
                    foreach (UDP_Plani_tbDeduccionInstitucionFinanciera_Insert_Result Area in _List)
                        ErrorMessage = Area.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("DeduccionFinanciera", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbDeduccionInstitucionFinanciera);
                    }
                  
                    else
                    {

                        TempData["swalfunction"] = "true";

                        return RedirectToAction("Index");
                    }

                }
                return View(tbDeduccionInstitucionFinanciera);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("DeduccionFinanciera", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbDeduccionInstitucionFinanciera);
            }

        }



        // GET: DeduccionInstitucionFinanciera/Edit/5
        public ActionResult Edit(int? id)
        {
            
      
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            if (tbDeduccionInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }

            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbDeduccionInstitucionFinanciera.emp_Id);
            ViewBag.insf_Id = new SelectList(db.tbInstitucionFinanciera, "insf_Id", "insf_Nombre", tbDeduccionInstitucionFinanciera.insf_Id);
            ViewBag.Empleado = db.tbEmpleado.Where(x => x.est_Id == 5).ToList();
            return View(tbDeduccionInstitucionFinanciera);
        }

        // POST: DeduccionInstitucionFinanciera/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deif_IdDeduccionInstFinanciera,emp_Id,insf_Id,deif_Monto,deif_Comentarios,deif_UsuarioCrea,deif_FechaCrea,deif_UsuarioModifica,deif_FechaModifica,deif_Activo")] tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera)
        {
            string UserName = "";
            try
            {
                ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbDeduccionInstitucionFinanciera.emp_Id);
                ViewBag.insf_Id = new SelectList(db.tbInstitucionFinanciera, "insf_Id", "insf_Nombre", tbDeduccionInstitucionFinanciera.insf_Id);
                ViewBag.Empleado = db.tbEmpleado.Where(x => x.est_Id == 5).ToList();
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                    if (db.tbDeduccionInstitucionFinanciera.Any(a => a.insf_Id == tbDeduccionInstitucionFinanciera.insf_Id && a.insf_Id != tbDeduccionInstitucionFinanciera.insf_Id))
                    {
                       
                    }

                IEnumerable<Object> List = null;
                string Msj = "";
                List = db.UDP_Plani_tbDeduccionInstitucionFinanciera_Update(tbDeduccionInstitucionFinanciera.deif_IdDeduccionInstFinanciera,
                                                                            tbDeduccionInstitucionFinanciera.insf_Id,
                                                                            tbDeduccionInstitucionFinanciera.emp_Id,
                                                                            tbDeduccionInstitucionFinanciera.deif_Monto,
                                                                            tbDeduccionInstitucionFinanciera.deif_Comentarios,
                                                                           EmployeeID,
                                                                            Function.DatetimeNow(),
                                                                        tbDeduccionInstitucionFinanciera.deif_Activo);
                foreach (UDP_Plani_tbDeduccionInstitucionFinanciera_Update_Result inst in List)
                    Msj = inst.MensajeError;
                if (Msj.StartsWith("-1"))
                {
                    Function.BitacoraErrores("InstitucionFinanciera", "EditPost", UserName, Msj);
                    return View(tbDeduccionInstitucionFinanciera);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("InstitucionFinanciera", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
            }
        }
    

        // GET: DeduccionInstitucionFinanciera/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            if (tbDeduccionInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbDeduccionInstitucionFinanciera);
        }

        // POST: DeduccionInstitucionFinanciera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            db.tbDeduccionInstitucionFinanciera.Remove(tbDeduccionInstitucionFinanciera);
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
