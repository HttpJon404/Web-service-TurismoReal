using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.ClasesJson
{
    public class GastosAdmin
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
        public int Id_Depto { get; set; }

    }
}