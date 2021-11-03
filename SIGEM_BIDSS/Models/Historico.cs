using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(HistoricoMetadata))]
    public partial class tbSueldoHistorico
    {
    }
    public class HistoricoMetadata
    {
        [Display(Name = "ID")]
        public int sue_Id { get; set; }

        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }


        [Display(Name = "Sueldo Anterior")]
        public Nullable<decimal> sue_Cantidad { get; set; }


        [Display(Name = "Moneda")]
        public short tmo_Id { get; set; }


        [Display(Name = "Usuario Crea")]
        public int sue_UsuarioCrea { get; set; }


        [Display(Name = "Fecha Crea")]
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }


        [Display(Name = "Usuario Modifica")]
        public Nullable<int> sue_UsuarioModifica { get; set; }


        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }
    }
   
}