using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class ParametroController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: tbParametroes
        public ActionResult Index()
        {
            var parametro = db.tbParametro.ToList();
            int count = db.tbParametro.Count();
            int idPar = 0;
            
            if (count > 0)
            {
                foreach (tbParametro id in parametro)
                    idPar = id.par_Id;
                return RedirectToAction("Details/" + idPar, "Parametro");
            }
            else
                return RedirectToAction("Create", "Parametro");
        }


        // GET: tbParametroes/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // GET: tbParametroes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbParametroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "par_Id,par_NombreEmpresa,par_TelefonoEmpresa,par_CorreoEmpresa,par_CorreoEmisor,par_CorreoRRHH,par_Password,par_Servidor,par_Puerto,par_PathLogo, par_PorcentajeAdelantoSalario,par_FrecuenciaAdelantoSalario")] tbParametro tbParametro,
           HttpPostedFileBase FotoPath
           )
        {
            string UserName = "";


            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                var path = "";
                if (FotoPath == null)
                    ModelState.AddModelError("par_PathLogo", "Imagen requerida.");

                if (ModelState.IsValid)
                {
                    if (FotoPath != null)
                    {
                        if (FotoPath.ContentLength > 0)
                        {
                            if (Path.GetExtension(FotoPath.FileName).ToLower() == ".jpg" || Path.GetExtension(FotoPath.FileName).ToLower() == ".png" || Path.GetExtension(FotoPath.FileName).ToLower() == ".jpeg")
                            {
                                string Extension = Path.GetExtension(FotoPath.FileName).ToLower();
                                string Archivo = "1" + Path.GetExtension(FotoPath.FileName).ToLower();
                                path = Path.Combine(Server.MapPath("~/Content/img/"), Archivo);
                                FotoPath.SaveAs(path);
                                tbParametro.par_PathLogo = "~/Content/img/" + Archivo;
                            }
                            else
                            {
                                ModelState.AddModelError("par_PathLogo", "Formato de archivo incorrecto, favor adjuntar una fotografía con extensión .jpg");
                                return View(tbParametro);
                            }
                        }
                    }

                    IEnumerable<object> List = null;
                    var MsjError = "";

                    List = db.UDP_Conf_tbParametro_Insert(tbParametro.par_NombreEmpresa.ToUpper(), 
                                                         tbParametro.par_TelefonoEmpresa, 
                                                         tbParametro.par_CorreoEmpresa, 
                                                         tbParametro.par_CorreoEmisor, 
                                                         tbParametro.par_CorreoRRHH, 
                                                         tbParametro.par_Password, 
                                                         tbParametro.par_Servidor, 
                                                         tbParametro.par_Puerto, 
                                                         tbParametro.par_PathLogo,
                                                         tbParametro.par_PorcentajeAdelantoSalario,
                                                         tbParametro.par_FrecuenciaAdelantoSalario

                        );
                    foreach (UDP_Conf_tbParametro_Insert_Result parametro in List)
                        MsjError = parametro.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Parametro", "CreatePost", UserName, MsjError);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbParametro);
                    }
                    else
                    {
                        TempData["swalfunction"] = GeneralFunctions._isCreated;
                        return RedirectToAction("Details/"+ MsjError);
                    }

                }
                {
                    ModelState.AddModelError("par_PathLogo", "Imagen requerida.");
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Function.BitacoraErrores("Parametro", "CreatePost", UserName, ve.ErrorMessage.ToString() + " " + ve.PropertyName.ToString());
                        ModelState.AddModelError("", ve.ErrorMessage.ToString() + " " + ve.PropertyName.ToString());
                        return View(tbParametro);
                    }
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Parametro", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbParametro);
            }
            return View(tbParametro);
        }
        // GET: tbParametroes/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // POST: tbParametroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(byte? id, [Bind(Include = "par_Id,par_NombreEmpresa,par_TelefonoEmpresa,par_CorreoEmpresa,par_CorreoEmisor, par_CorreoRRHH, par_Password ,par_Servidor,par_Puerto,par_PathLogo, par_PorcentajeAdelantoSalario,par_FrecuenciaAdelantoSalario")] tbParametro tbParametro,
       HttpPostedFileBase FotoPath)

        {
            var path = "";
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                tbParametro vtbparametro = db.tbParametro.Find(id);
                IEnumerable<object> List = null;
                var MsjError = "";
                if (ModelState.IsValid)
                {
                    if (FotoPath != null)
                    {
                        if (FotoPath.ContentLength > 0)
                        {
                            if (Path.GetExtension(FotoPath.FileName).ToLower() == ".jpg" || Path.GetExtension(FotoPath.FileName).ToLower() == ".png" || Path.GetExtension(FotoPath.FileName).ToLower() == ".jpeg")
                            {
                                string Extension = Path.GetExtension(FotoPath.FileName).ToLower();
                                string Archivo = "1"+ Path.GetExtension(FotoPath.FileName).ToLower();
                                path = Path.Combine(Server.MapPath("~/Content/img/"), Archivo);
                                FotoPath.SaveAs(path);
                                tbParametro.par_PathLogo = "~/Content/img/" + Archivo;
                            }
                            else
                            {
                                tbParametro.par_PathLogo = vtbparametro.par_PathLogo;
                                ModelState.AddModelError("par_PathLogo", "Formato de archivo incorrecto, favor adjuntar una fotografía con extensión .png ó .jpg");
                                return View(tbParametro);
                            }
                        }
                    }
                    

                    
                    List = db.UDP_Conf_tbParametro_Update(tbParametro.par_Id, 
                                                            tbParametro.par_NombreEmpresa.ToUpper(),    
                                                            tbParametro.par_TelefonoEmpresa, 
                                                            tbParametro.par_CorreoEmpresa, 
                                                            tbParametro.par_CorreoEmisor, 
                                                            tbParametro.par_CorreoRRHH, 
                                                            tbParametro.par_Password, 
                                                            tbParametro.par_Servidor, 
                                                            tbParametro.par_Puerto, 
                                                            tbParametro.par_PathLogo,
                                                            tbParametro.par_PorcentajeAdelantoSalario,
                                                            tbParametro.par_FrecuenciaAdelantoSalario);
                    foreach (UDP_Conf_tbParametro_Update_Result parametro in List)
                        MsjError = parametro.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Parametro", "EditPost", UserName, MsjError);
                        ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                        return RedirectToAction("Edit");
                    }
                    else
                    {
                        TempData["swalfunction"] = GeneralFunctions._isEdited;
                        return RedirectToAction("Details/" + MsjError);
                    }
                }
                tbParametro.par_PathLogo = vtbparametro.par_PathLogo;
                return View(tbParametro);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Parametro", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return RedirectToAction("Index");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: tbParametroes/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbParametro tbParametro = db.tbParametro.Find(id);
            if (tbParametro == null)
            {
                return HttpNotFound();
            }
            return View(tbParametro);
        }

        // POST: tbParametroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            tbParametro tbParametro = db.tbParametro.Find(id);
            db.tbParametro.Remove(tbParametro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
