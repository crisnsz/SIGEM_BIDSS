using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(RequisicionCompraDetalleMetadata))]
    public partial class tbRequisionCompraDetalle
    {
        [NotMapped]
        [Display(Name = "Cantidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Cantidad { get; set; }
    }
    public class RequisicionCompraDetalleMetadata
    {
        public int Reqde_Id { get; set; }

        [Display(Name = "Requisicion de Compra")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int Reqco_Id { get; set; }

        [Display(Name = "Producto")]
        public int prod_Id { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Reqde_Cantidad { get; set; }

        [Display(Name = "Justificación")]
        public string Reqde_Justificacion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int Reqde_UsuarioCrea { get; set; }
        
        [Display(Name = "Fecha Crea")]
        public System.DateTime Reqde_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> Reqde_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> Reqde_FechaModifica { get; set; }

    }
   
}