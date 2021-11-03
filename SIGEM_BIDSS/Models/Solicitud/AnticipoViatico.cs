using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(AnticipoViaticoMetadata))]
    public partial class tbAnticipoViatico
    {
    }


    public class AnticipoViaticoMetadata
    {
        [Display(Name ="ID")]
        public int Anvi_Id { get; set; }


        [Display(Name = "Código")]
        public string Anvi_Correlativo { get; set; }


        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }


        [Display(Name = "Jefe Inmediato")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int Anvi_JefeInmediato { get; set; }

        [Display(Name = "Fecha Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public System.DateTime Anvi_GralFechaSolicitud { get; set; }

        [Display(Name = "Fecha Viaje")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public System.DateTime Anvi_FechaViaje { get; set; }

        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string Anvi_Cliente { get; set; }

        [Display(Name = "Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string mun_Codigo { get; set; }

        [Display(Name = "Propósito Visita")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string Anvi_PropositoVisita { get; set; }

        [Display(Name = "Días Visita")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int Anvi_DiasVisita { get; set; }

        [Display(Name = "¿Necesita Hospedaje?")]
        public bool Anvi_Hospedaje { get; set; }

        [Display(Name = "Tipo Transporte")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int Anvi_tptran_Id { get; set; }


        [Display(Name = "Comentario")]
        [MaxLength(250, ErrorMessage = "Máximo {1} caracteres")]
        public string Anvi_Comentario { get; set; }






        [Display(Name = "Estado")]
        public int est_Id { get; set; }

        [Display(Name = "Razon de Rechazo")]
        public string Anvi_RazonRechazo { get; set; }

        //[Display(Name = "Usuario Crea")]
        //public int Anvi_UsuarioCrea { get; set; }

        //[Display(Name = "Fecha Crea")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        //public System.DateTime Anvi_FechaCrea { get; set; }

        //[Display(Name = "Usuario Modifica")]
        //public Nullable<int> Anvi_UsuarioModifica { get; set; }

        //[Display(Name = "Fecha Modifica")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> Anvi_FechaModifica { get; set; }
    }
}