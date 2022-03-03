using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class GASTOS_ADMINISTRATIVOSController : ApiController
    {
        private EntitiesTurismo db = new EntitiesTurismo();

        // GET: api/GASTOS_ADMINISTRATIVOS
        public IQueryable<GASTOS_ADMINISTRATIVOS> GetGASTOS_ADMINISTRATIVOS()
        {
            return db.GASTOS_ADMINISTRATIVOS;
        }

        // GET: api/GASTOS_ADMINISTRATIVOS/5
        [ResponseType(typeof(GASTOS_ADMINISTRATIVOS))]
        public IHttpActionResult GetGASTOS_ADMINISTRATIVOS(decimal id)
        {
            GASTOS_ADMINISTRATIVOS gASTOS_ADMINISTRATIVOS = db.GASTOS_ADMINISTRATIVOS.Find(id);
            if (gASTOS_ADMINISTRATIVOS == null)
            {
                return NotFound();
            }

            return Ok(gASTOS_ADMINISTRATIVOS);
        }

        // PUT: api/GASTOS_ADMINISTRATIVOS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGASTOS_ADMINISTRATIVOS(decimal id, GASTOS_ADMINISTRATIVOS gASTOS_ADMINISTRATIVOS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gASTOS_ADMINISTRATIVOS.ID)
            {
                return BadRequest();
            }

            db.Entry(gASTOS_ADMINISTRATIVOS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GASTOS_ADMINISTRATIVOSExists(id))
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






        [ResponseType(typeof(string))]
        public async Task<List<string>> LoginUSUARIO(GastosAdmin gastos)
        {

            List<string> respuesta = new List<string>();
            try
            {
                await Task.Run(() => {
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {




                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CREAR_MANTENCION";

                        var descripcion = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2, gastos.Descripcion, ParameterDirection.Input);
                        descripcion.Size = 200;

                        var precio = new OracleParameter("P_PRECIO", OracleDbType.Int32, gastos.Valor ,ParameterDirection.Input);
                        precio.Size = 200;

                        var id_depto = new OracleParameter("P_ID_DEPTO", OracleDbType.Int32, gastos.Id_Depto ,ParameterDirection.Input);
                        id_depto.Size = 200;



                        //var contrasenas = new OracleParameter("P_CONTRASENA", OracleDbType.Varchar2, log.contrasena, ParameterDirection.Input);
                        //contrasenas.Size = 1000;

                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;


                        cmd.Parameters.AddRange(new[] { descripcion, precio, id_depto, resultado });

                        cmd.Connection.OpenAsync();
                        var res = cmd.ExecuteNonQueryAsync();
                        cmd.Connection.Close();

                        respuesta.Add(resultado.Value.ToString());
                        respuesta.Add(id_depto.Value.ToString());

                    }

                    return respuesta;

                });

                if (respuesta.Count > 0)
                {
                    return respuesta;
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





        // POST: api/GASTOS_ADMINISTRATIVOS
        //[ResponseType(typeof(GASTOS_ADMINISTRATIVOS))]
        //public IHttpActionResult PostGASTOS_ADMINISTRATIVOS(GASTOS_ADMINISTRATIVOS gASTOS_ADMINISTRATIVOS)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.GASTOS_ADMINISTRATIVOS.Add(gASTOS_ADMINISTRATIVOS);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (GASTOS_ADMINISTRATIVOSExists(gASTOS_ADMINISTRATIVOS.ID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = gASTOS_ADMINISTRATIVOS.ID }, gASTOS_ADMINISTRATIVOS);
        //}

        // DELETE: api/GASTOS_ADMINISTRATIVOS/5
        [ResponseType(typeof(GASTOS_ADMINISTRATIVOS))]
        public IHttpActionResult DeleteGASTOS_ADMINISTRATIVOS(decimal id)
        {
            GASTOS_ADMINISTRATIVOS gASTOS_ADMINISTRATIVOS = db.GASTOS_ADMINISTRATIVOS.Find(id);
            if (gASTOS_ADMINISTRATIVOS == null)
            {
                return NotFound();
            }

            db.GASTOS_ADMINISTRATIVOS.Remove(gASTOS_ADMINISTRATIVOS);
            db.SaveChanges();

            return Ok(gASTOS_ADMINISTRATIVOS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GASTOS_ADMINISTRATIVOSExists(decimal id)
        {
            return db.GASTOS_ADMINISTRATIVOS.Count(e => e.ID == id) > 0;
        }
    }
}