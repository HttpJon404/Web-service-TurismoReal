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
    public class CheckinController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();
        // GET: api/checkin/
        [ResponseType(typeof(string))]
        public async Task<string> Getcheckin(decimal id)
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
                    OracleCommand cmd = new OracleCommand("GET_INVENTARIO_CHECKIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter id_user =
                        cmd.Parameters.Add("P_ID_DPTO", OracleDbType.Int32, id, ParameterDirection.Input);
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
                //throw;

            }


        }

        // POST: api/checkin
        [ResponseType(typeof(TIPO_INVENTARIO))]
        public async Task<string> PostCheckin(Departamento departamento)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GENERAR_CHECKIN";

                        

                        var id = new OracleParameter("P_ID_DPTO", OracleDbType.Int32, departamento.id, ParameterDirection.Input);
                        id.Size = 38;



                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;




                        cmd.Parameters.AddRange(new[] { id, resultado });

                        cmd.Connection.OpenAsync();
                        var res = cmd.ExecuteNonQueryAsync();
                        cmd.Connection.Close();


                        json = resultado.Value.ToString();





                    }

                });



                if (json == "CHECKIN GENERADO CON EXITO")
                {
                    return json;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception e)
            {

                return "";
            }
        }

        // Put: api/checkin
        [ResponseType(typeof(TIPO_INVENTARIO))]
        public async Task<string> PutCheckout(Checkout departamento)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GENERAR_CHECKOUT";



                        var id = new OracleParameter("P_ID_DPTO", OracleDbType.Int32, departamento.id, ParameterDirection.Input);
                        id.Size = 38;

                        var multa = new OracleParameter("P_MULTA", OracleDbType.Int32, departamento.multa, ParameterDirection.Input);
                        multa.Size = 38;



                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;




                        cmd.Parameters.AddRange(new[] { id, multa,resultado });

                        cmd.Connection.OpenAsync();
                        var res = cmd.ExecuteNonQueryAsync();
                        cmd.Connection.Close();


                        json = resultado.Value.ToString();





                    }

                });



                if (json == "CHECKOUT GENERADO CON EXITO")
                {
                    return json;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception e)
            {

                return "";
            }
        }
    }
}
