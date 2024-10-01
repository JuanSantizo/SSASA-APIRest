using APIRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System.Web.Http;

using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Routing;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        private IConfiguration _configuration;

        public DepartamentoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection cnn;

        /// <summary>
        /// Metodo para obtener un listado de departamentos
        /// </summary>
        /// <returns></returns>
        // GET: api/<DepartamentoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                List<Departamentos> Ldepartamento = new List<Departamentos>();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Departamento_Listar";
                cmd.Parameters.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Departamentos departamento = new Departamentos();

                    departamento.DepartamentoId = (int)dr["DepartamentoId"];
                    departamento.Nombre = (string)dr["Nombre"];
                    departamento.Estado = (string)dr["Estado"];
                    Ldepartamento.Add(departamento);

                }

                cnn.Close();


                return Ok(Ldepartamento);
            }
            catch (Exception ex)
            {
                cnn.Close();
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Metodo para insertar nuevos Departamento
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        // POST api/<DepartamentoController>
        [HttpPost]
        [Route("Insertar")]
        public async Task<IActionResult> Insertar([FromBody] Departamentos jsonParam)
        {
            try
            {

                string l_Nombre = jsonParam.Nombre.ToString().Trim();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Departamento_Insertar";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_Nombre", l_Nombre));
                dr = cmd.ExecuteReader();

                cnn.Close();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        /// <summary>
        /// Metodo para actualizar Departamentos existentes
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        // POST api/<DepartamentoController>
        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Departamentos jsonParam)
        {
            try
            {

                int l_DepartamentoId = jsonParam.DepartamentoId;
                string l_Nombre = jsonParam.Nombre.ToString().Trim();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Departamento_Actualizar";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_DepartamentoId", l_DepartamentoId));
                cmd.Parameters.Add(new SqlParameter("p_Nombre", l_Nombre));
                dr = cmd.ExecuteReader();

                cnn.Close();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Metodo para Desactivar Departamentos existentes
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        // POST api/<DepartamentoController>
        [HttpPost]
        [Route("Desactivar")]
        public async Task<IActionResult> Desactivar([FromBody] Departamentos jsonParam)
        {
            try
            {

                int l_DepartamentoId = jsonParam.DepartamentoId;                

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Departamento_Desactivar";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_DepartamentoId", l_DepartamentoId));                
                dr = cmd.ExecuteReader();

                cnn.Close();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Metodo para seleccionar un departamento en especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<DepartamentoController>/5
        [HttpGet("{id}")]
        public IActionResult  Get(int id)
        {
            try
            {
                int l_DepartamentoId = id;

                List<Departamentos> Ldepartamento = new List<Departamentos>();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Departamento_Seleccionar";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_DepartamentoId", l_DepartamentoId));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Departamentos departamento = new Departamentos();

                    departamento.DepartamentoId = (int)dr["DepartamentoId"];
                    departamento.Nombre = (string)dr["Nombre"];
                    departamento.Estado = (string)dr["Estado"];
                    Ldepartamento.Add(departamento);

                }

                cnn.Close();


                return Ok(Ldepartamento);
            }
            catch (Exception ex)
            {
                cnn.Close();
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Metodo para desactivar empleados segun el Departamento desactivado
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        // POST api/<DepartamentoController>
        [HttpPost]
        [Route("DesactivarEmpleados")]
        public async Task<IActionResult> DesactivarEmpleados([FromBody] Departamentos jsonParam)
        {
            try
            {

                int l_DepartamentoId = jsonParam.DepartamentoId;

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_DepartamentoEmpleados_Desactivar";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_DepartamentoId", l_DepartamentoId));
                dr = cmd.ExecuteReader();

                cnn.Close();


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



    }
}
