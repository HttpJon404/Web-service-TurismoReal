//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACOMPANANTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACOMPANANTE()
        {
            this.DETALLE_ACOMPA = new HashSet<DETALLE_ACOMPA>();
        }
    
        public decimal ID { get; set; }
        public string RUT { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string CELULAR { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_ACOMPA> DETALLE_ACOMPA { get; set; }
    }
}
