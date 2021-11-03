using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace SIGEM_BIDSS.Models
{
    public class GeneralFunctions
    {

        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        public List<tbEmpleado> Sexo()
        {
            List<tbEmpleado> Hola = new List<tbEmpleado>();
            //Hola.Add(new tbEmpleado { ge_Id = "", ge_Description = "Seleccione Uno" });
            Hola.Add(new tbEmpleado { ge_Id = "F", ge_Description = "Femenino" });
            Hola.Add(new tbEmpleado { ge_Id = "M", ge_Description = "Masculino" });
            return Hola;
        }

        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }
        public int GetUser()
        {
            int _user = 1;
            return _user;
        }

        //Catalogos
        public const string _isCreated = "Created";
        public const string _isEdited = "Edited";
        public const string _isDelete = "Delete";
        public const string _isInactiva = "Inactiva";
        public const string _isActiva = "Activa";

        public const string _isDependencia = "Dependencias";
        public const string _isDependenciaCate = "DependenciaCate";
        public const string _isDependenciasSubCate = "DependenciaSubCate";
        public const string _isDependenciaCateIn = "DependenciaCateIn";
        public const string _isDependenciasSubCateIn = "DependenciaSubCateIn";

        public const string _YaExiste = "ExisteCo";
        public const string _Nombre = "ExisteNom";


        //Solicitudes
        public const string sol_ErrorUpdateState = "Error001";

        public const string msj_ToAdmin = "a ingresado una solicitud";


        public const string sol_Enviada = "Enviada";
        public const string msj_Enviada = "Su solicituda a sido ingresada con exito";

        public const string sol_Revisada = "Revisada";
        public const string msj_Revisada = "Su solicituda a sido revisada";

        public const string sol_Aprobada = "Aceptada";
        public const string msj_Aprobada = "Su solicituda a sido aprobada";
        public const string msj_RevisadaPorJefe = "Su solicituda a sido aprobada por su Jefe Inmediato";
        public const string msj_RevisadaPorRRHH = "Su solicituda a sido aprobada por Recursos Humanos";
        public const string msj_RevisadaPorAdmin = "Su solicituda a sido aprobada por la Administracion";



        public const string sol_Rechazada = "Rechazada";
        public const string msj_Rechazada = "Su solicituda a sido rechazada";

        public const string MonthLimit = "MonthLimit";
        public const string YearLimit = "YearLimit";


        //Estados Solicitud
        public const int Enviada = 1;
        public const int Revisada = 2;
        public const int Aprobada = 3;
        public const int Rechazada = 4;
        public const int AprobadaPorJefe = 5;
        public const int AprobadaPorRRHH = 6;
        public const int AprobadaPorAdmin = 7;



        public const int intDefault = 1;
        public const int tiposalario = 1;
        public const int tiposoli = 2;

        //Defaults
        public const string stringEmpty = "";
        public const string stringDefault = "-----";
        public const string dateDefault = "01/01/1900";
        public const string date2 = "1900/01/01";
        public const bool boolDefault = false;


        //public const string AccionPersonal = "Accion Personal";
        public const int AccionPersonal = 1;
        public const int SolicitudPermisoLaboral = 2;
        public const int SolicitudAnticipoViaticos = 3;
        public const int SolicitudAnticipoSalario = 4;
        public const int LiquidacionAnticipoSalario = 5;
        public const int RequisicionCompra = 6;
        public const int RembolsoGastos = 7;
        public const int empleadoinactivo = 6;
        public const int empleadoactivo = 5;
        public const bool instfinDenegar = false;
        public const bool instfinAceptar = true;

        public const bool Activo = true;
        public const bool Inactivo = false;



        //Estado Categoria
        public const bool CategoriaActivo = true;

        public const bool CategoriaInactivo = false;

        //Estado Subcategoria
        public const bool SubcategoriaActivo = true;

        public const bool SubcategoriaInactivo = false;



        //Estado Producto

        public const bool ProductoActivo = true;
        public const bool ProductoInactivo = false;


        //-----------------------------
        public cGetUserInfo GetUserInfo(int EmployeeID)
        {
            object GetEmployee = null;
            cGetUserInfo cGetUserInfo = new cGetUserInfo();
            tbEmpleado tbEmpleado = new tbEmpleado();
            try
            {
                tbEmpleado = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).FirstOrDefault();
                cGetUserInfo.strFor = " Por: "; cGetUserInfo.emp_Nombres = tbEmpleado.emp_Nombres + " " + tbEmpleado.emp_Apellidos; cGetUserInfo.emp_CorreoElectronico = tbEmpleado.emp_CorreoElectronico;
            }
            catch (Exception Ex)
            {
                cGetUserInfo.strFor = Ex.Message.ToString() + " " + Ex.InnerException.Message.ToString();
            }
            return cGetUserInfo;
        }





        public int GetUser(out string UserName)
        {
            string email = "";
            int _user = 0;

            UserName = ClaimsPrincipal.Current.FindFirst("name").Value;
            email = ClaimsPrincipal.Current.FindFirst("preferred_username").Value;

            var Usuario = db.tbEmpleado.Select(s => new
            {
                emp_Id = s.emp_Id,
                emp_CorreoElectronico = s.emp_CorreoElectronico
            }).Where(x => x.emp_CorreoElectronico == email).ToList();

            foreach (var Result in Usuario)
                _user = Result.emp_Id;

            return _user;
        }

        //Bitácora de Errores
        public void BitacoraErrores(string Controller, string Action, string User, string ErrorMessage)
        {
            db.UDP_Acce_tbBitacoraErrores_Insert(Controller, Action, User, DatetimeNow(), ErrorMessage);
        }

        public bool LeerDatos(out string pvMensajeError, string Reference, string WelcomeName, string Empleado, string _msj, string _RazonRechazo, string Approver, string CorreoDestinatario)
        {
            pvMensajeError = "";
            string UserName = "",
                    lsSubject = "",
                    lsRutaPlantilla = "",
                    lsXMLDatos = "",
                    lsXMLEnvio = "";

            bool State = false;
            int StateIn = 0;

            GetUser(out UserName);

            lsSubject = "REF:(" + Reference + ")";

            lsRutaPlantilla = System.AppContext.BaseDirectory + "Content\\Email\\Solicitud.xml";

            lsXMLDatos = @"<principal>
                        <welcome>" + WelcomeName + "</welcome>" +
                          @"<nref>REF:(" + Reference + ")</nref>" +
                          @"<EmployeeName>" + Empleado + "</EmployeeName>" +
                          @"<msj>" + _msj + "</msj>" +
                          @"<RazonRechazo>" + _RazonRechazo + "</RazonRechazo>" +
                           @"<approver>" + Approver + "</approver>" +
                         "</principal>";

            State = EmailGenerar_Body(lsRutaPlantilla, lsXMLDatos, out lsXMLEnvio, out pvMensajeError);
            if (State)
            {
                //Si todo está bien procedo a enviar correo
                var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                StateIn = enviarCorreo(out pvMensajeError, _Parameters.par_CorreoEmisor, _Parameters.par_Password, lsXMLEnvio, lsSubject, CorreoDestinatario, _Parameters.par_Servidor, _Parameters.par_Puerto.ToString());
                if (StateIn != 1)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        private bool EmailGenerar_Body(string psRutaPlantilla, string psXML, out string psXMLEnvio, out string ErrorMessage)
        {
            psXMLEnvio = "";
            ErrorMessage = "";
            try
            {
                //Leer
                XmlTextReader reader = new XmlTextReader(psRutaPlantilla);
                reader.Read();

                //Cargar
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(reader);

                XmlReader xmlData = XmlReader.Create(new StringReader(psXML));

                XmlWriterSettings Configuraciones = new XmlWriterSettings();
                Configuraciones.OmitXmlDeclaration = true;
                Configuraciones.ConformanceLevel = ConformanceLevel.Fragment;
                //Configuraciones.Encoding = Encoding.UTF8;
                Configuraciones.CloseOutput = false;

                //Empieza a hacer el match
                using (StringWriter sw = new StringWriter())
                using (XmlWriter xwo = XmlWriter.Create(sw, Configuraciones)) // xslt.OutputSettings use OutputSettings of xsl, so it can be output as HTML
                {
                    xslt.Transform(xmlData, null, xwo);
                    psXMLEnvio = sw.ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
                psXMLEnvio = "";
                return false;
            }
        }

        static bool mailSent = false;
        static string userState = "message1";

        public int enviarCorreo(out string ErrorMessage, string psEmisor, string psPassword, string psMensaje, string psAsunto, string psDestinatario, string psServidor, string psPuerto)
        {
            //      0 = Ok      //
            //      1 = Error   //
            ErrorMessage = "";
            int tryAgain = 10;
            bool failed = false;
            do
            {
                try
                {
                    failed = false;

                    // Command-line argument must be the SMTP host.
                    SmtpClient envios = new SmtpClient();
                    // Specify the email sender.
                    // Create a mailing address that includes a UTF8 character
                    // in the display name.
                    MailAddress from = new MailAddress("","",System.Text.Encoding.UTF8);
                    // Set destinations for the email message.
                    MailAddress to = new MailAddress(psDestinatario.Trim());
                    // Specify the message content.
                    MailMessage message = new MailMessage(from, to);
                    message.To.Clear();
                    message.Body = "";
                    // Include some non-ASCII characters in body and subject.
                    message.Subject = "";
                    message.Body = psMensaje;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.Subject = psAsunto;
                    message.IsBodyHtml = true;
                    message.To.Add(psDestinatario.Trim());
                    message.From = new MailAddress(psEmisor);
                    // Datos del servidor //
                    envios.Credentials = new NetworkCredential(psEmisor, psPassword);
                    envios.Host = psServidor;
                    envios.Port = Convert.ToInt32(psPuerto);
                    envios.EnableSsl = true;
                    //Función de envío de correo //
                    // Set the method that is called back when the send operation ends.
                    envios.SendCompleted += new
                    SendCompletedEventHandler(SendCompletedCallback);
                    // The userState can be any object that allows your callback 
                    // method to identify this send operation.
                    // For this example, the userToken is a string constant.
                    envios.SendAsync(message, userState);
                    envios.SendCompleted += new
                    SendCompletedEventHandler(SendCompletedCallback);
                    // If the user canceled the send, and mail hasn't been sent yet,
                    // then cancel the pending operation.
                    //if (answer.StartsWith("c") && mailSent == false)
                    //{
                    //    envios.SendAsyncCancel();
                    //}
                    // Clean up.
                    message.Dispose();
                }
                catch (Exception ex)
                {
                    failed = true;
                    tryAgain--;
                    var exception = ex.Message.ToString();
                    //Other code for saving exception message to a log.
                }
            } while (failed && tryAgain != 0);
            return 0;
        }


        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;
            if (e.Cancelled)
            {
                userState = "Send canceled. " + token;
            }
            if (e.Error != null)
            {
                userState = "Error: "+ token+ " " + e.Error.ToString();
            }
            else
            {
                userState = "Message sent.";
            }
            mailSent = true;
        }
    }




    public class cGetUserInfo
    {
        public string strFor { get; set; }
        public string emp_Nombres { get; set; }
        public string emp_CorreoElectronico { get; set; }
    }

    public class cCalDecimales
    {
        public decimal empMonto { get; set; }

        public decimal empSueldo { get; set; }

        public decimal empPorcetanje { get; set; }

        //Asignamos el nombre a la propiedad privada

    }

    public class cCalFechas
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaInicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaFin { get; set; }

    }
}