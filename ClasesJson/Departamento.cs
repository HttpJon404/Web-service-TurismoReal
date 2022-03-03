using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.ClasesJson
{
    public class Departamento
    {
        public int id { get; set; }

        public int dormitorios { get; set; }

        public int baños { get; set; }
        public decimal metrosm2 { get; set; }
        public int estacionamiento { get; set; }
        public string direccion { get; set; }
        public int id_comuna { get; set; }

        public string nombre_comuna { get; set; }

        public int region { get; set; }

        public string nombre_region { get; set; }
        public int id_estado { get; set; }

        public string nombre_estado { get; set; }
        public decimal valor_arriendo { get; set; }
        public string condiciones { get; set; }
        public string resultado { get; set; }
        public int[] tipo_inventario { get; set; }

        public string nombre_inventario { get; set; }

        public decimal precio_inventario { get; set; }

        public string[] ruta_archivo { get; set; }

        public string portada { get; set; }

        public string content_portada { get; set; }

        public DateTime fecha_creacion {get; set;}

        public string contenttype { get; set; }

        public int orden { get; set; }
        public int[] id_gastos { get; set; }

        public string nombre_gastos { get; set; }

        public decimal valor_gasto { get; set; }




    }
}