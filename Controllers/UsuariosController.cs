using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebService.ClasesJson;
using WebService.Models;
using WebService.Procedimientos;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace WebService.Controllers
{
    public class UsuariosController : ApiController
    {
        EntitiesTurismo db = new EntitiesTurismo();

        public UsuariosController() // dbcontext constructor

        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }



        // GET: api/usuarios
        [ResponseType(typeof(string))]
        public async Task<string> GetUSUARIO()
        {
            string json = string.Empty;
            try

            {
               await Task.Run(() =>{

                   //Cambiar value de ruta archivos para la conexion de oracle en web.config
                   string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                   OracleConnection con = new OracleConnection(constr);
                   con.Open();

                   // Crea comando oracle
                   OracleCommand cmd = new OracleCommand("GET_USUARIOS", con);
                   cmd.CommandType = CommandType.StoredProcedure;


                   // p1 retorna REF CURSOR de select * from usuarios;
                   OracleParameter p1 =
                     cmd.Parameters.Add("C_CURSOR_OUT", OracleDbType.RefCursor);
                   p1.Direction = ParameterDirection.Output;

                   // Ejecuta comando
                   cmd.ExecuteNonQuery();

                   // leer data del cursor
                   OracleDataReader reader1 = ((OracleRefCursor)p1.Value).GetDataReader();

                   //convertir a json el resultado
                   var result = Serializar.GetInstance().Serialize(reader1).ToList();
                   json = JsonConvert.SerializeObject(result);

                   reader1.Close();
                   reader1.Dispose();


                   p1.Dispose();


                   cmd.Dispose();

                   con.Close();
                   con.Dispose();
                   return json;
               });

                if (json.Length > 0)
                {
                    return json;
                }
                else
                {
                    return "Vacio";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        // GET: api/usuarios/5
        [ResponseType(typeof(string))]
        public async Task<string> GetUSUARIO_ID(decimal id)
        {
            string json = string.Empty;
            try { 
            await Task.Run(() =>
            {

                string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                OracleConnection con = new OracleConnection(constr);
                con.Open();

                // Crea comando oracle
                OracleCommand cmd = new OracleCommand("OBTENER_USUARIO", con);
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter id_user =
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32, id, ParameterDirection.Input);
                id_user.Size = 38;
                // p1 retorna REF CURSOR de select * from usuarios;
                OracleParameter p1 =
                  cmd.Parameters.Add("C_CURSOR", OracleDbType.RefCursor);
                p1.Direction = ParameterDirection.Output;

                // Ejecuta comando
                cmd.ExecuteNonQuery();

                // leer data del cursor
                OracleDataReader reader1 = ((OracleRefCursor)p1.Value).GetDataReader();

                //convertir a json el resultado
                var result = Serializar.GetInstance().Serialize(reader1).ToList();
                json = JsonConvert.SerializeObject(result);



                reader1.Close();
                reader1.Dispose();



                //res.Add(result);
                p1.Dispose();


                cmd.Dispose();

                con.Close();
                con.Dispose();


                return json;

                
            });
                if (json.Length>0)
                {
                    return json;
                }
                else
                {
                    return "Vacio";
                }
            }

            catch (Exception e)
            {
                return string.Empty;
                throw;
                
            }
            
            
        }


        // PUT: api/usuarios/5
        [ResponseType(typeof(string))]
        public async Task<List<string>> PutUsuario(EditUsuario usuario)
        {
            List<string> respuesta = new List<string>();
            //Console.WriteLine(usuario);
            //Console.WriteLine(usuario);
            //int idUsuario = (int)usuario.id;
            //int idComuna = (int)usuario.id_comuna;
            //int idRegion = (int)usuario.id_region;
            //int idRol = (int)usuario.id_rol;
            try
            {
                await Task.Run(() => {
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "UPDATE_USUARIO";

                        var id_usuario = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32, usuario.id, ParameterDirection.Input);
                        id_usuario.Size = 200;
                        var comuna = new OracleParameter("P_ID_COMUNA", OracleDbType.Int32, usuario.id_comuna, ParameterDirection.Input);
                        comuna.Size = 38;
                        var region = new OracleParameter("P_ID_REGION", OracleDbType.Int32, usuario.id_region, ParameterDirection.Input);
                        region.Size = 38;
                        var nombres = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2, usuario.nombres, ParameterDirection.Input);
                        nombres.Size = 200;                                               
                        var apellidos = new OracleParameter("P_APELLIDOS", OracleDbType.Varchar2, usuario.apellidos, ParameterDirection.Input);
                        apellidos.Size = 200;
                        var direccion = new OracleParameter("P_DIRECCION", OracleDbType.Varchar2, usuario.direccion, ParameterDirection.Input);
                        direccion.Size = 200;
                        var correo = new OracleParameter("P_CORREO", OracleDbType.Varchar2, usuario.correo, ParameterDirection.Input);
                        correo.Size = 200;
                        var celular = new OracleParameter("P_CELULAR", OracleDbType.Varchar2, usuario.celular, ParameterDirection.Input);
                        celular.Size = 200;
                        var genero = new OracleParameter("P_GENERO", OracleDbType.Varchar2, usuario.genero, ParameterDirection.Input);
                        genero.Size = 1;
                        var contrasena = new OracleParameter("P_CONTRASENA", OracleDbType.Varchar2, usuario.contrasena, ParameterDirection.Input);
                        contrasena.Size = 1000;
                        var estado = new OracleParameter("P_ESTADO", OracleDbType.Varchar2, usuario.estado, ParameterDirection.Input);
                        estado.Size = 1;
                        var id_rol = new OracleParameter("P_ID_ROL", OracleDbType.Int32, usuario.id_rol, ParameterDirection.Input);
                        id_rol.Size = 38;
                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;
                        

                        cmd.Parameters.AddRange(new[] { id_usuario, comuna, region, nombres, apellidos, direccion, correo, celular, genero, contrasena, estado, id_rol, resultado });

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        respuesta.Add(resultado.Value.ToString());
                        respuesta.Add(id_usuario.Value.ToString());

                    }

                    return respuesta;
                });
                if (respuesta.Count > 0)
                {
                    return respuesta;
                }
                else
                {
                    return new List<string>();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        // POST: api/usuarios
        [ResponseType(typeof(string))]
        public async Task<List<string>> PostUSUARIO(UsuarioJson.Item usuario)
        {
            List<string> respuesta = new List<string>();

            try
            {
                await Task.Run(() =>{
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CREATE_USUARIO";

                        var comuna = new OracleParameter("P_ID_COMUNA", OracleDbType.Int32, usuario.id_comuna, ParameterDirection.Input);
                        comuna.Size = 38;
                        var region = new OracleParameter("P_ID_REGION", OracleDbType.Int32, usuario.id_region, ParameterDirection.Input);
                        region.Size = 38;
                        var rut = new OracleParameter("P_RUT", OracleDbType.Varchar2, usuario.rut, ParameterDirection.Input);
                        rut.Size = 200;
                        var nombres = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2, usuario.nombres, ParameterDirection.Input);
                        nombres.Size = 200;
                        var apellidos = new OracleParameter("P_APELLIDOS", OracleDbType.Varchar2, usuario.apellidos, ParameterDirection.Input);
                        apellidos.Size = 200;
                        var direccion = new OracleParameter("P_DIRECCION", OracleDbType.Varchar2, usuario.direccion, ParameterDirection.Input);
                        direccion.Size = 200;
                        var correo = new OracleParameter("P_CORREO", OracleDbType.Varchar2, usuario.correo, ParameterDirection.Input);
                        correo.Size = 200;
                        var edad = new OracleParameter("P_EDAD", OracleDbType.Int32, usuario.edad, ParameterDirection.Input);
                        edad.Size = 38;
                        var celular = new OracleParameter("P_CELULAR", OracleDbType.Varchar2, usuario.celular, ParameterDirection.Input);
                        celular.Size = 200;
                        var genero = new OracleParameter("P_GENERO", OracleDbType.Varchar2, usuario.genero, ParameterDirection.Input);
                        genero.Size = 1;
                        var contrasena = new OracleParameter("P_CONTRASENA", OracleDbType.Varchar2, usuario.contrasena, ParameterDirection.Input);
                        contrasena.Size = 1000;
                        var estado = new OracleParameter("P_ESTADO", OracleDbType.Varchar2, usuario.estado, ParameterDirection.Input);
                        estado.Size = 1;
                        var id_rol = new OracleParameter("P_ID_ROL", OracleDbType.Int32, usuario.id_rol, ParameterDirection.Input);
                        id_rol.Size = 38;
                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;
                        var id_usuario = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32, ParameterDirection.Output);
                        id_usuario.Size = 200;

                        cmd.Parameters.AddRange(new[] { comuna, region, rut, nombres, apellidos, direccion, correo, edad, celular, genero, contrasena, estado, id_rol, resultado, id_usuario });

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        respuesta.Add(resultado.Value.ToString());
                        respuesta.Add(id_usuario.Value.ToString());

                    }
                    
                    return respuesta;
                });
                if (respuesta.Count > 0)
                {
                    return respuesta;
                }
                else
                {
                    return new List<string>();
                }

            }
            catch (Exception e)
            {

                throw;
            }
            
        }




        // DELETE: api/usuarios/5
        [ResponseType(typeof(string))]
        public async Task<List<string>> DeleteUSUARIO(EditEstadoUsuario usuario)
        {

            List<string> respuesta = new List<string>();

            try
            {
                await Task.Run(() => {
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DEACTIVATE_USER";

                       
                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;
                        var estado = new OracleParameter("P_ESTADO", OracleDbType.Varchar2, usuario.estado, ParameterDirection.Input);
                        estado.Size = 1;
                        var id_usuario = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32,usuario.id ,ParameterDirection.Input);
                        id_usuario.Size = 200;

                        cmd.Parameters.AddRange(new[] { id_usuario, estado, resultado });

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        respuesta.Add(resultado.Value.ToString());
                        respuesta.Add(id_usuario.Value.ToString());

                    }

                    return respuesta;
                });

                if (respuesta.Count > 0)
                {
                    return respuesta;
                }
                else
                {
                    return new List<string>();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIOExists(decimal id)
        {
            return db.USUARIO.Count(e => e.ID == id) > 0;
        }

        
    }
}