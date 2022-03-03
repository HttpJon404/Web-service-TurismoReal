using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.ClasesJson
{
    public class ContrasenaLink
    {
        public long ClnkId { get; set; }
        public string ClnkLink { get; set; }
        public int UsuId { get; set; }
        public bool ClnkEstado { get; set; }
        public int TclnId { get; set; }
        public string UsuPassword { get; set; }
        public System.DateTime ClnkFechaCreacion { get; set; }
        public Nullable<System.DateTime> ClnkFechaSeteoPass { get; set; }

    }
}