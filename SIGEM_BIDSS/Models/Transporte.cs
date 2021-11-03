using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(TransporteMetaData))]
    public partial class tbTipoTransporte
    {
        [NotMapped]
        public List<tbTipoTransporte> TRansporteList { get; set; }
    }

    public class TransporteMetaData
    {
        [Display(Name = "ID")]
        public int tptran_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tptran_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tptran_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tptran_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tptran_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tptran_FechaModifica { get; set; }
    }
}