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
    public class ServicioController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();

        // GET: api/Servicio
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
                    con.Open();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("GET_TIPO_SERVICIOS", con);
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


        // GET: api/Servicio/5
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

        // PUT: api/Servicio/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTIPO_SERVICIO(decimal id, TIPO_SERVICIO tIPO_SERVICIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tIPO_SERVICIO.ID)
            {
                return BadRequest();
            }

            db.Entry(tIPO_SERVICIO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TIPO_SERVICIOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Servicio
        [ResponseType(typeof(TIPO_SERVICIO))]
        public async Task<IHttpActionResult> PostTIPO_SERVICIO(TIPO_SERVICIO tIPO_SERVICIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TIPO_SERVICIO.Add(tIPO_SERVICIO);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TIPO_SERVICIOExists(tIPO_SERVICIO.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tIPO_SERVICIO.ID }, tIPO_SERVICIO);
        }

        // DELETE: api/Servicio/5
        [ResponseType(typeof(TIPO_SERVICIO))]
        public async Task<IHttpActionResult> DeleteTIPO_SERVICIO(decimal id)
        {
            TIPO_SERVICIO tIPO_SERVICIO = await db.TIPO_SERVICIO.FindAsync(id);
            if (tIPO_SERVICIO == null)
            {
                return NotFound();
            }

            db.TIPO_SERVICIO.Remove(tIPO_SERVICIO);
            await db.SaveChangesAsync();

            return Ok(tIPO_SERVICIO);
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