using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(TipoPermisoMetaData))]
    public partial class tbTipoPermiso
    {
        [NotMapped]
        public List<tbTipoPermiso>  TipoPermiso { get; set; }
    }

    public class TipoPermisoMetaData
    {
        [Display(Name = "ID")]
        public int tperm_Id{ get; set; }
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MinLength(5, ErrorMessage = "Máximo {1} caracteres")]
        public string tperm_Descripcion { get; set; }
        [Display(Name = "Usuario Crea")]
        public int tperm_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime tperm_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tperm_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tperm_FechaModifica { get; set; }
    }
}