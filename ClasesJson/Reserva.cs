using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadServicio
{
    public class Reserva
    {
        public int id_usuario { get; set; }

        public int id_dpto { get; set; }

        public string listaAcompanante { get; set; }

        public string listaServicios { get; set; }

        public DateTime fecha_llegada { get; set; }

        public DateTime fecha_salida { get; set; }

        public int valorReserva { get; set; }

        public int valorRestante { get; set; }

        public int valorTotalDias { get; set; }

        public int valorDiario { get; set; }

        public string pago { get; set; }
    }
}
