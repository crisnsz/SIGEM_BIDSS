using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(UnidadMetaData))]
    public partial class tbUnidadMedida
    {
        [NotMapped]
        public List<tbUnidadMedida> Unidalist { get; set; }
    }

    public class UnidadMetaData
    {
        [Display(Name = "ID")]
        public int uni_Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]
        public string uni_Descripcion { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Abreviatura")]
        public string uni_Abreviatura { get; set; }
        [Display(Name = "Fecha Crea")]
        public int uni_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime uni_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> uni_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> uni_FechaModifica { get; set; }



    }
}