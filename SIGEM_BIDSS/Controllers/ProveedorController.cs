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
    public class ProveedorController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: Proveedor
        public ActionResult Index()
        {
            var tbProveedor = db.tbProveedor.Include(t => t.tbActividadEconomica).Include(t => t.tbMunicipio);
            return View(tbProveedor.ToList());
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        [HttpPost]
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = (from x in db.tbMunicipio where x.dep_codigo == CodDepartamento select new { mun_codigo = x.mun_codigo, mun_nombre = x.mun_nombre }).ToList();
            /*db.tbMunicipio.Where(x=> x.dep_codigo==CodDepartamento).ToList();*/
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: Proveedor/Create
        public ActionResult Create()
        {
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            return View();
        }

        // POST: Proveedor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,mun_codigo,prov_Email,prov_Telefono,prov_RTN,acte_Id,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor, string dep_codigo)
        {

            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (tbProveedor.mun_codigo == "Seleccione")
                    ModelState.AddModelError("mun_codigo", "El campo Municipio es obligatorio.");
                else
                    ViewBag.munCodigo = tbProveedor.mun_codigo;

                if (String.IsNullOrEmpty(dep_codigo))
                    ModelState.AddModelError("prov_UsuarioCrea", "El campo Departamento es obligatorio.");
                if (ModelState.IsValid)
                {
                    ViewBag.selectedMun = tbProveedor.mun_codigo;
                    ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Inv_tbProveedor_Insert(tbProveedor.prov_Id,
                                                        tbProveedor.prov_Nombre,
                                                        tbProveedor.prov_NombreContacto,
                                                        tbProveedor.prov_Direccion,
                                                        tbProveedor.mun_codigo,
                                                        tbProveedor.prov_Email,
                                                        tbProveedor.prov_Telefono,
                                                        tbProveedor.prov_RTN,
                                                        tbProveedor.acte_Id,
                                                        EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Inv_tbProveedor_Insert_Result Permiso in List)
                        Msj = Permiso.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Proveedor", "CreatePost", UserName, Msj);
                        ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_codigo);
                        ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
                        ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);
                        return View();
                    }
                    if (Msj.StartsWith("-2"))
                    {

                        ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_codigo);
                        ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
                        ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);
                        ModelState.AddModelError("", "Este RTN ya fue registrado.");
                        return View(tbProveedor);
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
            ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_codigo);
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);

            if (dep_codigo != "ajax")
                return View("Create");
            else
                return Json("Create");

        }

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            string _depto = db.tbMunicipio.Find(tbProveedor.mun_codigo).dep_codigo;
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }

            ViewBag.mun_codigo = new SelectList(db.tbMunicipio.Where(x => x.dep_codigo == _depto), "mun_codigo", "mun_nombre", tbProveedor.mun_codigo);
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion",tbProveedor.acte_Id);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre", tbProveedor.mun_codigo);
            string depCod = (from _dep in db.tbMunicipio where _dep.mun_codigo == tbProveedor.mun_codigo select _dep.dep_codigo).FirstOrDefault();
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", depCod);
            return View(tbProveedor);
        }

        // POST: Proveedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,mun_codigo,prov_Email,prov_Telefono,prov_RTN,acte_Id")] tbProveedor tbProveedor)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);
                ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                if (ModelState.IsValid)
                {
                    if (db.tbProveedor.Any(a => a.prov_RTN == tbProveedor.prov_RTN && a.prov_Id != tbProveedor.prov_Id))
                    {
                        string Error = "Ya existe un proveedor con este RTN.";
                        Function.BitacoraErrores("Proveedor", "EditPost", UserName, Error);
                        ModelState.AddModelError("", Error);
                        return View(tbProveedor);
                    }
                  

                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Inv_tbProveedor_Update(tbProveedor.prov_Id,
                                                   tbProveedor.prov_Nombre,
                                                   tbProveedor.prov_NombreContacto,
                                                   tbProveedor.prov_Direccion,
                                                   tbProveedor.mun_codigo,
                                                   tbProveedor.prov_Email,
                                                   tbProveedor.prov_Telefono,
                                                   tbProveedor.prov_RTN,
                                                   tbProveedor.acte_Id,
                                                   EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Inv_tbProveedor_Update_Result Permiso in List)
                        Msj = Permiso.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Proveedor", "EditPost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
                        ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);
                        ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
                        ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                        return View(tbProveedor);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(tbProveedor);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Proveedor", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbProveedor);
            }
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // POST: Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            db.tbProveedor.Remove(tbProveedor);
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
