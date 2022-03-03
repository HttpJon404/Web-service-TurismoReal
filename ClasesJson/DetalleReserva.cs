using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.ClasesJson
{
    public class DetalleReserva
    {
        
            public decimal id { get; set; }
            public string id_dpto { get; set; }

            public string portada { get; set; }

            public string content_portada { get; set; }

            public DateTime fecha_llegada { get; set; }

            public DateTime fecha_salida { get; set; }

            public decimal valor_reserva { get; set; }

            public decimal valor_restante { get; set; }

            public decimal valor_extra { get; set; }

            public decimal valor_total { get; set; }

            public decimal id_estado { get; set; }

            public decimal multa { get; set; }

            public string pago { get; set; }

            public string[] nombre_servicio { get; set; }

            public decimal[] valor_servicio { get; set; }

            public string[] rut_acompa { get; set; }

            public string[] nombre_acompa { get; set; }

            public string[] apellidos_acompa { get; set; }

            public string[] celular_acompa { get; set; }

        
    }
}