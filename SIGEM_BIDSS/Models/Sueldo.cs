using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(SueldoMetadata))]
    public partial class tbSueldo
    {
        [NotMapped]
        public string NombreEmpleado { get; set; }

        [NotMapped]
        [Display(Name = "Sueldo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
       
        public string Cantidad { get; set; }
    }
    public class SueldoMetadata
    {
        [Display(Name = "ID")]
        public int sue_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

   
        public Nullable<decimal> sue_Cantidad { get; set; }
        decimal value;
      



        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Moneda")]
        public short tmo_Id { get; set; }

        [Display(Name = "Usuario Crea")]
        public int sue_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> sue_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }

        public virtual tbMoneda tbMoneda { get; set; }
        public virtual tbEmpleado tbEmpleado { get; set; }
    }
}