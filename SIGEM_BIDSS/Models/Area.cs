using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(AreaMetaData))]
    public partial class tbArea
    {
        [NotMapped]
        public List<tbArea> AreaList { get; set; }
    }

    public class AreaMetaData
    {
        [Display(Name = "ID")]
        public int are_Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]   
      
        public string are_Descripcion { get; set; }
        [Display(Name = "Usuario Crea")]
        public int are_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime are_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> are_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> are_FechaModifica { get; set; }
               
    }
}