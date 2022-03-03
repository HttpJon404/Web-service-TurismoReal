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

namespace WebService.Controllers
{
    public class TipoInventarioController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();

        // GET: api/TipoInventario
        
        [ResponseType(typeof(string))]
        public async Task<string> GetTipoInventario()
        {
            string json = string.Empty;
            try

            {
                await Task.Run(() => {

                    //Cambiar value de ruta archivos para la conexion de oracle en web.config
                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                    OracleConnection con = new OracleConnection(constr);
                    con.OpenAsync();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("GET_TIPO_INVENTARIO", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    // p1 retorna REF CURSOR de select * from usuarios;
                    OracleParameter p1 =
                      cmd.Parameters.Add("C_CURSOR_OUT", OracleDbType.RefCursor);
                    p1.Direction = ParameterDirection.Output;

                    // Ejecuta comando
                    cmd.ExecuteNonQueryAsync();

                    // leer data del cursor
                    var cursor = new Object();

                    if (p1.Value != null && p1.Value.ToString() != "" && p1.Value.ToString() != "{}" && p1.Size == 0)
                    {
                        cursor = p1.Value;
                        OracleDataReader reader1 = ((OracleRefCursor)cursor).GetDataReader();
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

                    }
                    else
                    {
                        return "Vacio";
                      
                    }


                    
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

        // GET: api/TipoInventario/5
        [ResponseType(typeof(TIPO_INVENTARIO))]
        public async Task<IHttpActionResult> GetTIPO_INVENTARIO(decimal id)
        {
            TIPO_INVENTARIO tIPO_INVENTARIO = await db.TIPO_INVENTARIO.FindAsync(id);
            if (tIPO_INVENTARIO == null)
            {
                return NotFound();
            }

            return Ok(tIPO_INVENTARIO);
        }

        // PUT: api/TipoInventario/5
        [ResponseType(typeof(void))]
        public async Task<string> PutTIPO_INVENTARIO(Inventario inventario)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "UPDATE_INVENTARIO";

                        var id = new OracleParameter("P_ID_INVENTARIO", OracleDbType.Int32, inventario.Id, ParameterDirection.Input);
                        id.Size = 38;

                        var descripcion = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, inventario.Descripcion, ParameterDirection.Input);
                        descripcion.Size = 200;

                        var precio = new OracleParameter("P_PRECIO", OracleDbType.Int32, inventario.Precio, ParameterDirection.Input);
                        precio.Size = 38;



                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;




                        cmd.Parameters.AddRange(new[] { id,descripcion, precio, resultado });

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();


                        json = resultado.Value.ToString();





                    }

                });



                if (json == "OK")
                {
                    return json;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: api/TipoInventario
        [ResponseType(typeof(TIPO_INVENTARIO))]
        public async Task<string> PostTIPO_INVENTARIO(Inventario inventario)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CREAR_INVENTARIO";

                    var descripcion = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, inventario.Descripcion, ParameterDirection.Input);
                        descripcion.Size = 200;

                        var precio = new OracleParameter("P_PRECIO", OracleDbType.Int32, inventario.Precio, ParameterDirection.Input);
                        precio.Size = 38;



                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;




                        cmd.Parameters.AddRange(new[] { descripcion,precio, resultado });

                        cmd.Connection.OpenAsync();
                        var res = cmd.ExecuteNonQueryAsync();
                        cmd.Connection.Close();


                        json = resultado.Value.ToString();





                    }

                });



                if (json == "OK")
                {
                    return json;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE: api/TipoInventario/5
        [ResponseType(typeof(TIPO_INVENTARIO))]
        public async Task<string> DeleteTIPO_INVENTARIO(Inventario inventario)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DELETE_INVENTARIO";

                        

                        var id = new OracleParameter("P_ID_INVENTARIO", OracleDbType.Int32, inventario.Id, ParameterDirection.Input);
                        id.Size = 38;



                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;




                        cmd.Parameters.AddRange(new[] { id, resultado });

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();


                        json = resultado.Value.ToString();





                    }

                });



                if (json == "OK")
                {
                    return json;
                }
                else
                {
                    return "";
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

        private bool TIPO_INVENTARIOExists(decimal id)
        {
            return db.TIPO_INVENTARIO.Count(e => e.ID == id) > 0;
        }
    }
}