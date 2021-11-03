using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class ProductoCategoriaController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: ProductoCategoria
        public ActionResult Index()
        {
            return View(db.tbProductoCategoria.ToList());
        }

        // GET: ProductoCategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // GET: ProductoCategoria/Create
        public ActionResult Create()
        {
            Session["tbProductoSubcategoria"] = null;
            return View();
        }

        // POST: ProductoCategoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pcat_Id,pcat_Descripcion,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica,pcat_EsActivo")] tbProductoCategoria tbProductoCategoria)
        {
            IEnumerable<object> cate = null;
            IEnumerable<object> sub = null;
            string MsjError = "";
            string mensajeDetail = "";
            var listaSubCategoria = (List<tbProductoSubcategoria>)Session["tbProductoSubcategoria"];
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        cate = db.UDP_Inv_tbProductoCategoria_Insert(tbProductoCategoria.pcat_Descripcion.ToUpper(), Function.GetUser(), Function.DatetimeNow());
                        foreach (UDP_Inv_tbProductoCategoria_Insert_Result categoria in cate)
                            MsjError = categoria.MensajeError;
                        if (MsjError.StartsWith("-1"))
                        {
                           
                            ModelState.AddModelError("", "Ya existe una Categoría con ese nombre, agregue otra.");
                            return View(tbProductoCategoria);
                        }
                        else
                        {
                            if (listaSubCategoria != null)
                            {
                                if (listaSubCategoria.Count > 0)
                                {
                                    foreach (tbProductoSubcategoria subcategoria in listaSubCategoria)
                                    {
                                        sub = db.UDP_Inv_tbProductoSubcategoria_Insert(subcategoria.pscat_Descripcion.ToUpper()
                                                                                    , Convert.ToInt16(MsjError),
                                                                                    Function.GetUser(), Function.DatetimeNow()
                                                                                    );
                                        foreach (UDP_Inv_tbProductoSubcategoria_Insert_Result ProdSubCate in sub)
                                            mensajeDetail = ProdSubCate.MensajeError;
                                        if (mensajeDetail.StartsWith("-1"))
                                        {
                                           
                                            ModelState.AddModelError("", "No se pudo insertar el registro detalle, se encontraron Datos duplicados.");
                                            return View(tbProductoCategoria);
                                        }
                                    }
                                }
                            }
                            _Tran.Complete();
                        }

                    }
                    catch (Exception Ex)
                    {

                        Ex.Message.ToString();
                       
                        ModelState.AddModelError("", "2. No se pudo insertar el registro, favor contacte al administrador");
                        return View(tbProductoCategoria);
                    }
                }
                TempData["swalfunction"] = GeneralFunctions._isCreated;
                return RedirectToAction("Index");
            }
            return View(tbProductoCategoria);
        }


        // GET: ProductoCategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            Session["tbProductoSubcategoria"] = null;
            return View(tbProductoCategoria);
        }
        [HttpPost]
        public JsonResult GuardarSubCategoria(tbProductoSubcategoria tbsubcategoria)
        {

            List<tbProductoSubcategoria> sessionsubCate = new List<tbProductoSubcategoria>();
            var list = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];
            if (list == null)
            {
                sessionsubCate.Add(tbsubcategoria);
                Session["tbProductoSubCategoria"] = sessionsubCate;
            }
            else
            {
                list.Add(tbsubcategoria);
                Session["tbProductoSubCategoria"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetSubCate(int pscat_Id)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SDP_tbProductoSubcategoria_Select(pscat_Id).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
         
        }
        [HttpPost]
        public JsonResult UpdateSubCategoria(tbProductoSubcategoria EditarSubCategoria)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;

                tbProductoSubcategoria subcater = db.tbProductoSubcategoria.Find(EditarSubCategoria.pscat_Id);
                list = db.UDP_Inv_tbProductoSubcategoria_Update(
                                                        EditarSubCategoria.pscat_Id,
                                                        EditarSubCategoria.pscat_Descripcion,
                                                       subcater.pcat_Id,
                                                      Function.GetUser(), Function.DatetimeNow()
                    );
                foreach (UDP_Inv_tbProductoSubcategoria_Update_Result subcate in list)
                    Msj = subcate.MensajeError;
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json(Msj, JsonRequestBehavior.AllowGet);

        }

        // POST: ProductoCategoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pcat_Id,pcat_Descripcion,pcat_UsuarioCrea,pcat_FechaCrea,pcat_UsuarioModifica,pcat_FechaModifica,pcat_EsActivo")] tbProductoCategoria tbProductoCategoria)
        {
            IEnumerable<object> cate = null;
            IEnumerable<object> subcate = null;
            string MsjError = "";
            string MensajeError = "";
            var List = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        cate = db.UDP_Inv_tbProductoCategoria_Update(tbProductoCategoria.pcat_Id,
                                              tbProductoCategoria.pcat_Descripcion
                                              , Function.GetUser(),
                                              Function.DatetimeNow());
                        foreach (UDP_Inv_tbProductoCategoria_Update_Result ProductoCategoria in cate)
                            MsjError = ProductoCategoria.MensajeError;

                        if (MsjError.StartsWith("-1"))
                        {
                          
                            ModelState.AddModelError("", "El Nombre de la Categoría ya existe.");
                            return View(tbProductoCategoria);
                        }
                        else
                        {
                            if (List != null && List.Count > 0)
                            {
                                foreach (tbProductoSubcategoria subcategoria in List)
                                {
                                    subcate = db.UDP_Inv_tbProductoSubcategoria_Insert(subcategoria.pscat_Descripcion
                                                                                , Convert.ToInt16(MsjError),
                                                                                Function.GetUser(), Function.DatetimeNow()
                                                                                
                                                                                );

                                    foreach (UDP_Inv_tbProductoSubcategoria_Insert_Result ProdSubCate in subcate)
                                        MensajeError = ProdSubCate.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {
                                        
                                        ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                        return View(tbProductoCategoria);
                                    }
                                }
                            }
                            _Tran.Complete();
                            TempData["swalfunction"] = GeneralFunctions._isEdited;
                            return RedirectToAction("Edit/" + MsjError);
                        }
                    }
                    catch (Exception Ex)
                    {
                       
                        ModelState.AddModelError("", "2. No se pudo actualizar el registro, favor contacte al administrador.");
                        return RedirectToAction("Edit/" + MsjError);
                    }
                }
            }
            return View(tbProductoCategoria);
        }

        // GET: ProductoCategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            if (tbProductoCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tbProductoCategoria);
        }

        // POST: ProductoCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProductoCategoria tbProductoCategoria = db.tbProductoCategoria.Find(id);
            db.tbProductoCategoria.Remove(tbProductoCategoria);
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

        public ActionResult EliminarProductoCategoria(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";

                list = db.UDP_Inv_tbProductoCategoria_Delete(id);
                foreach (UDP_Inv_tbProductoCategoria_Delete_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                   
                    TempData["swalfunction"] = GeneralFunctions._isDependenciaCate;
                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Edit/" + id);
                }

                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isDelete;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se puede borrar el registro");
                return RedirectToAction("Index");
            }




        }
        ///APLICAR ESTE DELETE 
        public ActionResult DeteleSub(int? id)
        {

            try
            {
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Delete(id);
                foreach (UDP_Inv_tbProductoSubCategoria_Delete_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                 
                    TempData["swalfunction"] = GeneralFunctions._isDependenciasSubCate;
                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Index");
                }

                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isDelete;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se puede borrar el registro");
                return RedirectToAction("Index");
            }

        }

        public ActionResult ActivarCateValidacion(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, Function.GetUser(), Function.DatetimeNow(), GeneralFunctions.CategoriaActivo);
                foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    
                    ViewBag.smserror = TempData["smserror"];

                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isActiva;
                    return RedirectToAction("Edit/" + id);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + id);
            }


            //return RedirectToAction("Index");
        }

        public ActionResult ActivarSubValidacion(int? id)
        {

            try
            {
                tbProductoSubcategoria objeto = db.tbProductoSubcategoria.Find(id);
                var idcate = objeto.pcat_Id;
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado(id, GeneralFunctions.SubcategoriaActivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbProductoSubCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + idcate);
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isActiva;
                    return RedirectToAction("Edit/" + idcate);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Index");
            }
        }

        public ActionResult InactivarSubValidacion(int? id)
        {

            try
            {
                tbProductoSubcategoria objeto = db.tbProductoSubcategoria.Find(id);
                var idcate = objeto.pcat_Id;
                tbProductoSubcategoria obj = db.tbProductoSubcategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoSubCategoria_Update_Estado(id, GeneralFunctions.SubcategoriaInactivo, Function.GetUser(), Function.DatetimeNow());
                foreach (UDP_Inv_tbProductoSubCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {
                    TempData["swalfunction"] = GeneralFunctions._isDependenciasSubCateIn;
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + idcate);
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isInactiva;
                    return RedirectToAction("Edit/" + idcate);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Index");
            }
        }


        public ActionResult InactivarCateValidacion(int? id)
        {

            try
            {
                tbProductoCategoria obj = db.tbProductoCategoria.Find(id);
                IEnumerable<object> list = null;
                var MsjError = "";
                list = db.UDP_Inv_tbProductoCategoria_Update_Estado(id, Function.GetUser(), Function.DatetimeNow(), GeneralFunctions.SubcategoriaInactivo);
                foreach (UDP_Inv_tbProductoCategoria_Update_Estado_Result obje in list)
                    MsjError = obje.MensajeError;

                if (MsjError.StartsWith("-2"))
                {

                    TempData["swalfunction"] = GeneralFunctions._isDependenciaCateIn;
                  
                    ModelState.AddModelError("", "No se Actualizo el registro");
                    return RedirectToAction("Edit/" + id);
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isInactiva;
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

        [HttpPost]
        public JsonResult removeSubCategoria(tbProductoSubcategoria borrado)
        {
            var list = (List<tbProductoSubcategoria>)Session["tbProductoSubCategoria"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.pscat_Id == borrado.pscat_Id);
                list.Remove(itemToRemove);
                Session["tbProductoSubCategoria"] = list;
            }

            return Json(list, JsonRequestBehavior.AllowGet);

        }


    }
}
