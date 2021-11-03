using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(ParametrosMetaData))]
    public partial class tbParametro
    {
    }
    public class ParametrosMetaData
    {

        [Display(Name = "Nombre Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_NombreEmpresa { get; set; }

        [Display(Name = "Teléfono de la Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Phone(ErrorMessage = "Por favor complete el campo Télefono")]
        public string par_TelefonoEmpresa { get; set; }

        [Display(Name = "Correo de la Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_CorreoEmpresa { get; set; }

        [Display(Name = "Correo Emisor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_CorreoEmisor { get; set; }

        [Display(Name = "Correo RRHH")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_CorreoRRHH { get; set; }


        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_Password { get; set; }

        [Display(Name = "Servidor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_Servidor { get; set; }

        [Display(Name = "Puerto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int par_Puerto { get; set; }

        [Display(Name = "Logo")]
      
        public string par_PathLogo { get; set; }


        [Display(Name = "Porcentaje Adelanto de Salario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Range(0, 100, ErrorMessage = "El porcentaje de Adelanto de Salario no debe ser mayor al 100%")]
        public decimal par_PorcentajeAdelantoSalario { get; set; }



        [Display(Name = "Frecuencia de Adelanto de Salario")]
        [Range(1, 12, ErrorMessage = "La frecuencia de Adelanto de Salario no debe ser mayor a 12")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int par_FrecuenciaAdelantoSalario { get; set; }
    }
}