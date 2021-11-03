using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(MonedaMetaData))]
    public partial class tbMoneda
    {

        [NotMapped]
        public List<tbMoneda> TipoMonedaList { get; set; }

    }
    public class MonedaMetaData
    {

        [Display(Name = "ID")]
        public short tmo_Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Abreviatura")]
        [MinLength(3, ErrorMessage = "Minimo {1} caracteres")]
        public string tmo_Abreviatura { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string tmo_Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Usuario Crea")]
        public int tmo_UsuarioCrea { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Crea")]
        public System.DateTime tmo_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tmo_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tmo_FechaModifica { get; set; }
    }

   


}