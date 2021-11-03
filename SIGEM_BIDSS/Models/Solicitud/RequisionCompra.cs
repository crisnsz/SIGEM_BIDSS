using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(RequisionCompraMetadata))]
    public partial class tbRequisionCompra
    {
        [NotMapped]
        [Display(Name = "Jefe de Área")]
        public string Reqco_Jefe { get; set; }
        [NotMapped]
        [Display(Name = "Puesto")]
        public string pto_Id { get; set; }
        [NotMapped]
        [Display(Name = "Área")]
        public string Area { get; set; }
    }


    public class RequisionCompraMetadata
    {
        [Display(Name = "Codigo")]
        public int Reqco_Id { get; set; }

        [Display(Name = "Correlativo")]
        public string Reqco_Correlativo { get; set; }

        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Área")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public int are_Id { get; set; }

        [Display(Name = "Fecha Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime Reqco_GralFechaSolicitud { get; set; }

        [Display(Name = "Comentario")]
        public string Reqco_Comentario { get; set; }

        [Display(Name = "Estado")]
        public int est_Id { get; set; }

        [Display(Name = "Razon de Rechazo")]
        public string Reqco_RazonRechazo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int Reqco_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime Reqco_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> Reqco_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> Reqco_FechaModifica { get; set; }

    }


}