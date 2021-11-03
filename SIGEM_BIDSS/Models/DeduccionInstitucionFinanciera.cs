using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(DeduccionInstitucionFinancieraMetadata))]
  
    public partial class tbDeduccionInstitucionFinanciera
    {
       
    }


    public class DeduccionInstitucionFinancieraMetadata
    {


        [Display(Name = "ID")]
        public int deif_IdDeduccionInstFinanciera { get; set; }


        [Display(Name = "Empleado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int emp_Id { get; set; }


        [Display(Name = "Institución Financiera")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int insf_Id { get; set; }


        [Display(Name = "Monto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<decimal> deif_Monto { get; set; }


        [Display(Name = "Comentario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string deif_Comentarios { get; set; }


        [Display(Name = "Usuario Crea")]
        public int deif_UsuarioCrea { get; set; }


        [Display(Name = "Fecha crea")]
        public System.DateTime deif_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> deif_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> deif_FechaModifica { get; set; }

        [Display(Name = "Activo")]
        public bool deif_Activo { get; set; }




        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbInstitucionFinanciera tbInstitucionFinanciera { get; set; }
    }
}