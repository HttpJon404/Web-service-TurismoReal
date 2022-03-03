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
    public class ContrasenaController : ApiController
    {
        EntitiesTurismo db = new EntitiesTurismo();
        // POST: api/contrasena
        //crea contraseña link para recuperar contraseña
        [ResponseType(typeof(string))]
        public async Task<List<string>> PostContrasena(ContrasenaLink clink)
        {
            List<string> json = new List<string>();

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CREATE_CLINK";

                        var usuario = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32, clink.UsuId, ParameterDirection.Input);
                        usuario.Size = 38;

                        var type = new OracleParameter("P_TYPE", OracleDbType.Int32, clink.TclnId, ParameterDirection.Input);
                        type.Size = 38;

                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;

                        var link = new OracleParameter("P_LINK", OracleDbType.Varchar2, ParameterDirection.Output);
                        link.Size = 200;

                        var nombre = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, ParameterDirection.Output);
                        nombre.Size = 200;

                        var correo= new OracleParameter("P_CORREO", OracleDbType.Varchar2, ParameterDirection.Output);
                        correo.Size = 200;


                        cmd.Parameters.AddRange(new[] { usuario, type, resultado, link , nombre, correo});

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();


                        if (resultado.Value.ToString() == "OK")
                        {
                            json.Add(link.Value.ToString());
                            json.Add(nombre.Value.ToString());
                            json.Add(correo.Value.ToString());
                        }




                    }

                });



                if (json.Count > 0)
                {
                    return json;
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

        // GET: api/contrasena/5

        // valida que el link de recuperacion exista en la base de datos
        [ResponseType(typeof(string))]
        public async Task<string> GetObtenerLink(string link)
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
                    OracleCommand cmd = new OracleCommand("OBTENER_LINK", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter id_user =
                        cmd.Parameters.Add("P_LINK", OracleDbType.Varchar2, link, ParameterDirection.Input);
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

        // Put: api/contrasena
        //crea contraseña link para recuperar contraseña

        [ResponseType(typeof(string))]
        public async Task<List<string>> PutContrasena(ContrasenaLink clink)
        {
            List<string> json = new List<string>();

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "UPDATE_CONTRASENA";

                        var link = new OracleParameter("P_LINK", OracleDbType.Varchar2, clink.ClnkLink, ParameterDirection.Input);
                        link.Size = 200;

                        var pass = new OracleParameter("P_NEWPASS", OracleDbType.Varchar2, clink.UsuPassword, ParameterDirection.Input);
                        pass.Size = 200;

                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;

                        var correo = new OracleParameter("P_CORREO", OracleDbType.Varchar2, ParameterDirection.Output);
                        correo.Size = 200;


                        cmd.Parameters.AddRange(new[] { link, pass, resultado, correo });

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();


                        if (resultado.Value.ToString() == "CONTRASEÑA ACTUALIZADA CORRECTAMENTE")
                        {
                            json.Add(resultado.Value.ToString());
                            json.Add(correo.Value.ToString());
                        }




                    }

                });



                if (json.Count > 0)
                {
                    return json;
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


    }
}
