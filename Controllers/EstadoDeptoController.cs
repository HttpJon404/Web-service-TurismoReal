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
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;


namespace WebService.Controllers
{
    public class EstadoDeptoController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();

        // GET: api/EstadoDepto
        [ResponseType(typeof(string))]
        public async Task<string> GetEstado()
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
                    OracleCommand cmd = new OracleCommand("GET_ESTADO_DEPTO", con);
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
        // GET: api/EstadoDepto/5
        [ResponseType(typeof(ESTADO_DPTO))]
        public IHttpActionResult GetESTADO_DPTO(decimal id)
        {
            ESTADO_DPTO eSTADO_DPTO = db.ESTADO_DPTO.Find(id);
            if (eSTADO_DPTO == null)
            {
                return NotFound();
            }

            return Ok(eSTADO_DPTO);
        }

        // PUT: api/EstadoDepto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutESTADO_DPTO(decimal id, ESTADO_DPTO eSTADO_DPTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eSTADO_DPTO.ID)
            {
                return BadRequest();
            }

            db.Entry(eSTADO_DPTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ESTADO_DPTOExists(id))
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

        // POST: api/EstadoDepto
        [ResponseType(typeof(ESTADO_DPTO))]
        public IHttpActionResult PostESTADO_DPTO(ESTADO_DPTO eSTADO_DPTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ESTADO_DPTO.Add(eSTADO_DPTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ESTADO_DPTOExists(eSTADO_DPTO.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eSTADO_DPTO.ID }, eSTADO_DPTO);
        }

        // DELETE: api/EstadoDepto/5
        [ResponseType(typeof(ESTADO_DPTO))]
        public IHttpActionResult DeleteESTADO_DPTO(decimal id)
        {
            ESTADO_DPTO eSTADO_DPTO = db.ESTADO_DPTO.Find(id);
            if (eSTADO_DPTO == null)
            {
                return NotFound();
            }

            db.ESTADO_DPTO.Remove(eSTADO_DPTO);
            db.SaveChanges();

            return Ok(eSTADO_DPTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ESTADO_DPTOExists(decimal id)
        {
            return db.ESTADO_DPTO.Count(e => e.ID == id) > 0;
        }
    }
}