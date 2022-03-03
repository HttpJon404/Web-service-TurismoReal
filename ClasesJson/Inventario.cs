using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.ClasesJson
{
    public class Inventario
    {
        public decimal Id { get; set; }
        public string Descripcion { get; set; }
        //public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}