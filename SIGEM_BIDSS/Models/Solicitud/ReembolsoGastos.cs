using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(SolicitudReembolsoGastos))]
    public partial class tbSolicitudReembolsoGastos
    {

        [NotMapped]

        public string dep_codigo { get; set; }
    }

    public class SolicitudReembolsoGastos
    {

        [Display(Name = "ID")]   
        public int Reemga_Id { get; set; }

        [Display(Name = "Correlativo")]
        public string Reemga_Correlativo { get; set; }

        [Display(Name = "Empleado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int emp_Id { get; set; }


        [Display(Name = "Fecha Solicitud")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime Reemga_GralFechaSolicitud { get; set; }

        [Display(Name = "Fecha Viaje")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime Reemga_FechaViaje { get; set; }

        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string Reemga_Cliente { get; set; }

        [Display(Name = "Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string mun_codigo { get; set; }

        [Display(Name = "Proposito Visita")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string Reemga_PropositoVisita { get; set; }

        [Display(Name = "Dias De Visita")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int Reemga_DiasVisita { get; set; }

        [Display(Name = "Comentario")]     
        public string Reemga_Comentario { get; set; }

        [Display(Name = "Estado")]      
        public int est_Id { get; set; }

        [Display(Name = "Razón Rechazo")]
        public string Reemga_RazonRechazo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int Reemga_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime Reemga_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public int Reemga_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public System.DateTime Reemga_FechaModifica { get; set; }

    }

  
   
}