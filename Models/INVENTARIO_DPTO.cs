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
    
    public partial class INVENTARIO_DPTO
    {
        public decimal ID { get; set; }
        public decimal ID_DPTO { get; set; }
        public decimal TIPO_INVENTARIO { get; set; }
    
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual TIPO_INVENTARIO TIPO_INVENTARIO1 { get; set; }
    }
}