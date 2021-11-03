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
    public class CargosController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: Cargos
        public ActionResult Index()
        {
            try
            {
                var tbPuesto = db.tbPuesto.Include(t => t.tbArea);
                return View(tbPuesto.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }


        // GET: Cargos/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbPuesto tbPuesto = db.tbPuesto.Find(id);
                if (tbPuesto == null)
                {
                    return HttpNotFound();
                }
                return View(tbPuesto);
            }

            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }
        // GET: Cargos/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
                return View();
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Cargos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pto_Id,are_Id,pto_Descripcion,pto_UsuarioCrea,pto_FechaCrea,pto_UsuarioModifica,pto_FechaModifica")] tbPuesto tbPuesto)
        {
            string UserName = "";
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbPuesto.are_Id);
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<Object> List = null;
                    string ErrorMessage = "";
                    List = db.UDP_Gral_tbPuesto_Insert(tbPuesto.are_Id, tbPuesto.pto_Descripcion, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Gral_tbPuesto_Insert_Result Puesto in List)
                        ErrorMessage = Puesto.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Cargos", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    if (ErrorMessage.StartsWith("-2"))
                    {
                    
                        ModelState.AddModelError("", "Este cargo ya éxiste para esta área.");
                        return View();
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";
                      
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Cargos", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
            }

            return View(tbPuesto);
        }


        // GET: Cargos/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbPuesto tbPuesto = db.tbPuesto.Find(id);
                if (tbPuesto == null)
                {
                    return HttpNotFound();
                }
                ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbPuesto.are_Id);
                return View(tbPuesto);
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Cargos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pto_Id,are_Id,pto_Descripcion,pto_UsuarioCrea,pto_FechaCrea,pto_UsuarioModifica,pto_FechaModifica")] tbPuesto tbPuesto)
        {
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbPuesto.are_Id);


            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    if (isDuplicated(tbPuesto))
                    {
                        ModelState.AddModelError("", "Este cargo ya  éxiste para esta área.");
                        return View(tbPuesto);
                    }
                IEnumerable<Object> List = null;
                string ErrorMessage = "";
                List = db.UDP_Gral_tbPuesto_Update(tbPuesto.pto_Id, tbPuesto.are_Id, tbPuesto.pto_Descripcion, EmployeeID, Function.DatetimeNow());
                foreach (UDP_Gral_tbPuesto_Update_Result Cargo in List)
                    ErrorMessage = Cargo.MensajeError;
                if (ErrorMessage.StartsWith("-1"))
                {
                        Function.BitacoraErrores("Puesto", "EditPost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbPuesto);
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isEdited;
                    return RedirectToAction("Index");
                }
            }
                return View(tbPuesto);
            }
            catch (Exception Ex)
            {

                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbPuesto);
            }




        }

        // GET: Cargos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            if (tbPuesto == null)
            {
                return HttpNotFound();
            }
            return View(tbPuesto);
        }

        public bool isDuplicated(tbPuesto tbPuesto)
        {
            bool _boolExist = false;
            try
            {
                int _Exist = (from _tbM in db.tbPuesto where _tbM.pto_Descripcion == tbPuesto.pto_Descripcion && _tbM.pto_Id != tbPuesto.pto_Id && _tbM.are_Id == tbPuesto.are_Id select _tbM).Count();
                if (_Exist >= 1)
                {
                    return _boolExist = true;
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
            return _boolExist;
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            db.tbPuesto.Remove(tbPuesto);
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
