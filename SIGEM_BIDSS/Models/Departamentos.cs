using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(DepartamentosMetaData))]
    public partial class tbDepartamento
    {
    }
    public class DepartamentosMetaData
    {


        [Display(Name = "Código Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string dep_Codigo { get; set; }

       
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string dep_Nombre { get; set; }


        //public int dep_UsuarioCrea { get; set; }
        //public System.DateTime dep_FechaCrea { get; set; }
        //public Nullable<int> dep_UsuarioModifica { get; set; }
        //public Nullable<System.DateTime> dep_FechaModifica { get; set; }
    }
}