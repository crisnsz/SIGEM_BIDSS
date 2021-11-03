using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(LiquidacionViaticoMetaData))]
    public partial class tbLiquidacionAnticipoViatico
    {
        [NotMapped]
        public List<LiquidacionViaticoMetaData> LiquidacioAnticipoList { get; set; }
    }

    public class LiquidacionViaticoMetaData
    {
        [Display(Name = "ID")]
        public int Lianvi_Id { get; set; }
        [Display(Name = "Correlativo")]
        public string Lianvi_Correlativo { get; set; }

        [Display(Name = "ID Anticipo de Viático")]
        public int Anvi_Id { get; set; } 

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Liquidación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public System.DateTime Lianvi_FechaLiquida { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Inicio De Viaje")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public System.DateTime Lianvi_FechaInicioViaje { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Regreso Viaje")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public System.DateTime Lianvi_FechaFinViaje { get; set; }

        [Display(Name = "Comentario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string Lianvi_Comentario { get; set; }

        [Display(Name = "Estado")]
        public int est_Id { get; set; }
        [Display(Name = "Razón de rechazo")]
        public string Lianvi_RazonRechazo { get; set; }
        [Display(Name = "Usuario Creación")]
        public int Lianvi_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Creación")]
        public System.DateTime Lianvi_FechaCrea { get; set; }
        [Display(Name = "Usuario Modificación")]
        public Nullable<int> Lianvi_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modificación")]
        public Nullable<System.DateTime> Lianvi_FechaModifica { get; set; }

    }
}