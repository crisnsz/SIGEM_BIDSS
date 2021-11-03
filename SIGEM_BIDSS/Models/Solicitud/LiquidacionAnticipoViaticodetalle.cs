using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(LiquidacionAnticipoViaticoDetalleMetaData))]
    public partial class tbLiquidacionAnticipoViaticoDetalle
    {
        [NotMapped]
        public List<tbLiquidacionAnticipoViaticoDetalle> LiquidacionAnticipoViaticoDetalles { get; set; }
    }

    public class LiquidacionAnticipoViaticoDetalleMetaData
    {
        [Display(Name = "ID")]
        public int Lianvide_Id { get; set; }
      

        [Display(Name = "Liquidación Anticipo Viático")]
        public int Lianvi_Id { get; set; }
      


        [Display(Name = "Fecha Gastos")]
        public System.DateTime Lianvide_FechaGasto { get; set; }
        


        [Display(Name = "Tipo Gasto")]  
        public int tpv_Id { get; set; }
      

        [Display(Name = "Monto Gastos")]
        public decimal Lianvide_MontoGasto { get; set; }
    


        [Display(Name = "Concepto")]
        public string Lianvide_Concepto { get; set; }
      
        [Display(Name = "Archivo")]
        public string Lianvide_Archivo { get; set; }
   
        //public int Lianvide_UsuarioCrea { get; set; }
        //public System.DateTime Lianvide_FechaCrea { get; set; }
        //public Nullable<int> Lianvide_UsuarioModifica { get; set; }
        //public Nullable<System.DateTime> Lianvide_FechaModifica { get; set; }

    }
}