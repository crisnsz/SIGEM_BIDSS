using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(AnticipoSalarioMetadata))]
    public partial class tbAnticipoSalario
    {
        [NotMapped]
        public string nRazonRechazo { get; set; }

        [NotMapped]
        [Display(Name = "Monto a Solicitar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string Cantidad { get; set; }

        [NotMapped]
        [Display(Name = "Sueldo")]
        public decimal Sueldo { get; set; }

        [NotMapped]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Porcentaje")]
        public decimal Porcentaje { get; set; }
    }


    public class AnticipoSalarioMetadata
    {
        [Display(Name = "ID")]
        public int Ansal_Id { get; set; }

        [Display(Name = "Correlativo")]
        public string Ansal_Correlativo { get; set; }

        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Jefe Inmediato")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")] 
        public int Ansal_JefeInmediato { get; set; }

        [Display(Name = "Fecha de Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Ansal_GralFechaSolicitud { get; set; }

        [Display(Name = "Monto a Solicitar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")] 
        public decimal Ansal_MontoSolicitado { get; set; }

        [Display(Name = "Tipo de Salario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")] 
        public int tpsal_id { get; set; }

        [Display(Name = "Justificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(250, ErrorMessage = "Máximo {1} caracteres")]
        public string Ansal_Justificacion { get; set; }

        [Display(Name = "Comentario")]
        [MaxLength(250, ErrorMessage = "Máximo {1} caracteres")]
        public string Ansal_Comentario { get; set; }

        [Display(Name = "Estado")]
        public int est_Id { get; set; }

        [Display(Name = "Razón de Rechazo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(250, ErrorMessage = "Máximo {1} caracteres")]
        public string Ansal_RazonRechazo { get; set; }

        [Display(Name = "Usuario Crea")]
        public int Ansal_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public System.DateTime Ansal_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")] 
        public Nullable<int> Ansal_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Ansal_FechaModifica { get; set; }
    }

}