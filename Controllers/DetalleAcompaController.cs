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
    public class DetalleAcompaController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();

        // GET: api/DetalleAcompa
        public IQueryable<DETALLE_ACOMPA> GetDETALLE_ACOMPA()
        {
            return db.DETALLE_ACOMPA;
        }

        // GET: api/DetalleAcompa/5
        [ResponseType(typeof(string))]
        public async Task<string> GetDetalleAcompaID(decimal id)
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
                    OracleCommand cmd = new OracleCommand("GET_DETALLE_ACOMPANANTES", con);
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

        // PUT: api/DetalleAcompa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDETALLE_ACOMPA(decimal id, DETALLE_ACOMPA dETALLE_ACOMPA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dETALLE_ACOMPA.ID)
            {
                return BadRequest();
            }

            db.Entry(dETALLE_ACOMPA).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DETALLE_ACOMPAExists(id))
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

        // POST: api/DetalleAcompa
        [ResponseType(typeof(DETALLE_ACOMPA))]
        public async Task<IHttpActionResult> PostDETALLE_ACOMPA(DETALLE_ACOMPA dETALLE_ACOMPA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DETALLE_ACOMPA.Add(dETALLE_ACOMPA);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DETALLE_ACOMPAExists(dETALLE_ACOMPA.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dETALLE_ACOMPA.ID }, dETALLE_ACOMPA);
        }

        // DELETE: api/DetalleAcompa/5
        [ResponseType(typeof(DETALLE_ACOMPA))]
        public async Task<IHttpActionResult> DeleteDETALLE_ACOMPA(decimal id)
        {
            DETALLE_ACOMPA dETALLE_ACOMPA = await db.DETALLE_ACOMPA.FindAsync(id);
            if (dETALLE_ACOMPA == null)
            {
                return NotFound();
            }

            db.DETALLE_ACOMPA.Remove(dETALLE_ACOMPA);
            await db.SaveChangesAsync();

            return Ok(dETALLE_ACOMPA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DETALLE_ACOMPAExists(decimal id)
        {
            return db.DETALLE_ACOMPA.Count(e => e.ID == id) > 0;
        }
    }
}