using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(TipoSangreMetaData))]
    public partial class tbTipoSangre
    {
        [NotMapped]
        public List<tbTipoSangre> AreaList { get; set; }

    }
    public class TipoSangreMetaData
    {

        [Display(Name = "ID")]
        public int tps_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")] 
        public string tps_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tps_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tps_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tps_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tps_FechaModifica { get; set; }

      
        public virtual ICollection<tbEmpleado> tbEmpleado { get; set; }
    }

  
}