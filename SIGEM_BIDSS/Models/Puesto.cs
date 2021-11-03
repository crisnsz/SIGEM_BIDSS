using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(PuestoMetadata))]
    public partial class tbPuesto
    {
        [NotMapped]
        public List<tbPuesto> PuestoList { get; set; }
        
    }
    
    public class PuestoMetadata
    {
        [Display(Name = "ID")]
        public int pto_Id { get; set; }
        [Display(Name = "Área")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int are_Id { get; set; }
        [Display(Name = "Cargo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MinLength(5, ErrorMessage = "Minimo {1} caracteres")]
        public string pto_Descripcion { get; set; }
        [Display(Name = "Usuario Crea")]
        public int pto_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime pto_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> pto_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> pto_FechaModifica { get; set; }

    }
}