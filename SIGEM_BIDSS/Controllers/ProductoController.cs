using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class ProductoController : BaseController

    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        GeneralFunctions Function = new GeneralFunctions();
        // GET: Producto
        public ActionResult Index()
        {
            var tbProducto = db.tbProducto.Include(t => t.tbUnidadMedida).Include(t => t.tbProductoSubcategoria).Include(t => t.tbProveedor);
            return View(tbProducto.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            return View(tbProducto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {


            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria.Where(x => x.pcat_EsActivo == true), "pcat_Id", "pcat_Descripcion");
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");

            return View();
        }
        [HttpPost]
        public JsonResult GetValue(string pcat_Id, string pscat_Id)
        {
            ObjectParameter Output = new ObjectParameter("prod_Codigo", typeof(string));
            var Categoria = Convert.ToInt32(pcat_Id);
            var SubCategoria = Convert.ToInt32(pscat_Id);
           
            //var MsjError = "";
            var list = db.UDP_Inv_tbProducto_ValorCodigo(Categoria, SubCategoria, Output);
            foreach (UDP_Inv_tbProducto_ValorCodigo_Result Producto in list)
                ViewBag.prod_Codigo = Producto.MensajeError;
            //ViewBag.prod_Codigo = list;
            return Json(ViewBag.prod_Codigo, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetSubCategoriaProducto(int idcate)
        {
            var list = (from x in db.tbProductoSubcategoria where x.pcat_Id == idcate select new { pscat_Id = x.pscat_Id, pscat_Descripcion = x.pscat_Descripcion }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "prod_CodigoBarras,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prov_Id,prod_EsActivo,prod_RazonInactivacion,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica," +
            "prod_FechaModifica,pcat_Id")] tbProducto tbProducto ,string pcat_Id)
        {
            string UserName = "";

           
            if (String.IsNullOrEmpty(pcat_Id))
                ModelState.AddModelError("prod_UsuarioCrea", "El campo Categoría es obligatorio.");
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (db.tbProducto.Any(a => a.prod_CodigoBarras == tbProducto.prod_CodigoBarras))
                {
                    ModelState.AddModelError("", "El Codigo de Barras ya Existe.");
                }
                if (ModelState.IsValid)
                {
                    
                    
                    string MsjError = "";
                    IEnumerable<object> List = null;
                    List = db.UDP_Inv_tbProducto_Insert(
                                                       
                                                        tbProducto.prod_CodigoBarras,
                                                        tbProducto.prod_Descripcion.ToUpper(),
                                                        tbProducto.prod_Marca.ToUpper(),
                                                        tbProducto.prod_Modelo.ToUpper(),
                                                        tbProducto.prod_Talla.ToUpper(),
                                                        tbProducto.prod_Color.ToUpper(),
                                                        tbProducto.pscat_Id,
                                                        tbProducto.uni_Id,
                                                        tbProducto.prov_Id,
                                                        GeneralFunctions.Activo,
                                                        tbProducto.prod_RazonInactivacion,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow()
                                                            );
                    foreach (UDP_Inv_tbProducto_Insert_Result Producto in List)
                        MsjError = Producto.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                        ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                        ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
                        ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Descripcion");
                        ModelState.AddModelError("", "No se Pudo Insertar el Registro, Favor Contacte al Administrador.");
                        return View(tbProducto);
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
                ModelState.AddModelError("", "No se Pudo Insertar el Registro, Favor Contacte al Administrador.");

                ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
                ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Descripcion");
                return View(tbProducto);
            }
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Descripcion", pcat_Id);
            ViewBag.pscat_Id = new SelectList(db.tbMunicipio, "pscat_Id", "pcat_Id", tbProducto.pscat_Id);
            return View(tbProducto);
            
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbProducto tbProducto = db.tbProducto.Find(id);

            if (tbProducto == null)
            {
                return HttpNotFound();
            }

            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
            ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Descripcion");
            return View(tbProducto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "prod_Id,prod_Codigo,prod_CodigoBarras,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prov_Id,prod_EsActivo,prod_RazonInactivacion,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica, pcat_Id")] tbProducto tbProducto, string pcat_Id)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
              
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Inv_tbProducto_Update(tbProducto.prod_Id,
                                                        tbProducto.prod_CodigoBarras,
                                                         tbProducto.prod_Descripcion.ToUpper(),
                                                        tbProducto.prod_Marca.ToUpper(),
                                                        tbProducto.prod_Modelo.ToUpper(),
                                                        tbProducto.prod_Talla.ToUpper(),
                                                        tbProducto.prod_Color.ToUpper(),
                                                        tbProducto.pscat_Id,
                                                        tbProducto.uni_Id,
                                                        tbProducto.prov_Id,
                                                        GeneralFunctions.Activo,
                                                        tbProducto.prod_RazonInactivacion,
                                                       EmployeeID,
                                                        Function.DatetimeNow()
                                                        );
                    foreach (UDP_Inv_tbProducto_Update_Result producto in List)
                        Msj = producto.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Producto", "EditPost", UserName, Msj);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
                        ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
                        ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
                        ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Descripcion");
                        return View(tbProducto);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
                ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
                ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
                ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Descripcion");
                return View(tbProducto);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Producto", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbProducto);
            }
          

        }
        [HttpPost]
        public JsonResult GetCategoriaProducto(int codsubcategoria)
        {
            var list = db.UDP_Inv_tbProducto_GetCategoria(codsubcategoria).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            return View(tbProducto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProducto tbProducto = db.tbProducto.Find(id);
            db.tbProducto.Remove(tbProducto);
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
        [HttpPost]
        public JsonResult EstadoInactivar(tbProducto tbProducto, string Razon_Inactivacion)
        {
            tbProducto vProducto = db.tbProducto.Find(tbProducto.prod_Id);
            try
            {
                var list = db.UDP_Inv_tbProducto_Update_RazonInactivacion(tbProducto.prod_Id, GeneralFunctions.Inactivo, Razon_Inactivacion, vProducto.prod_UsuarioCrea, vProducto.prod_FechaCrea, Function.GetUser(), Function.DatetimeNow()).ToList();
                return Json("Éxito", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {

                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InactivarProducto(int? id)
        {

            try
            {
                tbProducto obj = db.tbProducto.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProducto_Estado(id, GeneralFunctions.Inactivo);
                foreach (UDP_Inv_tbProducto_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["smserror"] = "No se puede cambiar el estado del dato porque está en uso.";
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }
        }

        public ActionResult EstadoActivar(int? id)
        {
            try
            {
                tbProducto obj = db.tbProducto.Find(id);
                tbProducto producto = new tbProducto();
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProducto_Estado(id, GeneralFunctions.Activo);
                foreach (UDP_Inv_tbProducto_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro contacte con el administrador");
                return RedirectToAction("Edit/" + id);
            }
            //return RedirectToAction("Index");
        }
    }
}
