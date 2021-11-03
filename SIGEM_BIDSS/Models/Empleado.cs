using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(EmpleadosMetaData))]
    public partial class tbEmpleado
    {
        [NotMapped]
        public List<tbEmpleado> EmpleadoList { get; set; }

        [NotMapped]
        public string ge_Id { get; set; }

        [NotMapped]
        public string ge_Description { get; set; }

        [NotMapped]
        [Display(Name = "Área")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int are_Id { get; set; }

        [NotMapped]
        public List<tbEmpleado> Jefe { get; set; }

        [NotMapped]
        public bool  jefe_id { get; set; }

        [NotMapped]
        public string condicion { get; set; }

        [NotMapped]
        [Display(Name = "Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string dep_Codigo { get; set; }

    


    }


    public class EmpleadosMetaData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Id Empleado")]
        public int emp_Id { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombres")]
        public string emp_Nombres { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellidos")]
        public string emp_Apellidos { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Sexo")]
        public string emp_Sexo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public string emp_FechaNacimiento { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Identificación")]
        public string emp_Identificacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Teléfono")]
        public string emp_Telefono { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Correo Electrónico")]
        public string emp_CorreoElectronico { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo Sangre")]
        public int tps_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Puesto De Trabajo")]
        public int pto_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha ingresó a la empresa")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public string emp_FechaIngreso { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Dirección")]
        public string emp_Direccion { get; set; }



        [Display(Name = "Razón Inactivación")]
      
        public string emp_RazonInactivacion { get; set; }



        [Display(Name = "Estado")]
        public int est_Id { get; set; }

      
        [Display(Name = "Fotografía")]
        public string emp_PathImage { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Municipio")]
        public string mun_codigo { get; set; }


        [Display(Name = "Usuario Crea")]
        public Nullable<int> emp_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> emp_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> emp_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }

    }



}