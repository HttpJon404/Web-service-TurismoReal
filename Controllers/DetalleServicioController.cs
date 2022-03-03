using EntidadServicio;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class DetalleServicioController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();

        // GET: api/DetalleServicio
        public IQueryable<SERVICIO_ADICIONAL> GetSERVICIO_ADICIONAL()
        {
            return db.SERVICIO_ADICIONAL;
        }

        // GET: api/DetalleServicio/5
        [ResponseType(typeof(string))]
        public async Task<string> GetDetalleServicioID(decimal id)
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
                    OracleCommand cmd = new OracleCommand("GET_DETALLE_SERVICIOS", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter id_user =
                        cmd.Parameters.Add("P_ID_USUARIO", OracleDbType.Int32, id, ParameterDirection.Input);
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
                throw;

            }


        }

        // PUT: api/DetalleServicio/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSERVICIO_ADICIONAL(decimal id, SERVICIO_ADICIONAL sERVICIO_ADICIONAL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sERVICIO_ADICIONAL.ID)
            {
                return BadRequest();
            }

            db.Entry(sERVICIO_ADICIONAL).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SERVICIO_ADICIONALExists(id))
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

        // POST: api/DetalleServicio
        [ResponseType(typeof(SERVICIO_ADICIONAL))]
        public async Task<IHttpActionResult> PostSERVICIO_ADICIONAL(SERVICIO_ADICIONAL sERVICIO_ADICIONAL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SERVICIO_ADICIONAL.Add(sERVICIO_ADICIONAL);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SERVICIO_ADICIONALExists(sERVICIO_ADICIONAL.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sERVICIO_ADICIONAL.ID }, sERVICIO_ADICIONAL);
        }

        // DELETE: api/DetalleServicio/5
        [ResponseType(typeof(SERVICIO_ADICIONAL))]
        public async Task<IHttpActionResult> DeleteSERVICIO_ADICIONAL(decimal id)
        {
            SERVICIO_ADICIONAL sERVICIO_ADICIONAL = await db.SERVICIO_ADICIONAL.FindAsync(id);
            if (sERVICIO_ADICIONAL == null)
            {
                return NotFound();
            }

            db.SERVICIO_ADICIONAL.Remove(sERVICIO_ADICIONAL);
            await db.SaveChangesAsync();

            return Ok(sERVICIO_ADICIONAL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SERVICIO_ADICIONALExists(decimal id)
        {
            return db.SERVICIO_ADICIONAL.Count(e => e.ID == id) > 0;
        }
    }
}