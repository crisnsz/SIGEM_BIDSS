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
    public class tbTipoPermisoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();
     

        // GET: tbTipoPermiso
        public ActionResult Index()
        {
            try
            {
                return View(db.tbTipoPermiso.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: tbTipoPermiso/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
                if (tbTipoPermiso == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoPermiso);

            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }

        }
        


        // GET: tbTipoPermiso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbTipoPermiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tperm_Id,tperm_Descripcion,tperm_UsuarioCrea,tperm_FechaCrea,tperm_UsuarioModifica,tperm_FechaModifica")] tbTipoPermiso tbTipoPermiso)
        {
            string UserName = "";       
            try
                {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                 

                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoPermiso_Insert(tbTipoPermiso.tperm_Descripcion, 1, Function.DatetimeNow());
                    foreach (UDP_Gral_tbTipoPermiso_Insert_Result Permiso in List)
                        Msj = Permiso.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("TipoPermiso", "CreatePost", UserName, Msj);
                        ModelState.AddModelError("Codigo Error" + Msj, "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        ModelState.AddModelError("", "Ya existe un Permiso con el mismo nombre.");
                        return View(tbTipoPermiso);
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
                Function.BitacoraErrores("Proveedor", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
                }

            return View(tbTipoPermiso);


        }


        // GET: tbTipoPermiso/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
                if (tbTipoPermiso == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoPermiso);
            }
            catch(Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: tbTipoPermiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tperm_Id,tperm_Descripcion,tperm_UsuarioCrea,tperm_FechaCrea,tperm_UsuarioModifica,tperm_FechaModifica")] tbTipoPermiso tbTipoPermiso)
        {

            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                if (ModelState.IsValid)
                {
                    if (db.tbTipoPermiso.Any(a => a.tperm_Descripcion == tbTipoPermiso.tperm_Descripcion && a.tperm_Id != tbTipoPermiso.tperm_Id))
                    {
                        ModelState.AddModelError("", "Ya existe un Tipo de Permiso con el mismo nombre.");
                        return View(tbTipoPermiso);
                    }

                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoPermiso_Update(tbTipoPermiso.tperm_Id, tbTipoPermiso.tperm_Descripcion, EmployeeID);
                    foreach (UDP_Gral_tbTipoPermiso_Update_Result permis in List)
                        Msj = permis.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("TipoPermiso", "EditPost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }

                    else
                    {
                        return RedirectToAction("Index");
                    }

                }
                return View(tbTipoPermiso);
            }

            catch (Exception Ex)
            {
                Function.BitacoraErrores("TipoPermiso", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
            }
        }


        // GET: tbTipoPermiso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
            if (tbTipoPermiso == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoPermiso);
        }

        // POST: tbTipoPermiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
            db.tbTipoPermiso.Remove(tbTipoPermiso);
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
