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
    
    public partial class tbMoneda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbMoneda()
        {
            this.tbSueldo = new HashSet<tbSueldo>();
            this.tbSueldo1 = new HashSet<tbSueldo>();
            this.tbSueldoHistorico = new HashSet<tbSueldoHistorico>();
        }
    
        public short tmo_Id { get; set; }
        public string tmo_Abreviatura { get; set; }
        public string tmo_Nombre { get; set; }
        public int tmo_UsuarioCrea { get; set; }
        public System.DateTime tmo_FechaCrea { get; set; }
        public Nullable<int> tmo_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tmo_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldo> tbSueldo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldo> tbSueldo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldoHistorico> tbSueldoHistorico { get; set; }
    }
}
