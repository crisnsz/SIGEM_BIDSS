

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
    public class AnticipoSalarioController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        GeneralFunctions Function = new GeneralFunctions();

        // GET: AnticipoSalario
        public ActionResult Index()
        {
            try
            {
                var tbAnticipoSalario = db.tbAnticipoSalario.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbEstado).Include(t => t.tbTipoSalario);
                return View(tbAnticipoSalario.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: AnticipoSalario/Details/5
        public ActionResult Details(int? id)
        {
            string vReturn = "";
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_ErrorUpdateState;
                }
            }
            if (tbAnticipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbAnticipoSalario);
        }


        // GET: AnticipoSalario/Revisar
        [HttpPost]
        public JsonResult Revisar(int id, string RazonRechazo)
        {
            string vReturn = "";
            string IsFor = "false";
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Revisada, RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                    IsFor = "true";
                }
            }
            if (tbAnticipoSalario == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(IsFor, JsonRequestBehavior.AllowGet);
        }

        // GET: AnticipoSalario/Revisar
        [HttpPost]
        public JsonResult Calcular(cCalDecimales cCalDecimal)
        {

            string spanCantidad = "", MASspanCantidad = "";
            if (cCalDecimal.empMonto > cCalDecimal.empSueldo)
            {
                MASspanCantidad = "Monto solicitado mayor que el Sueldo";
            }

            if (cCalDecimal.empMonto > cCalDecimal.empPorcetanje)
            {
                spanCantidad = "El monto no puede ser mayor que el pocentaje permitido";
            }

            object vCalcular = new { spanCantidad, MASspanCantidad };
            return Json(vCalcular, JsonRequestBehavior.AllowGet);
        }




        // GET: AnticipoSalario/Create
        public ActionResult Create()
        {
            tbAnticipoSalario tbAnticipoSalario = new tbAnticipoSalario();
            string ErrorMessage = "", UserName = ""; ;
            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                int Year = Function.DatetimeNow().Year;
                int Month = Function.DatetimeNow().Month;

                int SolCount = (from _tbSol in db.tbAnticipoSalario where _tbSol.Ansal_FechaCrea.Year == Year && _tbSol.emp_Id == EmployeeID select _tbSol).Count();
                int SolCountMonth = (from _tbSol in db.tbAnticipoSalario where _tbSol.emp_Id == EmployeeID &&  _tbSol.Ansal_FechaCrea.Month == Month && _tbSol.Ansal_FechaCrea.Year == Year  select _tbSol).Count();

                var _Parameters = db.tbParametro.FirstOrDefault();
                if (_Parameters == null)
                {
                    return RedirectToAction("Create", "Parametro");
                }
                if (SolCount >= _Parameters.par_FrecuenciaAdelantoSalario)
                {
                    TempData["swalfunction"] = GeneralFunctions.YearLimit;
                    return RedirectToAction("Solicitud", "Menu");
                }
                if (SolCountMonth >= 7)
                {
                    TempData["swalfunction"] = GeneralFunctions.MonthLimit;
                    return RedirectToAction("Solicitud", "Menu");
                }

                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                                where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID
                                                orderby _tbEmp.emp_Nombres
                                                select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();

                var vSueldo = (from _tbSueldo in db.tbSueldo where _tbSueldo.emp_Id == EmployeeID select _tbSueldo.sue_Cantidad).FirstOrDefault();
                var _percent = vSueldo * (Convert.ToDecimal(_Parameters.par_PorcentajeAdelantoSalario) / 100);

                tbAnticipoSalario.Sueldo = Convert.ToDecimal(vSueldo);
                tbAnticipoSalario.Porcentaje = decimal.Round(Convert.ToDecimal(_percent), 2);
                ViewBag.Ansal_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
                ViewBag.tpsal_id = new SelectList(db.tbTipoSalario.OrderBy(x => x.tpsal_Descripcion), "tpsal_id", "tpsal_Descripcion");
            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message.ToString();
                throw;
            }
            return View(tbAnticipoSalario);
        }

        // POST: AnticipoSalario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ansal_Id,Ansal_Correlativo,emp_Id,Ansal_JefeInmediato,Ansal_GralFechaSolicitud,Ansal_MontoSolicitado,tpsal_id,Ansal_Justificacion,Ansal_Comentario,est_Id,Ansal_RazonRechazo,Cantidad,Sueldo,Porcentaje")] tbAnticipoSalario tbAnticipoSalario)
        {
            string UserName = "", ErrorEmail = "", ErrorMessage = "";
            bool Result = false, ResultAdm = false;
            IEnumerable<object> Insert = null;
          
            try
            {
                cGetUserInfo GetEmployee = null;
                int EmployeeID = Function.GetUser(out UserName);

                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                                where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID
                                                orderby _tbEmp.emp_Nombres
                                                select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();
                ViewBag.Ansal_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres", tbAnticipoSalario.Ansal_JefeInmediato);
                ViewBag.tpsal_id = new SelectList(db.tbTipoSalario.OrderBy(x => x.tpsal_Descripcion), "tpsal_id", "tpsal_Descripcion", tbAnticipoSalario.tpsal_id);

                tbAnticipoSalario.emp_Id = EmployeeID;
                tbAnticipoSalario.Ansal_GralFechaSolicitud = Function.DatetimeNow();
                tbAnticipoSalario.est_Id = GeneralFunctions.Enviada;

                var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                var vSueldo = (from _tbSueldo in db.tbSueldo where _tbSueldo.emp_Id == EmployeeID select _tbSueldo.sue_Cantidad).FirstOrDefault();
                var _percent = vSueldo * (Convert.ToDecimal(_Parameters.par_PorcentajeAdelantoSalario) / 100);

                if (String.IsNullOrEmpty(tbAnticipoSalario.Ansal_Comentario)) { tbAnticipoSalario.Ansal_Comentario = GeneralFunctions.stringDefault; }
                tbAnticipoSalario.Ansal_MontoSolicitado = Convert.ToDecimal(tbAnticipoSalario.Cantidad.Replace(",", ""));

                if (tbAnticipoSalario.Ansal_MontoSolicitado > vSueldo)
                    ModelState.AddModelError("Cantidad", "El monto no puede ser mayor que el sueldo.");

                if (tbAnticipoSalario.Ansal_MontoSolicitado > _percent)
                    ModelState.AddModelError("Cantidad", "El monto no puede ser mayor que el pocentaje permitido.");


                if (ModelState.IsValid)
                {

                    Insert = db.UDP_Adm_tbAnticipoSalario_Insert(EmployeeID,
                                                                tbAnticipoSalario.Ansal_JefeInmediato,
                                                                Function.DatetimeNow(),
                                                                tbAnticipoSalario.Ansal_MontoSolicitado,
                                                                tbAnticipoSalario.tpsal_id,
                                                                tbAnticipoSalario.Ansal_Justificacion,
                                                                tbAnticipoSalario.Ansal_Comentario,
                                                                tbAnticipoSalario.est_Id,
                                                                tbAnticipoSalario.Ansal_RazonRechazo,
                                                                EmployeeID,
                                                                Function.DatetimeNow());
                    foreach (UDP_Adm_tbAnticipoSalario_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {
                        GetEmployee = Function.GetUserInfo(EmployeeID);

                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico);
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, _Parameters.par_CorreoRRHH);

                        if (!Result) Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);

                        TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                Function.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ex.Message.ToString());
            }
            return View(tbAnticipoSalario);

        }




        public bool UpdateState(out string pvReturn, tbAnticipoSalario tbAnticipoSalario, int State, string RazonRechazo)
        {
            pvReturn = "";
            string UserName = "",
                ErrorEmail = "",
                ErrorMessage = "", 
                _msj = "", 
                reject = "";
            bool Result = false;
            IEnumerable<object> Update = null;
            cGetUserInfo GetEmployee = null, Approver = null;
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                tbAnticipoSalario.est_Id = State;
                tbAnticipoSalario.Ansal_RazonRechazo = RazonRechazo;
                Update = db.UDP_Adm_tbAnticipoSalario_Update(tbAnticipoSalario.Ansal_Id,
                                                                tbAnticipoSalario.est_Id,
                                                                tbAnticipoSalario.Ansal_RazonRechazo,
                                                                EmployeeID,
                                                                Function.DatetimeNow());
                foreach (UDP_Adm_tbAnticipoSalario_Update_Result Res in Update)
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
                    switch (State)
                    {
                        case GeneralFunctions.Revisada:
                            _msj = GeneralFunctions.msj_Revisada;
                            break;
                        case GeneralFunctions.Aprobada:
                            _msj = GeneralFunctions.msj_Aprobada;
                            break;
                        case GeneralFunctions.Rechazada:
                            _msj = GeneralFunctions.msj_Rechazada;
                            reject = " Razon de Rechazo:";
                            break;
                    }
                    if (RazonRechazo == GeneralFunctions.stringDefault) { RazonRechazo = null; };

                    GetEmployee = Function.GetUserInfo(tbAnticipoSalario.emp_Id);
                    Approver = Function.GetUserInfo(EmployeeID);
                    if (RazonRechazo == GeneralFunctions.stringDefault) { RazonRechazo = null; };

                    Result = Function.LeerDatos(out ErrorEmail, tbAnticipoSalario.Ansal_Correlativo, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, _msj, reject + " " + RazonRechazo, Approver.strFor + Approver.emp_Nombres, GetEmployee.emp_CorreoElectronico);

                    if (!Result)
                    {
                        Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail);
                        return false;
                    }
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
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Aprobada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
                }
            }
            if (tbAnticipoSalario == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Reject(int id, string RazonRechazo)
        {
            var list = "";
            string vReturn = "";
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Rechazada, RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Rechazada;
                }
            }
            if (tbAnticipoSalario == null)
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
