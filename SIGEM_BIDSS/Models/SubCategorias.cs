using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(SubCategoriaMetaData))]
    public partial class tbProductoSubcategoria
    {

    }

    public class SubCategoriaMetaData
    {
        [Display(Name = "ID")]
        public int pscat_Id { get; set; }

        [Display(Name = "Subcategoría")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MinLength(5, ErrorMessage = "Máximo {1} caracteres")]


        public string pscat_Descripcion { get; set; }


        [Display(Name = "Estado")]
        public string pscat_EsActiva { get; set; }

    }
}