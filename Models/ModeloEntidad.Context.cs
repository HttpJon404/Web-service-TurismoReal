﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EntitiesTurismo : DbContext
    {
        public EntitiesTurismo()
            : base("name=EntitiesTurismo")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACOMPANANTE> ACOMPANANTE { get; set; }
        public virtual DbSet<COMUNA> COMUNA { get; set; }
        public virtual DbSet<CONDUCTOR> CONDUCTOR { get; set; }
        public virtual DbSet<DEPARTAMENTO> DEPARTAMENTO { get; set; }
        public virtual DbSet<DETALLE_ACOMPA> DETALLE_ACOMPA { get; set; }
        public virtual DbSet<DETALLE_ARRIENDO> DETALLE_ARRIENDO { get; set; }
        public virtual DbSet<DISPONIBILIDAD> DISPONIBILIDAD { get; set; }
        public virtual DbSet<ESTADO_DPTO> ESTADO_DPTO { get; set; }
        public virtual DbSet<GASTOS_ADMINISTRATIVOS> GASTOS_ADMINISTRATIVOS { get; set; }
        public virtual DbSet<IMAGENES> IMAGENES { get; set; }
        public virtual DbSet<INVENTARIO_DPTO> INVENTARIO_DPTO { get; set; }
        public virtual DbSet<PROVINCIAS> PROVINCIAS { get; set; }
        public virtual DbSet<REGION> REGION { get; set; }
        public virtual DbSet<ROL> ROL { get; set; }
        public virtual DbSet<SERVICIO_ADICIONAL> SERVICIO_ADICIONAL { get; set; }
        public virtual DbSet<TIPO_INVENTARIO> TIPO_INVENTARIO { get; set; }
        public virtual DbSet<TIPO_PAGO> TIPO_PAGO { get; set; }
        public virtual DbSet<TIPO_SERVICIO> TIPO_SERVICIO { get; set; }
        public virtual DbSet<TRANSPORTE> TRANSPORTE { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<USUARIO_ROL> USUARIO_ROL { get; set; }
    
        public virtual int CREATE_USUARIO(Nullable<decimal> p_ID_COMUNA, Nullable<decimal> p_ID_REGION, string p_RUT, string p_NOMBRES, string p_APELLIDOS, string p_DIRECCION, string p_CORREO, string p_EDAD, string p_CELULAR, string p_GENERO, string p_CONTRASENA, string p_ESTADO, Nullable<decimal> p_ID_ROL, ObjectParameter p_RESULTADO)
        {
            var p_ID_COMUNAParameter = p_ID_COMUNA.HasValue ?
                new ObjectParameter("P_ID_COMUNA", p_ID_COMUNA) :
                new ObjectParameter("P_ID_COMUNA", typeof(decimal));
    
            var p_ID_REGIONParameter = p_ID_REGION.HasValue ?
                new ObjectParameter("P_ID_REGION", p_ID_REGION) :
                new ObjectParameter("P_ID_REGION", typeof(decimal));
    
            var p_RUTParameter = p_RUT != null ?
                new ObjectParameter("P_RUT", p_RUT) :
                new ObjectParameter("P_RUT", typeof(string));
    
            var p_NOMBRESParameter = p_NOMBRES != null ?
                new ObjectParameter("P_NOMBRES", p_NOMBRES) :
                new ObjectParameter("P_NOMBRES", typeof(string));
    
            var p_APELLIDOSParameter = p_APELLIDOS != null ?
                new ObjectParameter("P_APELLIDOS", p_APELLIDOS) :
                new ObjectParameter("P_APELLIDOS", typeof(string));
    
            var p_DIRECCIONParameter = p_DIRECCION != null ?
                new ObjectParameter("P_DIRECCION", p_DIRECCION) :
                new ObjectParameter("P_DIRECCION", typeof(string));
    
            var p_CORREOParameter = p_CORREO != null ?
                new ObjectParameter("P_CORREO", p_CORREO) :
                new ObjectParameter("P_CORREO", typeof(string));
    
            var p_EDADParameter = p_EDAD != null ?
                new ObjectParameter("P_EDAD", p_EDAD) :
                new ObjectParameter("P_EDAD", typeof(string));
    
            var p_CELULARParameter = p_CELULAR != null ?
                new ObjectParameter("P_CELULAR", p_CELULAR) :
                new ObjectParameter("P_CELULAR", typeof(string));
    
            var p_GENEROParameter = p_GENERO != null ?
                new ObjectParameter("P_GENERO", p_GENERO) :
                new ObjectParameter("P_GENERO", typeof(string));
    
            var p_CONTRASENAParameter = p_CONTRASENA != null ?
                new ObjectParameter("P_CONTRASENA", p_CONTRASENA) :
                new ObjectParameter("P_CONTRASENA", typeof(string));
    
            var p_ESTADOParameter = p_ESTADO != null ?
                new ObjectParameter("P_ESTADO", p_ESTADO) :
                new ObjectParameter("P_ESTADO", typeof(string));
    
            var p_ID_ROLParameter = p_ID_ROL.HasValue ?
                new ObjectParameter("P_ID_ROL", p_ID_ROL) :
                new ObjectParameter("P_ID_ROL", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CREATE_USUARIO", p_ID_COMUNAParameter, p_ID_REGIONParameter, p_RUTParameter, p_NOMBRESParameter, p_APELLIDOSParameter, p_DIRECCIONParameter, p_CORREOParameter, p_EDADParameter, p_CELULARParameter, p_GENEROParameter, p_CONTRASENAParameter, p_ESTADOParameter, p_ID_ROLParameter, p_RESULTADO);
        }
    }
}