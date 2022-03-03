using EntidadServicio;
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
    public class PagoController : ApiController
    {

        EntitiesTurismo db = new EntitiesTurismo();
        // POST: api/pago
        //crea contraseña link para recuperar contraseña
        [ResponseType(typeof(string))]
        public async Task<List<string>> PostPago(DetalleServicio detalle)
        {
            List<string> json = new List<string>();

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "PAGO_RESTANTE";

                        var det = new OracleParameter("P_ID_DETALLE", OracleDbType.Int32, detalle.id, ParameterDirection.Input);
                        det.Size = 38;

                        

                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;

                        


                        cmd.Parameters.AddRange(new[] { det, resultado});

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();


                        json.Add(resultado.Value.ToString());
                           




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
