using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.ClasesJson
{
    public class UsuarioJson
    {
        public class Rootobject
        {
            public Item[] items { get; set; }
            public bool hasMore { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public int count { get; set; }
            public Link1[] links { get; set; }
        }

        public class Item
        {
            public decimal id { get; set; }
            public decimal id_comuna { get; set; }
            public decimal id_region { get; set; }
            public string rut { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public string direccion { get; set; }
            public string correo { get; set; }
            public string celular { get; set; }
            public decimal edad { get; set; }
            public string genero { get; set; }
            public string contrasena { get; set; }
            public string estado { get; set; }
            public decimal id_rol { get; set; }
            public string descripcion_rol { get; set; }
            public Link[] links { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
        }

        public class Link1
        {
            public string rel { get; set; }
            public string href { get; set; }
        }
    }
}