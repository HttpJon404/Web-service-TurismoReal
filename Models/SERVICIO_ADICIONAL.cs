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
    
    public partial class SERVICIO_ADICIONAL
    {
        public decimal ID { get; set; }
        public decimal ID_TIPO { get; set; }
        public decimal ID_DETALLEARRIENDO { get; set; }
    
        public virtual DETALLE_ARRIENDO DETALLE_ARRIENDO { get; set; }
        public virtual TIPO_SERVICIO TIPO_SERVICIO { get; set; }
    }
}
