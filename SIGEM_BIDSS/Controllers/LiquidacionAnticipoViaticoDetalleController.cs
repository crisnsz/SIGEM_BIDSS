using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class LiquidacionAnticipoViaticoDetalleController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();


        // GET: LiquidacionAnticipoViaticoDetalle
        public ActionResult Index()
        {
            var tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Include(t => t.tbAnticipoViatico).Include(t => t.tbEstado);
            return View(tbLiquidacionAnticipoViatico.ToList());
        }

        //GET: LiquidacionAnticipoViaticoDetalle/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                //string vReturn = "";
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
                ViewBag.RequisicionDetalle = db.tbRequisionCompraDetalle.Where(x => x.Reqco_Id == tbLiquidacionAnticipoViatico.Lianvi_Id).ToList();
                if (tbLiquidacionAnticipoViatico.est_Id == GeneralFunctions.Enviada)
                {
                    //if (UpdateState(out vReturn, tbLiquidacionAnticipoViatico, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
                    //{
                    //    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                    //}
                    //else
                    //{
                    //    TempData["swalfunction"] = GeneralFunctions.sol_ErrorUpdateState;
                    //}
                }
                if (tbLiquidacionAnticipoViatico == null)
                {
                    return HttpNotFound();
                }
                return View(tbLiquidacionAnticipoViatico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GET: LiquidacionAnticipoViaticoDetalle/Create
        public ActionResult Create( )
        {
            

         int Id =  Convert.ToInt32(Session["NombreLiquidacione"]);
   
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = new tbLiquidacionAnticipoViaticoDetalle();
            tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id = Id;
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            tbLiquidacionAnticipoViaticoDetalle.Lianvide_FechaGasto = Function.DatetimeNow();
         

            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
         

        }


        // POST: LiquidacionAnticipoViaticoDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public JsonResult SaveLiquidacionAnticipoDetalle(tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle, HttpPostedFileBase Archivo)
        {
            int datos = 0;

            decimal Lianvide_MontoGasto = tbLiquidacionAnticipoViaticoDetalle.Lianvide_MontoGasto;
            List<tbLiquidacionAnticipoViaticoDetalle> sessionSolicitudReembolsoDetalle = new List<tbLiquidacionAnticipoViaticoDetalle>();
            var list = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["NombreLiquidaciondetalle"];

            if (list == null)
            {
                sessionSolicitudReembolsoDetalle.Add(tbLiquidacionAnticipoViaticoDetalle);
                Session["NombreLiquidaciondetalle"] = sessionSolicitudReembolsoDetalle;
            }
            else
            {

                list.Add(tbLiquidacionAnticipoViaticoDetalle);
                Session["NombreLiquidaciondetalle"] = list;
                return Json(tbLiquidacionAnticipoViaticoDetalle, JsonRequestBehavior.AllowGet);
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lianvide_Id,Lianvi_Id,Lianvide_FechaGasto,tpv_Id,Lianvide_MontoGasto,Lianvide_Concepto,Lianvide_UsuarioCrea,Lianvide_FechaCrea,Lianvide_UsuarioModifica,Lianvide_FechaModifica")] tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle, HttpPostedFileBase ArchivoPath)
        {


            string UserName = "";
            string MsjError = "";
            string ErrorEmail = "";
            var listaLiquidacion = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["NombreLiquidaciondetalle"];
            try
            {
                cGetUserInfo GetEmployee = null;
                cGetUserInfo EmpJefe = null;
                bool Result = false, ResultAdm = false;
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                IEnumerable<object> lista = null;
                int EmployeeID = Function.GetUser(out UserName);
                if (listaLiquidacion != null)
                {
                    if (listaLiquidacion.Count > 0)
                    {
                        foreach (tbLiquidacionAnticipoViaticoDetalle Sol in listaLiquidacion)
                        {
                            lista = db.UDP_Adm_tbLiquidacionAnticipoViaticoDetalle_Insert(tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id, Sol.Lianvide_FechaGasto, Sol.tpv_Id, Sol.Lianvide_MontoGasto, Sol.Lianvide_Concepto, Sol.Lianvide_Archivo, EmployeeID, Function.DatetimeNow());
                            foreach (UDP_Adm_tbLiquidacionAnticipoViaticoDetalle_Insert_Result SolicitudDetalle in lista)
                                MsjError = SolicitudDetalle.MensajeError;
                            if (MsjError.StartsWith("-1"))
                            {

                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                return View(tbLiquidacionAnticipoViaticoDetalle);
                            }
                        }
                    }
                }

                GetEmployee = Function.GetUserInfo(EmployeeID);
                EmpJefe = Function.GetUserInfo(EmployeeID);

                Result = Function.LeerDatos(out ErrorEmail, MsjError, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico);
                ResultAdm = Function.LeerDatos(out ErrorEmail, MsjError, EmpJefe.emp_Nombres, GetEmployee.emp_Nombres, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, EmpJefe.emp_CorreoElectronico);


                if (!Result) Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ErrorEmail);
                if (!ResultAdm) Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ErrorEmail);


                return RedirectToAction("Index", "LiquidacionAnticipoViatico");
            }
            catch (Exception)
            {
                TempData["swalfunction"] = GeneralFunctions._isCreated;
                ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Id");
                ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo");
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbLiquidacionAnticipoViaticoDetalle);
            }

        }


        // GET: LiquidacionAnticipoViaticoDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo", tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbLiquidacionAnticipoViaticoDetalle.tpv_Id);
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // POST: LiquidacionAnticipoViaticoDetalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lianvide_Id,Lianvi_Id,Lianvide_FechaGasto,tpv_Id,Lianvide_MontoGasto,Lianvide_Concepto,Lianvide_Archivo,Lianvide_UsuarioCrea,Lianvide_FechaCrea,Lianvide_UsuarioModifica,Lianvide_FechaModifica")] tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbLiquidacionAnticipoViaticoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo", tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbLiquidacionAnticipoViaticoDetalle.tpv_Id);
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // GET: LiquidacionAnticipoViaticoDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // POST: LiquidacionAnticipoViaticoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            db.tbLiquidacionAnticipoViaticoDetalle.Remove(tbLiquidacionAnticipoViaticoDetalle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public bool UpdateState(out string pvReturn, tbRequisionCompra tbRequisionCompra, int State, string RazonRechazo)
        {
            pvReturn = "";
            string UserName = "", ErrorEmail = "", ErrorMessage = "", _msj = "", _msjFor = "";
            bool Result = false, ResultFor = false;
            IEnumerable<object> Update = null;
            var reject = "";

            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                tbRequisionCompra.est_Id = State;

                tbRequisionCompra.Reqco_RazonRechazo = RazonRechazo;

                Update = db.UDP_Adm_tbRequisionCompra_Update(tbRequisionCompra.Reqco_Id,
                                                                              tbRequisionCompra.est_Id,
                                                                              tbRequisionCompra.Reqco_RazonRechazo,
                                                                              EmployeeID,
                                                                              Function.DatetimeNow());
                foreach (UDP_Adm_tbRequisionCompra_Update_Result Res in Update)
                    ErrorMessage = Res.MensajeError;
                pvReturn = ErrorMessage;
                if (ErrorMessage.StartsWith("-1"))
                {

                    Function.BitacoraErrores("RequisionCompra", "UpdateState", UserName, ErrorMessage);
                    ModelState.AddModelError("", "No se pudo actualizar el registro contacte al administrador.");
                    return false;
                }
                else
                {
                    var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                    var GetEmployee = Function.GetUserInfo(tbRequisionCompra.emp_Id);
                    var Approver = Function.GetUserInfo(EmployeeID);
                    string Correlativo = tbRequisionCompra.Reqco_Correlativo;
                    string Nombres = GetEmployee.emp_Nombres;
                    string CorreoElectronico = GetEmployee.emp_CorreoElectronico;
                    string sApprover = Approver.strFor + Approver.emp_Nombres;
                    if (RazonRechazo == GeneralFunctions.stringDefault) { RazonRechazo = null; };
                    _msjFor = GeneralFunctions.msj_ToAdmin;
                    switch (State)
                    {
                        case GeneralFunctions.Revisada:
                            _msj = GeneralFunctions.msj_Revisada;
                            break;
                        case GeneralFunctions.AprobadaPorJefe:
                            _msj = GeneralFunctions.msj_RevisadaPorJefe;
                            ResultFor = Function.LeerDatos(out ErrorEmail, Correlativo, _Parameters.par_NombreEmpresa, Nombres, _msjFor, GeneralFunctions.stringEmpty, sApprover, _Parameters.par_CorreoEmpresa);
                            break;
                        case GeneralFunctions.AprobadaPorRRHH:
                            _msj = GeneralFunctions.msj_RevisadaPorRRHH;
                            ResultFor = Function.LeerDatos(out ErrorEmail, Correlativo, _Parameters.par_NombreEmpresa, Nombres, _msjFor, GeneralFunctions.stringEmpty, sApprover, _Parameters.par_CorreoEmpresa);
                            break;
                        case GeneralFunctions.AprobadaPorAdmin:
                            _msj = GeneralFunctions.msj_RevisadaPorAdmin;
                            ResultFor = Function.LeerDatos(out ErrorEmail, Correlativo, _Parameters.par_NombreEmpresa, Nombres, _msjFor, GeneralFunctions.stringEmpty, sApprover, _Parameters.par_CorreoEmpresa);
                            break;
                        case GeneralFunctions.Aprobada:
                            _msj = GeneralFunctions.msj_Aprobada;
                            break;
                        case GeneralFunctions.Rechazada:
                            _msj = GeneralFunctions.msj_Rechazada;
                            reject = " Razon de Rechazo:";
                            break;
                    }
                    Result = Function.LeerDatos(out ErrorEmail, Correlativo, Nombres, GeneralFunctions.stringEmpty, _msj, reject + " " + RazonRechazo, sApprover, CorreoElectronico);


                    if (!Result) { Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail); return false; }
                    if (!ResultFor) { Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail); return false; }

                }
                return true;

            }
            catch (Exception ex)
            {
                pvReturn = ex.Message.ToString();
                Function.BitacoraErrores("AnticipoViatico", "UpdateState", UserName, ex.Message.ToString());
                return false;
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

        [HttpPost]
        public JsonResult RemoveDetalle(tbLiquidacionAnticipoViaticoDetalle LiquidacionDetalle)
        {
            var list = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["LiquidacionAnticipoViaticoDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.Lianvide_Id == LiquidacionDetalle.Lianvide_Id);
                list.Remove(itemToRemove);
     
                Session["LiquidacionAnticipoViaticoDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }


}
