//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGEM_BIDSS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbLiquidacionAnticipoViaticoDetalle
    {
        public int Lianvide_Id { get; set; }
        public int Lianvi_Id { get; set; }
        public System.DateTime Lianvide_FechaGasto { get; set; }
        public int tpv_Id { get; set; }
        public decimal Lianvide_MontoGasto { get; set; }
        public string Lianvide_Concepto { get; set; }
        public string Lianvide_Archivo { get; set; }
        public int Lianvide_UsuarioCrea { get; set; }
        public System.DateTime Lianvide_FechaCrea { get; set; }
        public Nullable<int> Lianvide_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Lianvide_FechaModifica { get; set; }
    
        public virtual tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico { get; set; }
        public virtual tbTipoViatico tbTipoViatico { get; set; }
    }
}
