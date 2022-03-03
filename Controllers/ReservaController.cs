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
    //POST: api/reserva
    public class ReservaController : ApiController
    {
        [ResponseType(typeof(string))]

        public async Task<List<string>> PostReserva(Reserva reserva)
        {
            List<string> json = new List<string>();

            try
            {
                await Task.Run(async () =>
                {

                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];

                    OracleConnection con = new OracleConnection(constr);
                    await con.OpenAsync();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("BEGIN " +
                        "PKGCREAR_RESERVA.CREAR_RESERVA(:P_ID_USUARIO, :P_ID_DPTO, :P_RUT_ACOMPA, :P_NOMBRE_ACOMPA, :P_APELLIDO_ACOMPA" +
                        ", :P_CELULAR, :P_TIPO_SERVICIO, :P_RESULTADO, :P_ID_DETALLE, :P_FECHA_LLEGADA, :P_FECHA_SALIDA, :P_PAGO); END; ", con);

                    

                    var id_usuario = cmd.Parameters.Add("P_ID_USUARIO", OracleDbType.Int32);
                    id_usuario.Direction = ParameterDirection.Input;
                    id_usuario.Size = 38;

                    var id_dpto = cmd.Parameters.Add("P_ID_DPTO", OracleDbType.Int32);
                    id_dpto.Direction = ParameterDirection.Input;
                    id_dpto.Size = 38;

                    var lst_rut_acompa = cmd.Parameters.Add("P_RUT_ACOMPA", OracleDbType.Varchar2);
                    lst_rut_acompa.Direction = ParameterDirection.Input;
                    lst_rut_acompa.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                    var lst_nombre_acompa = cmd.Parameters.Add("P_NOMBRE_ACOMPA", OracleDbType.Varchar2);
                    lst_nombre_acompa.Direction = ParameterDirection.Input;
                    lst_nombre_acompa.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                    var lst_apellido_acompa = cmd.Parameters.Add("P_APELLIDO_ACOMPA", OracleDbType.Varchar2);
                    lst_apellido_acompa.Direction = ParameterDirection.Input;
                    lst_apellido_acompa.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                    var lst_celular = cmd.Parameters.Add("P_CELULAR", OracleDbType.Varchar2);
                    lst_celular.Direction = ParameterDirection.Input;
                    lst_celular.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                    var lst_tipo_servicio = cmd.Parameters.Add("P_TIPO_SERVICIO", OracleDbType.Int32);
                    lst_tipo_servicio.Direction = ParameterDirection.Input;
                    lst_tipo_servicio.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                    var resultado = cmd.Parameters.Add("P_RESULTADO", OracleDbType.Varchar2);
                    resultado.Direction = ParameterDirection.Output;
                    resultado.Size = 200;

                    var id_detalle = cmd.Parameters.Add("P_ID_DETALLE", OracleDbType.Int32);
                    id_detalle.Direction = ParameterDirection.Output;
                    id_detalle.Size = 38;

                    var fecha_llegada = cmd.Parameters.Add("P_FECHA_LLEGADA", OracleDbType.Date);
                    fecha_llegada.Direction = ParameterDirection.Input;
                    fecha_llegada.Size = 100;

                    var fecha_salida = cmd.Parameters.Add("P_FECHA_SALIDA", OracleDbType.Date);
                    fecha_salida.Direction = ParameterDirection.Input;
                    fecha_salida.Size = 100;

                    var pago = cmd.Parameters.Add("P_PAGO", OracleDbType.Varchar2);
                    pago.Direction = ParameterDirection.Input;
                    pago.Size = 300;

                    var listaVacia = "[]";

                    id_usuario.Value = reserva.id_usuario;
                    id_dpto.Value = reserva.id_dpto;


                    List<DetalleAcompanante> detalleDes = new List<DetalleAcompanante>();

                    string[] rut;
                    string[] nombre;
                    string[] apellido;
                    string[] celular;

                    if (reserva.listaAcompanante != listaVacia)
                    {

                        detalleDes = JsonConvert.DeserializeObject<List<DetalleAcompanante>>(reserva.listaAcompanante);
                        rut = new string[detalleDes.Count];
                        nombre = new string[detalleDes.Count];
                        apellido = new string[detalleDes.Count];
                        celular = new string[detalleDes.Count];

                    }
                    else
                    {
                        reserva.listaAcompanante = "NOK";
                        rut = new string[1];
                        nombre = new string[1];
                        apellido = new string[1];
                        celular = new string[1];
                    }


                    if (detalleDes.Count > 0 && reserva.listaAcompanante != "NOK")
                    {
                        for (var i = 0; i < detalleDes.Count; i++)
                        {
                            rut[i] = detalleDes[i].rut;
                            nombre[i] = detalleDes[i].nombre;
                            apellido[i] = detalleDes[i].apellido;
                            celular[i] = detalleDes[i].celular;

                        }

                        lst_rut_acompa.Value = rut;
                        lst_rut_acompa.Size = 6;
                        lst_rut_acompa.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };


                        lst_nombre_acompa.Value = nombre;
                        lst_nombre_acompa.Size = 6;
                        lst_nombre_acompa.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };

                        lst_apellido_acompa.Value = apellido;
                        lst_apellido_acompa.Size = 6;
                        lst_apellido_acompa.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };

                        lst_celular.Value = celular;
                        lst_celular.Size = 6;
                        lst_celular.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };
                    }
                    else
                    {

                        rut[0] = "NOK";
                        nombre[0] = "NOK";
                        apellido[0] = "NOK";
                        celular[0] = "NOK";

                        lst_rut_acompa.Value = rut;
                        lst_rut_acompa.Size = 6;
                        lst_rut_acompa.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };


                        lst_nombre_acompa.Value = nombre;
                        lst_nombre_acompa.Size = 6;
                        lst_nombre_acompa.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };

                        lst_apellido_acompa.Value = apellido;
                        lst_apellido_acompa.Size = 6;
                        lst_apellido_acompa.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };

                        lst_celular.Value = celular;
                        lst_celular.Size = 6;
                        lst_celular.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };
                    }


                    List<DetalleServicio> detalleServ = new List<DetalleServicio>();
                    decimal[] IdServicio;
                    if (reserva.listaServicios != listaVacia)
                    {
                        detalleServ = JsonConvert.DeserializeObject<List<DetalleServicio>>(reserva.listaServicios);
                        IdServicio = new decimal[detalleServ.Count];
                    }
                    else
                    {
                        IdServicio = new decimal[1];
                        reserva.listaServicios = "NOK";
                    }



                    if (detalleServ.Count > 0 && reserva.listaServicios != "NOK")
                    {
                        for (var i = 0; i < detalleServ.Count; i++)
                        {
                            IdServicio[i] = detalleServ[i].id;
                        }
                        lst_tipo_servicio.Value = IdServicio;
                        lst_tipo_servicio.Size = 10;
                        lst_tipo_servicio.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };
                    }
                    else
                    {
                        //Sin servicio extra el id numero 999
                        IdServicio[0] = 999;
                        lst_tipo_servicio.Value = IdServicio;
                        lst_tipo_servicio.Size = 10;
                        lst_tipo_servicio.ArrayBindSize = new[] { 200, 200, 200, 200, 200, 200 };
                    }


                    fecha_llegada.Value = reserva.fecha_llegada;
                    fecha_salida.Value = reserva.fecha_salida;
                    pago.Value = reserva.pago;


                    await cmd.ExecuteNonQueryAsync();

                    cmd.Connection.Close();

                    json.Add(resultado.Value.ToString());
                    json.Add(id_dpto.Value.ToString());



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
            catch (Exception e)
            {

                return new List<string>();
            }

        }

        //GET: api/reserva/45
        [ResponseType(typeof(string))]
        public async Task<string> GetReservaID(decimal id)
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
                    OracleCommand cmd = new OracleCommand("GET_DETALLE_RESERVA", con);
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


        // PUT: api/reserva/5
        [ResponseType(typeof(string))]
        public async Task<string> PutReserva(CancelarReserva cancelar)
        {
            string json = string.Empty;
            try
            {
                await Task.Run(async () =>
                {

                    string constr = ConfigurationManager.AppSettings["RutaArchivosConexion"];
                    OracleConnection con = new OracleConnection(constr);
                    await con.OpenAsync();

                    // Crea comando oracle
                    OracleCommand cmd = new OracleCommand("DELETE_RESERVA", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter id_detalle =
                        cmd.Parameters.Add("P_ID_DETALLE", OracleDbType.Int32, cancelar.id, ParameterDirection.Input);
                    id_detalle.Size = 38;

                    var resultado = cmd.Parameters.Add("P_RESULTADO", OracleDbType.Varchar2);
                    resultado.Direction = ParameterDirection.Output;
                    resultado.Size = 200;

                    // Ejecuta comando
                    cmd.ExecuteNonQuery();

                    //// leer data del cursor
                    //cmd.Parameters.AddRange(new[] { id_detalle, resultado });

                    //cmd.Connection.Open();
                    //var res = cmd.ExecuteNonQuery();
                    //cmd.Connection.Close();
                    cmd.Dispose();

                    con.Close();
                    con.Dispose();

                    json = resultado.Value.ToString();
                    

                   

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


    }
}
