using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Procedimientos
{
    public class CreateUsuario
    {
        public int id_comuna { get; set; }
        public int id_region { get; set; }
        public string rut { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }

        public int edad { get; set; }
        public string celular { get; set; }
        
        public string genero { get; set; }
        public string contrasena { get; set; }
        public string estado { get; set; }
        public int id_rol { get; set; }
        public string resultado { get; set; }
    }
}