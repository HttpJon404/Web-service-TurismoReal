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
    public class TipoServicioController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();

        // GET: api/TipoServicio
        public IQueryable<TIPO_SERVICIO> GetTIPO_SERVICIO()
        {
            return db.TIPO_SERVICIO;
        }

        // GET: api/TipoServicio/5
        [ResponseType(typeof(TIPO_SERVICIO))]
        public async Task<IHttpActionResult> GetTIPO_SERVICIO(decimal id)
        {
            TIPO_SERVICIO tIPO_SERVICIO = await db.TIPO_SERVICIO.FindAsync(id);
            if (tIPO_SERVICIO == null)
            {
                return NotFound();
            }

            return Ok(tIPO_SERVICIO);
        }

        // PUT: api/TipoServicio/5
        [ResponseType(typeof(void))]
        public async Task<string> PutTIPO_SERVICIO(tipoServicio servicio)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "UPDATE_SERVICIO";

                        var id= new OracleParameter("P_ID_SERVICIO", OracleDbType.Int32, servicio.Id, ParameterDirection.Input);
                        id.Size = 38;

                        var descripcion = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, servicio.Descripcion, ParameterDirection.Input);
                        descripcion.Size = 200;

                        var valor= new OracleParameter("P_VALOR", OracleDbType.Int32, servicio.Valor, ParameterDirection.Input);
                        valor.Size = 38;



                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;




                        cmd.Parameters.AddRange(new[] { id, descripcion, valor, resultado });

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

        // POST: api/TipoServicio
        [ResponseType(typeof(TIPO_INVENTARIO))]
        public async Task<string> PostTIPO_SERVICIO(tipoServicio servicio)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CREAR_SERVICIO";

                        var descripcion = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, servicio.Descripcion, ParameterDirection.Input);
                        descripcion.Size = 200;

                        var valor = new OracleParameter("P_VALOR", OracleDbType.Int32, servicio.Valor, ParameterDirection.Input);
                        valor.Size = 38;



                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;




                        cmd.Parameters.AddRange(new[] { descripcion, valor, resultado });

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

        // DELETE: api/TipoServicio/5
        [ResponseType(typeof(TIPO_INVENTARIO))]
        public async Task<string> DeleteTIPO_SERVICIO(tipoServicio servicios)
        {
            string json = string.Empty;

            try
            {
                await Task.Run(() => {

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DELETE_SERVICIO";



                        var id = new OracleParameter("P_ID_SERVICIO", OracleDbType.Int32, servicios.Id, ParameterDirection.Input);
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

        private bool TIPO_SERVICIOExists(decimal id)
        {
            return db.TIPO_SERVICIO.Count(e => e.ID == id) > 0;
        }
    }
}