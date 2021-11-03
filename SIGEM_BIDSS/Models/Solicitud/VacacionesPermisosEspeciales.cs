using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(VacacionesPermisosEspecialesMetadata))]
    public partial class tbVacacionesPermisosEspeciales
    {

    }

    public class VacacionesPermisosEspecialesMetadata
    {
        [Display(Name = "Codigo")]
        public int VPE_Id { get; set; }

        [Display(Name = "Correlativo")]
        public string VPE_Correlativo { get; set; }

        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Jefe Inmediato")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int VPE_JefeInmediato { get; set; }

        [Display(Name = "Tipo de Permiso")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int tperm_Id { get; set; }

        [Display(Name = "Estado")]
        public int est_Id { get; set; }

        [Display(Name = "Fecha de Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime VPE_GralFechaSolicitud { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime VPE_FechaInicio { get; set; }

        [Display(Name = "Fecha de Finalizacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime VPE_FechaFin { get; set; }

        [Display(Name = "Cantidad de Dias")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int VPE_CantidadDias { get; set; }

        [Display(Name = "Comentario")]
        public string VPE_Comentario { get; set; }

        [Display(Name = "Razon de Rechazo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string VPE_RazonRechazo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int VPE_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime VPE_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> VPE_UsuarioModifica { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<System.DateTime> VPE_FechaModifica { get; set; }

    }

}