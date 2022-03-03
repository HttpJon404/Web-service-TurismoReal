using Minio;
using Minio.DataModel;
using Minio.Exceptions;
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
    public class DepartamentoController : ApiController
    {
        EntitiesTurismo db = new EntitiesTurismo();
        

        //Post: (api/departamento)
        [ResponseType(typeof(string))]

        public async Task<List<string>> PostDepartamento(Departamento departamento)
        {
            List<string> json = new List<string>();

            try
            {
                await Task.Run(async () => {

                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];

                    OracleConnection con = new OracleConnection(constr);
                    await con.OpenAsync();

                    
                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("BEGIN " +
                        "pkgArrayInventarioDpto.CREATE_DPTO(:P_DORMITORIOS, :P_BAÑOS, :P_METROSM2 , :P_ESTACIONAMIENTO" +
                        ", :P_DIRECCION, :P_ID_COMUNA, :P_ID_ESTADO, :P_VALOR_ARRIENDO, :P_CONDICIONES, :P_RESULTADO ,:P_TIPOINVENTARIO, :P_IMAGEN_LINK, :P_ID_DPTO, :P_PORTADA); END; ", con);

                    var dormitorios = cmd.Parameters.Add("P_DORMITORIOS", OracleDbType.Int32);
                    dormitorios.Direction = ParameterDirection.Input;
                    dormitorios.Size = 38;

                    var baños = cmd.Parameters.Add("P_BAÑOS", OracleDbType.Int32);
                    baños.Direction = ParameterDirection.Input;
                    baños.Size = 38;

                    var metrosm2 = cmd.Parameters.Add("P_METROSM2", OracleDbType.Decimal);
                    metrosm2.Direction = ParameterDirection.Input;
                    metrosm2.Size = 38;

                    var estacionamiento = cmd.Parameters.Add("P_ESTACIONAMIENTO", OracleDbType.Int32);
                    estacionamiento.Direction = ParameterDirection.Input;
                    estacionamiento.Size = 38;

                    var direccion = cmd.Parameters.Add("P_DIRECCION", OracleDbType.Varchar2);
                    direccion.Direction = ParameterDirection.Input;
                    direccion.Size = 200;

                    var comuna = cmd.Parameters.Add("P_ID_COMUNA", OracleDbType.Int32);
                    comuna.Direction = ParameterDirection.Input;
                    comuna.Size = 38;

                    var estado = cmd.Parameters.Add("P_ID_ESTADO", OracleDbType.Int32);
                    estado.Direction = ParameterDirection.Input;
                    estado.Size = 38;

                    var valor = cmd.Parameters.Add("P_VALOR_ARRIENDO", OracleDbType.Decimal);
                    valor.Direction = ParameterDirection.Input;
                    valor.Size = 38;

                    var condiciones = cmd.Parameters.Add("P_CONDICIONES", OracleDbType.Varchar2);
                    condiciones.Direction = ParameterDirection.Input;
                    condiciones.Size = 38;

                    var resultado = cmd.Parameters.Add("P_RESULTADO", OracleDbType.Varchar2);
                    resultado.Direction = ParameterDirection.Output;
                    resultado.Size = 200;

                    var lst_tipoInventario = cmd.Parameters.Add("P_TIPOINVENTARIO", OracleDbType.Int32);
                    lst_tipoInventario.Direction = ParameterDirection.Input;
                    lst_tipoInventario.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                    var imagen_link = cmd.Parameters.Add("P_IMAGEN_LINK", OracleDbType.Varchar2);
                    imagen_link.Direction = ParameterDirection.Input;
                    imagen_link.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                    var id_dpto = cmd.Parameters.Add("P_ID_DPTO", OracleDbType.Int32);
                    id_dpto.Direction = ParameterDirection.Output;
                    id_dpto.Size = 200;

                    var portada = cmd.Parameters.Add("P_PORTADA", OracleDbType.Varchar2);
                    portada.Direction = ParameterDirection.Input;
                    portada.Size = 200;

                    ////ejemplo de ruta, deberian ser "n" rutas
                    /// c:\\turismo\\p1.jpg
                    string[] imagePath = departamento.ruta_archivo;
                    

                    dormitorios.Value = departamento.dormitorios;
                    baños.Value = departamento.baños;
                    metrosm2.Value = departamento.metrosm2;
                    estacionamiento.Value = departamento.estacionamiento;
                    direccion.Value = departamento.direccion;
                    comuna.Value = departamento.id_comuna;
                    estado.Value = departamento.id_estado;
                    valor.Value = departamento.valor_arriendo;
                    condiciones.Value = departamento.condiciones;
                    portada.Value = departamento.portada;
                    

                    ///listas array
                    int[] tipoInventario = departamento.tipo_inventario;

                    if (tipoInventario.Length == 0)
                    {
                        tipoInventario = new int[1] { 999 };
                    }

                    lst_tipoInventario.Value = tipoInventario;
                    lst_tipoInventario.Size = 20;
                    lst_tipoInventario.ArrayBindSize = tipoInventario;

                    imagen_link.Value = imagePath;
                    imagen_link.Size = 6;
                    imagen_link.ArrayBindSize = new[] { 200, 200, 200,200,200,200 };

                    

                    



                    //cmd.Parameters.AddRange(new[] { dormitorios, baños,metrosm2,estacionamiento,direccion,comuna,estado,valor,condiciones,resultado,lst_tipoInventario});

                    //cmd.Connection.Open();
                    await cmd.ExecuteNonQueryAsync();                   
                    
                    cmd.Connection.Close();

                    json.Add(resultado.ToString());
                    json.Add(id_dpto.ToString());



                });


                if (json.Count>0)
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

                return new List<string>();
            }

        }

        //Get: (api/departamento)
        [ResponseType(typeof(string))]

        public async Task<string> GetDepartamentos()
        {
            string json = string.Empty;
            try

            {
                await Task.Run( async() => {

                    //Cambiar value de ruta archivos para la conexion de oracle en web.config
                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                    OracleConnection con = new OracleConnection(constr);
                    await con.OpenAsync();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("GET_DEPARTAMENTOS", con);
                    cmd.CommandType = CommandType.StoredProcedure;


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

        [ResponseType(typeof(string))]

        public async Task<string> GetDepartamentoPorId(int id)
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
                    OracleCommand cmd = new OracleCommand("OBTENER_DEPARTAMENTO", con);
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



        // DELETE: api/departamento/5
        [ResponseType(typeof(string))]
        public async Task<List<string>> DeleteDEPTO(Departamento depto)
        {

            List<string> respuesta = new List<string>();

            try
            {
                await Task.Run(() => {
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "DEACTIVATE_DEPTO";


                        var resultado = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, ParameterDirection.Output);
                        resultado.Size = 200;
                        var estado = new OracleParameter("P_ESTADO", OracleDbType.Varchar2, depto.id_estado, ParameterDirection.Input);
                        estado.Size = 1;
                        var id_depto = new OracleParameter("P_ID_DEPTO", OracleDbType.Int32, depto.id, ParameterDirection.Input);
                        id_depto.Size = 200;

                        cmd.Parameters.AddRange(new[] { id_depto, estado, resultado });

                        cmd.Connection.Open();
                        var res = cmd.ExecuteNonQuery();
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

    }
}
