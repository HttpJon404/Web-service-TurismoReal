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
    
    public partial class TIPO_PAGO
    {
        public decimal ID { get; set; }
        public string DESCRIPCION { get; set; }
        public Nullable<decimal> ID_DETALLE { get; set; }
    
        public virtual DETALLE_ARRIENDO DETALLE_ARRIENDO { get; set; }
    }
}
