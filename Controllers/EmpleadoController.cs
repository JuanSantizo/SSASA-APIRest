using APIRest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private IConfiguration _configuration;

        public EmpleadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection cnn;


        /// <summary>
        /// Metodo para obtener un listado de empleados
        /// </summary>
        /// <returns></returns>
        // GET: api/<EmpleadoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                List<Empleados> LEmpleado = new List<Empleados>();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Empleados_Listar";
                cmd.Parameters.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Empleados Empleado = new Empleados();

                    Empleado.DPI = (string)dr["DPI"];
                    Empleado.Nombres = (string)dr["Nombres"];
                    Empleado.Apellidos = (string)dr["Apellidos"];
                    Empleado.FechaNacimiento = (DateTime)dr["FechaNacimiento"];
                    Empleado.SexoId = (int)dr["SexoId"];
                    Empleado.Fecha_Ingreso = (DateTime)dr["Fecha_Ingreso"];
                    Empleado.Edad = (int)dr["Edad"];
                    Empleado.Direccion = (string)dr["Direccion"];
                    Empleado.NIT = (string)dr["NIT"];
                    Empleado.DepartamentoId = (int)dr["DepartamentoId"];
                    Empleado.Estado = (string)dr["Estado"];
                    

                    LEmpleado.Add(Empleado);

                }

                cnn.Close();


                return Ok(LEmpleado);
            }
            catch (Exception ex)
            {
                cnn.Close();
                return BadRequest(ex.Message);
            }
        }





        /// <summary>
        /// Metodo para insertar nuevos Empleados
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        // POST api/<EmpleadoController>
        [HttpPost]
        [Route("Insertar")]
        public async Task<IActionResult> Insertar([FromBody] Empleados jsonParam)
        {
            try
            {

                string l_DPI = jsonParam.DPI.ToString().Trim();
                string l_Nombres = jsonParam.Nombres.ToString().Trim();
                string l_Apellidos = jsonParam.Apellidos.ToString().Trim();
                DateTime l_FechaNacimiento = (DateTime)jsonParam.FechaNacimiento;
                int l_SexoId = (int)jsonParam.SexoId;
                DateTime l_Fecha_Ingreso = (DateTime)jsonParam.Fecha_Ingreso;
                int l_Edad = (int)jsonParam.Edad;
                string l_Direccion = jsonParam.Direccion.ToString().Trim();
                string l_NIT = jsonParam.NIT.ToString().Trim();
                int l_DepartamentoId = (int)jsonParam.DepartamentoId;

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Empleados_Insertar";
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("p_DPI", l_DPI));
                cmd.Parameters.Add(new SqlParameter("p_Nombres", l_Nombres));
                cmd.Parameters.Add(new SqlParameter("p_Apellidos", l_Apellidos));
                cmd.Parameters.Add(new SqlParameter("p_FechaNacimiento", l_FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("p_SexoId", l_SexoId));
                cmd.Parameters.Add(new SqlParameter("p_Fecha_Ingreso", l_Fecha_Ingreso));
                cmd.Parameters.Add(new SqlParameter("p_Edad", l_Edad));
                cmd.Parameters.Add(new SqlParameter("p_Direccion", l_Direccion));
                cmd.Parameters.Add(new SqlParameter("p_NIT", l_NIT));
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
        /// Metodo para actualizar un Empleado
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        // POST api/<EmpleadoController>
        [HttpPost]
        [Route("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Empleados jsonParam)
        {
            try
            {

                string l_DPI = jsonParam.DPI.ToString().Trim();
                string l_Nombres = jsonParam.Nombres.ToString().Trim();
                string l_Apellidos = jsonParam.Apellidos.ToString().Trim();
                DateTime l_FechaNacimiento = (DateTime)jsonParam.FechaNacimiento;
                int l_SexoId = (int)jsonParam.SexoId;
                DateTime l_Fecha_Ingreso = (DateTime)jsonParam.Fecha_Ingreso;
                int l_Edad = (int)jsonParam.Edad;
                string l_Direccion = jsonParam.Direccion.ToString().Trim();
                string l_NIT = jsonParam.NIT.ToString().Trim();
                int l_DepartamentoId = (int)jsonParam.DepartamentoId;


                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Empleados_Actualizar";
                cmd.Parameters.Clear();

                cmd.Parameters.Add(new SqlParameter("p_DPI", l_DPI));
                cmd.Parameters.Add(new SqlParameter("p_Nombres", l_Nombres));
                cmd.Parameters.Add(new SqlParameter("p_Apellidos", l_Apellidos));
                cmd.Parameters.Add(new SqlParameter("p_FechaNacimiento", l_FechaNacimiento));
                cmd.Parameters.Add(new SqlParameter("p_SexoId", l_SexoId));
                cmd.Parameters.Add(new SqlParameter("p_Fecha_Ingreso", l_Fecha_Ingreso));
                cmd.Parameters.Add(new SqlParameter("p_Edad", l_Edad));
                cmd.Parameters.Add(new SqlParameter("p_Direccion", l_Direccion));
                cmd.Parameters.Add(new SqlParameter("p_NIT", l_NIT));
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
        /// Metodo para desactivar un Empleado
        /// </summary>
        /// <param name="jsonParam"></param>
        /// <returns></returns>
        // POST api/<EmpleadoController>
        [HttpPost]
        [Route("Desactivar")]
        public async Task<IActionResult> Desactivar([FromBody] Empleados jsonParam)
        {
            try
            {

                string l_DPI = jsonParam.DPI.ToString().Trim();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Empleados_Desactivar";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_DPI", l_DPI));
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
        /// Metodo para seleccionar un Empleado en especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<EmpleadoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                string l_DPI = id.ToString().Trim();

                List<Empleados> LEmpleado = new List<Empleados>();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Empleados_Seleccionar";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_DPI", l_DPI));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Empleados Empleado = new Empleados();

                    Empleado.DPI = (string)dr["DPI"];
                    Empleado.Nombres = (string)dr["Nombres"];
                    Empleado.Apellidos = (string)dr["Apellidos"];
                    Empleado.FechaNacimiento = (DateTime)dr["FechaNacimiento"];
                    Empleado.SexoId = (int)dr["SexoId"];
                    Empleado.Fecha_Ingreso = (DateTime)dr["Fecha_Ingreso"];
                    Empleado.Edad = (int)dr["Edad"];
                    Empleado.Direccion = (string)dr["Direccion"];
                    Empleado.NIT = (string)dr["NIT"];
                    Empleado.DepartamentoId = (int)dr["DepartamentoId"];
                    Empleado.Estado = (string)dr["Estado"];

                    LEmpleado.Add(Empleado);

                }

                cnn.Close();


                return Ok(LEmpleado);
            }
            catch (Exception ex)
            {
                cnn.Close();
                return BadRequest(ex.Message);
            }
        }




    }
}
