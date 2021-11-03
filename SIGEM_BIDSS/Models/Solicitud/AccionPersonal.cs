using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(AccionPersonalMetadata))]
    public partial class tbAccionPersonal
    {
        [NotMapped]
        public string nRazonRechazo { get; set; }

    }


    public class AccionPersonalMetadata
    {
        [Display(Name = "ID")]
        public int Acp_Id { get; set; }

        [Display(Name = "Correlativo")]
        public string Acp_Correlativo { get; set; }

        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Jefe Inmediato")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int Acp_JefeInmediato { get; set; }

        [Display(Name = "Fecha Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Acp_FechaSolicitud { get; set; }

        [Display(Name = "Tipo Movimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int tipmo_id { get; set; }

        [Display(Name = "Comentario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string Acp_Comentario { get; set; }

        [Display(Name = "Estado")]
        public int est_Id { get; set; }
        [Display(Name = "Razón de Rechazo")]
        public string Acp_RazonRechazo { get; set; }

        [Display(Name = "Usuario Crea")]
        public Nullable<int> Acp_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime Acp_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> Acp_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Acp_FechaModifica { get; set; }
    }

}