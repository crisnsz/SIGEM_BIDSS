using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(ProveedorMetaData))]
    public partial class tbProveedor
    {
        [NotMapped]
        public List<tbProveedor> ProveedorList { get; set; }

   
    }

    public class ProveedorMetaData
    {
        [Display(Name = "ID")]
        public int prov_Id { get; set; }
        
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]


        public string prov_Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]


        [Display(Name = "Contacto")]
        public string prov_NombreContacto { get; set; }


       
        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prov_Direccion { get; set; }

       
        [Display(Name = "Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string mun_codigo { get; set; }



        [Display(Name = "Correo Electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prov_Email { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Phone(ErrorMessage = "Por favor complete el campo Télefono")]
        public string prov_Telefono { get; set; }
       

        [Display(Name = "RTN")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prov_RTN { get; set; }

       
        [Display(Name = "Actividad Económica")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public short acte_Id { get; set; }
  

    }
}