using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(InventariosCompraMetaData))]

    public partial class tbInventarioCompra
    {
    
    }

    public class InventariosCompraMetaData
    {

        [Display(Name = "ID")]

        public int invCom_Id { get; set; }


        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string invCom_Descripcion { get; set; }

        //public int invCom_UsuarioCrea { get; set; }

        //public System.DateTime invCom_FechaCrea { get; set; }

        //public Nullable<int> invCom_UsuarioModifica { get; set; }

        //public Nullable<System.DateTime> invCom_FechaModifica { get; set; }


    }

}