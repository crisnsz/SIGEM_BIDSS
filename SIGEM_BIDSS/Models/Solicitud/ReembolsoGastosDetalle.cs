using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models.Solicitud
{
    [MetadataType(typeof(ReembolsoGastosDetalleMetaData))]

    public class ReembolsoGastosDetalleMetaData
    {

        [Display(Name = "ID")]
        public int ReemgaDet_Id { get; set; }


        [Display(Name = "ID Reembolso")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int Reemga_Id { get; set; }


        [Display(Name = "Fecha Gasto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime ReemgaDet_FechaGasto { get; set; }


        [Display(Name = "Gasto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int tpv_Id { get; set; }


        [Display(Name = "Monto Gasto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public decimal ReemgaDet_MontoGasto { get; set; }


        [Display(Name = "Concepto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string ReemgaDet_Concepto { get; set; }

        [Display(Name = "Archivo")]
        public string ReemgaDet_Archivo { get; set; }




        public virtual tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos { get; set; }
        public virtual tbTipoViatico tbTipoViatico { get; set; }
    }

    public partial class tbSolicitudReembolsoGastosDetalle
    {

    }
}