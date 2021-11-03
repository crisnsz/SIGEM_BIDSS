using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;
using System.Transactions;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    public class DepartamentoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        GeneralFunctions Function = new GeneralFunctions();

        // GET: Departamento
        public ActionResult Index()
        {
            return View(db.tbDepartamento.ToList());
        }

        // GET: Departamento/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            Session["tbMunicipio"] = null;
            return View();
        }

        // POST: Departamento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {

            IEnumerable<object> list = null;
            IEnumerable<object> lista = null;
            string UserName = "";
            string MensajeError = "";
            string MsjError = "";
            var listMunicipios = (List<tbMunicipio>)Session["tbMunicipio"];



            if (db.tbDepartamento.Any(a => a.dep_Nombre == tbDepartamento.dep_Nombre))
            {
                ModelState.AddModelError("", "Ya existe un Departamento con ese Nombre, agregue otro.");
            }
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        int EmployeeID = Function.GetUser(out UserName);
                        tbDepartamento.dep_UsuarioCrea = EmployeeID;


                        list = db.UDP_Gral_tbDepartamento_Insert(tbDepartamento.dep_Codigo, tbDepartamento.dep_Nombre, EmployeeID, Function.DatetimeNow());
                        foreach (UDP_Gral_tbDepartamento_Insert_Result departamento in list)
                            MsjError = departamento.MensajeError;
                        if (MsjError.StartsWith("-1"))
                        {

                            ModelState.AddModelError("", "Ya existe un Departamento con ese Código, agregue otro.");
                            return View(tbDepartamento);
                        }
                        else
                        {
                            if (listMunicipios != null)
                            {
                                if (listMunicipios.Count > 0)
                                {
                                    foreach (tbMunicipio mun in listMunicipios)
                                    {
                                        lista = db.UDP_Gral_tbMunicipio_Insert(mun.mun_codigo, tbDepartamento.dep_Codigo, mun.mun_nombre, EmployeeID, Function.DatetimeNow());
                                        foreach (UDP_Gral_tbMunicipio_Insert_Result municipios in lista)
                                            MensajeError = municipios.MensajeError;
                                        if (MensajeError.StartsWith("-1"))
                                        {

                                            ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                            return View(tbDepartamento);
                                        }
                                    }
                                }
                            }
                            _Tran.Complete();
                        }
                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbDepartamento);
                    }
                }
                TempData["swalfunction"] = GeneralFunctions._isCreated;
                return RedirectToAction("Index");
            }
            return View(tbDepartamento);
        }

        // GET: Departamento/Edit/5
        public ActionResult Edit(string id)
        {
            Session["tbMunicipio"] = null;
            try
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }
            catch { }

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);

            return View(tbDepartamento);
        }

        // POST: Departamento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            
            IEnumerable<object> depart = null;
            IEnumerable<object> munici = null;
            string MsjError = "";
            string MensajeError = "";
            string UserName = "";
            var List = (List<tbMunicipio>)Session["tbMunicipio"];
            if (ModelState.IsValid)
            {
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                    {
                        int EmployeeID = Function.GetUser(out UserName);
                        tbDepartamento.dep_UsuarioCrea = EmployeeID;
                        depart = db.UDP_Gral_tbDepartamento_Update(tbDepartamento.dep_Codigo,
                                                                    tbDepartamento.dep_Nombre,
                                                                    EmployeeID
                            );
                        foreach (UDP_Gral_tbDepartamento_Update_Result Departamentos in depart)
                            MsjError = Departamentos.MensajeError;

                        if (MsjError.StartsWith("-1"))
                        {

                            ModelState.AddModelError("", "1. No se pudo actualizar el registro, favor contacte al administrador.");
                            return View(tbDepartamento);
                        }
                        else
                        {
                           

                            if (List != null && List.Count > 0)
                            {
                               
                                foreach (tbMunicipio municipio in List)
                                {
                                    munici = db.UDP_Gral_tbMunicipio_Insert(municipio.mun_codigo
                                                                                , tbDepartamento.dep_Codigo,
                                                                                municipio.mun_nombre,
                                                                                1,
                                                                                Function.DatetimeNow()
                                                                                );



                                    foreach (UDP_Gral_tbMunicipio_Insert_Result mun in munici)
                                        MensajeError = mun.MensajeError;
                                    if (MensajeError.StartsWith("-1"))
                                    {


                                        TempData["swalfunction"] = GeneralFunctions._YaExiste;
                                       
                                        return RedirectToAction("Edit/" + MsjError);
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
            return View(tbDepartamento);
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
        public JsonResult AnadirMunicipio(tbMunicipio Municipio)
        {
            List<tbMunicipio> sessionMunicipio = new List<tbMunicipio>();
            var list = (List<tbMunicipio>)Session["tbMunicipio"];
            if (list == null)
            {
                sessionMunicipio.Add(Municipio);
                Session["tbMunicipio"] = sessionMunicipio;
            }
            else
            {
                list.Add(Municipio);
                Session["tbMunicipio"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult RemoveMunicipios(tbMunicipio Municipios)
        {
            var list = (List<tbMunicipio>)Session["tbMunicipio"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.mun_codigo == Municipios.mun_codigo);
                list.Remove(itemToRemove);
                Session["tbMunicipio"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ActualizarMunicipio(tbMunicipio Municipio)
        {
            string MsjError = "";
            string depCodigo = Municipio.mun_codigo.Substring(0, 2);
            try
            {
                IEnumerable<object> list = null;
                tbMunicipio munl = db.tbMunicipio.Find(Municipio.mun_codigo);
                list = db.UDP_Gral_tbMunicipio_Update(Municipio.mun_codigo,
                    munl.dep_codigo,
                    Municipio.mun_nombre,
                    1
                    );
                foreach (UDP_Gral_tbMunicipio_Update_Result mun in list)
                    MsjError = (mun.MensajeError);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMun(tbMunicipio GuardarMunicipios)
        {
            {
                string MsjError = "";
                try
                {
                    IEnumerable<object> list = null;
                    list = db.UDP_Gral_tbMunicipio_Insert(GuardarMunicipios.mun_codigo, GuardarMunicipios.dep_codigo, GuardarMunicipios.mun_nombre, 1, Function.DatetimeNow());
                    foreach (UDP_Gral_tbMunicipio_Insert_Result mun in list)
                        MsjError = (mun.MensajeError);

                    if (MsjError.Substring(0, 1) == "-1")
                    {
                        ModelState.AddModelError("", "No se Guardo el Registro");
                    }
                    else
                    {

                        return Json("Index");
                    }
                }
                catch (Exception Ex)
                {
                    Ex.Message.ToString();
                    ModelState.AddModelError("", "No se Guardo el registro");
                }
                return Json("Index");
            }
        }

        [HttpPost]
        public JsonResult GuardarMunicipioModal([Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbMunicipio tbMunicipio)
        {
            string Msj = "";
            try
            {
                IEnumerable<object> list = null;
                list = db.UDP_Gral_tbMunicipio_Insert(tbMunicipio.mun_codigo, tbMunicipio.dep_codigo, tbMunicipio.mun_nombre, 1, Function.DatetimeNow());

                foreach (UDP_Gral_tbMunicipio_Insert_Result Municipio in list)
                    Msj = Municipio.MensajeError;

                if (Msj.Substring(0, 2) == "-1")
                {
                    ModelState.AddModelError("", "No se Actualizo el registro");


                }
                else
                {
                    return Json(Msj);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
            }
            return Json("Index");
        }

        [HttpPost]
        public JsonResult GuardarMuninicipio(string depCodigo)
        {
            string MsjError = "";
            var listMunicipios = (List<tbMunicipio>)Session["tbMunicipio"];
            IEnumerable<object> list = null;
            try
            {
                if (listMunicipios.Count > 0 && listMunicipios != null)
                {
                    foreach (tbMunicipio mun in listMunicipios)
                    {
                        list = db.UDP_Gral_tbMunicipio_Insert(mun.mun_codigo, depCodigo, mun.mun_nombre, 1, Function.DatetimeNow());
                        foreach (UDP_Gral_tbMunicipio_Insert_Result municipios in list)
                        {
                            MsjError = municipios.MensajeError;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MsjError = "-1";
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Guardo el registro");
            }
            return Json(MsjError, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EliminarMunicipio(string id, string dep_Codigo)
        {
            //Validar Inicio de Sesión
            GeneralFunctions Function = new GeneralFunctions();
            try
            {
                tbMunicipio obj = db.tbMunicipio.Find(id);
                IEnumerable<object> list = null;
                var MsjError = ""; list = db.UDP_Gral_tbMunicipio_Delete(id);
                foreach (UDP_Gral_tbMunicipio_Delete_Result mun in list)
                    MsjError = mun.MensajeError;
                if (MsjError.StartsWith("-1The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    TempData["smserror"] = "No se puede eliminar el registro porque posee dependencias, favor contacte al administrador.";
                    TempData["swalfunction"] = GeneralFunctions._isDependencia;
                    ModelState.AddModelError("", "No se puede borrar el registro");
                    return RedirectToAction("Edit/" + dep_Codigo);
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions._isDelete;
                    return RedirectToAction("Edit/" + dep_Codigo);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ModelState.AddModelError("", "No se Actualizo el registro");
                return RedirectToAction("Edit/" + dep_Codigo);
            }
        }

        [HttpPost]
        public JsonResult getMunicipio(string munCodigo)
        {
            IEnumerable<object> list = null;
            try
            {
                list = db.SDP_tbMunicipio_Select(munCodigo).ToList();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }

}
