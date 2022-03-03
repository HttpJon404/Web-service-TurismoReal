using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
    public class InventarioController : ApiController
    {
        [ResponseType(typeof(string))]

        public async Task<string> GetInventarioPorId(int id)
        {
            string json = string.Empty;
            try

            {
                await Task.Run(async () => {

                    //Cambiar value de ruta archivos para la conexion de oracle en web.config
                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                    OracleConnection con = new OracleConnection(constr);
                    await con.OpenAsync();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("OBTENER_INVENTARIO", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter id_dep =
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32, id, ParameterDirection.Input);
                    id_dep.Size = 38;
                    // p1 retorna REF CURSOR de select * from usuarios;
                    OracleParameter p1 =
                      cmd.Parameters.Add("C_CURSOR_OUT", OracleDbType.RefCursor);
                    p1.Direction = ParameterDirection.Output;

                    // Ejecuta comando
                    await cmd.ExecuteNonQueryAsync();

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
