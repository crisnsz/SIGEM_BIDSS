using SIGEM_BIDSS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    [SessionManager]
    public class VacacionesPermisosEspecialesController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: VacacionesPermisosEspeciales
        public ActionResult Index()
        {
            var tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbEstado).Include(t => t.tbTipoPermiso);
            return View(tbVacacionesPermisosEspeciales.ToList());
        }

        // GET: VacacionesPermisosEspeciales/Details/5
        public ActionResult Details(int? id)
        {
            string vReturn = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Find(id);
            if (tbVacacionesPermisosEspeciales.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbVacacionesPermisosEspeciales, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_ErrorUpdateState;
                }
            }
            if (tbVacacionesPermisosEspeciales == null)
            {
                return HttpNotFound();
            }
            return View(tbVacacionesPermisosEspeciales);
        }


        [HttpPost]
        public JsonResult CalcularFecha(cCalFechas cCalFechas)
        {

            string MASspan = "", MASspanFecha = "";
            if (cCalFechas.FechaInicio > cCalFechas.FechaFin)
            {
                MASspan = "La Fecha de inicio no puede ser mayor que la final";
            }
            if (cCalFechas.FechaFin < cCalFechas.FechaInicio)
            {
                MASspanFecha = "La Fecha de finalizacion no puede ser menor que la inicio";
            }
            object vCalcular = new { MASspan, MASspanFecha };
            return Json(vCalcular, JsonRequestBehavior.AllowGet);
        }





        // GET: VacacionesPermisosEspeciales/Create
        public ActionResult Create()
        {
            string UserName = "";
            int EmployeeID = Function.GetUser(out UserName);
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();


            ViewBag.VPE_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion");
            return View();
        }

        // POST: VacacionesPermisosEspeciales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VPE_JefeInmediato,tperm_Id,VPE_GralFechaSolicitud,VPE_FechaInicio,VPE_FechaFin,VPE_CantidadDias,VPE_MontoSolicitado,VPE_Comentario,VPE_RazonRechazo")] tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales)
        {
            string UserName = "", ErrorEmail = "", ErrorMessage = "";
            bool Result = false, ResultAdm = false;
            IEnumerable<object> Insert = null;

            try
            {
                if (tbVacacionesPermisosEspeciales.VPE_FechaFin > tbVacacionesPermisosEspeciales.VPE_FechaFin)
                {
                    ModelState.AddModelError("ValidationSummary", "La Fecha de inicio no puede ser mayor que la final.");
                }
                int EmployeeID = Function.GetUser(out UserName);
                cGetUserInfo GetEmployee = null;
                cGetUserInfo EmpJefe = null;

                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                                where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID
                                                select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();

                ViewBag.VPE_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres", tbVacacionesPermisosEspeciales.VPE_JefeInmediato);
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbVacacionesPermisosEspeciales.est_Id);
                ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion", tbVacacionesPermisosEspeciales.tperm_Id);

                tbVacacionesPermisosEspeciales.emp_Id = EmployeeID;
                tbVacacionesPermisosEspeciales.VPE_GralFechaSolicitud = Function.DatetimeNow();
                tbVacacionesPermisosEspeciales.est_Id = GeneralFunctions.Enviada;



                if (String.IsNullOrEmpty(tbVacacionesPermisosEspeciales.VPE_Comentario)) { tbVacacionesPermisosEspeciales.VPE_Comentario = GeneralFunctions.stringDefault; }

                if (ModelState.IsValid)
                {
                    Insert = db.UDP_Adm_tbVacacionesPermisosEspeciales_Insert(EmployeeID,
                                                                              tbVacacionesPermisosEspeciales.VPE_JefeInmediato,
                                                                              tbVacacionesPermisosEspeciales.tperm_Id,
                                                                              tbVacacionesPermisosEspeciales.est_Id,
                                                                              Function.DatetimeNow(),
                                                                              tbVacacionesPermisosEspeciales.VPE_FechaInicio,
                                                                              tbVacacionesPermisosEspeciales.VPE_FechaFin,
                                                                              tbVacacionesPermisosEspeciales.VPE_CantidadDias,
                                                                              tbVacacionesPermisosEspeciales.VPE_Comentario,
                                                                              tbVacacionesPermisosEspeciales.VPE_RazonRechazo,
                                                                              EmployeeID,
                                                                              Function.DatetimeNow());

                    foreach (UDP_Adm_tbVacacionesPermisosEspeciales_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;

                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("VacacionesPermisosEspeciales", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {

                        GetEmployee = Function.GetUserInfo(EmployeeID);
                        EmpJefe = Function.GetUserInfo(tbVacacionesPermisosEspeciales.VPE_JefeInmediato);


                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico);
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, EmpJefe.emp_Nombres, GetEmployee.emp_Nombres, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, EmpJefe.emp_CorreoElectronico);

                        if (!Result) Function.BitacoraErrores("VacacionesPermisosEspeciales", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("VacacionesPermisosEspeciales", "CreatePost", UserName, ErrorEmail);

                        TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Function.BitacoraErrores("VacacionesPermisosEspeciales", "CreatePost", UserName, ex.Message.ToString());
            }

            return View(tbVacacionesPermisosEspeciales);
        }






        public bool UpdateState(out string pvReturn, tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales, int State, string RazonRechazo)
        {
            pvReturn = "";
            string UserName = "", ErrorEmail = "", ErrorMessage = "", _msj = "", _msjFor = "";
            bool Result = false, ResultFor = false;
            IEnumerable<object> Update = null;
            var reject = "";

            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                tbVacacionesPermisosEspeciales.est_Id = State;

                tbVacacionesPermisosEspeciales.VPE_RazonRechazo = RazonRechazo;
                Update = db.UDP_Adm_tbVacacionesPermisosEspeciales_Update(tbVacacionesPermisosEspeciales.VPE_Id,
                                                                              tbVacacionesPermisosEspeciales.est_Id,
                                                                              tbVacacionesPermisosEspeciales.VPE_RazonRechazo,
                                                                              EmployeeID,
                                                                              Function.DatetimeNow());
                foreach (UDP_Adm_tbVacacionesPermisosEspeciales_Update_Result Res in Update)
                    ErrorMessage = Res.MensajeError;
                pvReturn = ErrorMessage;
                if (ErrorMessage.StartsWith("-1"))
                {

                    Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorMessage);
                    ModelState.AddModelError("", "No se pudo actualizar el registro contacte al administrador.");
                    return false;
                }
                else
                {
                    var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                    var GetEmployee = Function.GetUserInfo(tbVacacionesPermisosEspeciales.emp_Id);
                    var Approver = Function.GetUserInfo(EmployeeID);
                    string Correlativo = tbVacacionesPermisosEspeciales.VPE_Correlativo;
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

        // GET: AnticipoSalario/Approve/5
        [HttpPost]
        public JsonResult Approve(int? id)
        {
            var list = "";
            string vReturn = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
            }
            tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Find(id);
            if (tbVacacionesPermisosEspeciales.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbVacacionesPermisosEspeciales, GeneralFunctions.Aprobada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
                }
            }
            if (tbVacacionesPermisosEspeciales == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: AnticipoSalario/Approve/5
        [HttpPost]
        public JsonResult ApprovePorJefe(int? id)
        {
            var list = "";
            string vReturn = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
            }
            tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Find(id);
            if (tbVacacionesPermisosEspeciales.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbVacacionesPermisosEspeciales, GeneralFunctions.AprobadaPorJefe, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
                }
            }
            if (tbVacacionesPermisosEspeciales == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: AnticipoSalario/Approve/5
        [HttpPost]
        public JsonResult ApprovePorRRHH(int? id)
        {
            var list = "";
            string vReturn = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
            }
            tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Find(id);
            if (tbVacacionesPermisosEspeciales.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbVacacionesPermisosEspeciales, GeneralFunctions.AprobadaPorRRHH, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
                }
            }
            if (tbVacacionesPermisosEspeciales == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ApprovePorAdmin(int? id)
        {
            var list = "";
            string vReturn = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
            }
            tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Find(id);
            if (tbVacacionesPermisosEspeciales.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbVacacionesPermisosEspeciales, GeneralFunctions.AprobadaPorAdmin, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
                }
            }
            if (tbVacacionesPermisosEspeciales == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: AnticipoSalario/Approve/5   
        [HttpPost]
        public JsonResult Reject(int id, string RazonRechazo)
        {
            var list = "";
            string vReturn = "";
            tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Find(id);
            if (tbVacacionesPermisosEspeciales.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbVacacionesPermisosEspeciales, GeneralFunctions.Rechazada, RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Rechazada;
                }
            }
            if (tbVacacionesPermisosEspeciales == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
                //return HttpNotFound();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
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
