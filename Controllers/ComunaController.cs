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
    public class ComunaController : ApiController
    {
        // GET: api/comuna
        [ResponseType(typeof(string))]
        public async Task<string> GetUbicacion()
        {
            string json = string.Empty;
            try

            {
                await Task.Run(() => {
                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                    OracleConnection con = new OracleConnection(constr);
                    con.Open();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("GET_UBICACION", con);
                    cmd.CommandType = CommandType.StoredProcedure;


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

        // GET: Comuna
        [ResponseType(typeof(string))]
        public async Task<string> GetRegionComuna(int id)
        {
            string json = string.Empty;
            try

            {
                await Task.Run(() => {
                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                    OracleConnection con = new OracleConnection(constr);
                    con.Open();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("GET_REGION_COMUNA", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter id_com =
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32, id, ParameterDirection.Input);
                    id_com.Size = 38;
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
    }
}
