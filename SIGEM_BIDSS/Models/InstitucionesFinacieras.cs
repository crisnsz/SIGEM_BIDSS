using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(InstFinacierasMetaData))]
    public partial class tbInstitucionFinanciera
    {

    }

    public class InstFinacierasMetaData
    {
        [Display(Name = "ID")]
        public int insf_Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string insf_Nombre { get; set; }

        [Display(Name = "Contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string insf_Contacto { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Phone(ErrorMessage = "Por favor complete el campo Télefono")]
        public string insf_Telefono { get; set; }


        [Display(Name = "Correo Electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string insf_Correo { get; set; }

        [Display(Name = "Estado")]
        public bool insf_Activo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int insf_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime insf_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> insf_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> insf_FechaModifica { get; set; }
    }
}