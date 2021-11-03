using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(TipoViaticoMetaData))]
    public partial class tbTipoViatico
    {
        [NotMapped]
        public List<tbTipoViatico> TipoViaticoList { get; set; }
    }

    public class TipoViaticoMetaData
    {
        [Display(Name = "ID")]
        public int tpv_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
    
        public string tpv_Descripcion { get; set; }


    }
}