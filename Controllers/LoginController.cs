using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.ClasesJson;
using WebService.Models;
using WebService.Procedimientos;

namespace WebService.Controllers
{
    public class LoginController : ApiController
    {
        EntitiesTurismo db = new EntitiesTurismo();
        // POST: (api/login)
        [ResponseType(typeof(string))]
        public async Task<List<string>> LoginUSUARIO(Login log)
        {

            List<string> respuesta = new List<string>();
            try
            {
                await Task.Run(() => {
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "LOGIN_USUARIO";

                        var correos = new OracleParameter("P_CORREO", OracleDbType.Varchar2, log.correo, ParameterDirection.Input);
                        correos.Size = 200;
                        var contrasenas = new OracleParameter("P_CONTRASENA", OracleDbType.Varchar2, log.contrasena, ParameterDirection.Input);
                        contrasenas.Size = 1000;
                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;
                        var id_usuario = new OracleParameter("P_ID", OracleDbType.Int32, ParameterDirection.Output);
                        id_usuario.Size = 200;

                        cmd.Parameters.AddRange(new[] { correos, contrasenas, resultado, id_usuario });

                        cmd.Connection.OpenAsync();
                        var res = cmd.ExecuteNonQueryAsync();
                        cmd.Connection.Close();

                        respuesta.Add(resultado.Value.ToString());
                        respuesta.Add(id_usuario.Value.ToString());

                    }

                    return respuesta;

                });

                if (respuesta.Count>0)
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


        // GET: api/login/5

        // obtiene usuario por correo para recuperar contraseña
        [ResponseType(typeof(string))]
        public async Task<string> GetUSUARIO_ID(string correo)
        {
            string json = string.Empty;
            try
            {
                await Task.Run(() =>
                {
                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
               
                    OracleConnection con = new OracleConnection(constr);
                    con.Open();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("OBTENER_USUARIO_EMAIL", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter id_user =
                        cmd.Parameters.Add("P_CORREO", OracleDbType.Varchar2, correo, ParameterDirection.Input);
                    id_user.Size = 200;
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
                if (json.Length > 0)
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


    }
}
