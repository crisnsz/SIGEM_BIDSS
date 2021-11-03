using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(ProductoMetadata))]
    public partial class tbProducto
    {

        [NotMapped]
        [Display(Name = "Unidad Medida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int uni_Descripcion { get; set; }

        [NotMapped]
        [Display(Name = "Proveedor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int pro_Nombre { get; set; }

    }
    public class ProductoMetadata
    {
        [Display(Name = "ID")]
        public int prod_Id { get; set; }

        [Display(Name = "Correlativo")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Código de Barras")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_CodigoBarras { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Descripcion { get; set; }

        [Display(Name = "Marca")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Marca { get; set; }

        [Display(Name = "Modelo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Modelo { get; set; }

        [Display(Name = "Talla")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Talla { get; set; }

        [Display(Name = "Color")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string prod_Color { get; set; }

        [Display(Name = "Subcategoría")]
      
        public int pscat_Id { get; set; }

        [Display(Name = "Unidad de Medida")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int uni_Id { get; set; }

        [Display(Name = "Proveedor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int prov_Id { get; set; }

        [Display(Name = "Estado")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public bool prod_EsActivo { get; set; }

        [Display(Name = "Razón Inactivación")]
        public string prod_RazonInactivacion { get; set; }
    }
}