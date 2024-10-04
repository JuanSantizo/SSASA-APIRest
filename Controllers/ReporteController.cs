using APIRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {


        private IConfiguration _configuration;

        public ReporteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection cnn;


        // GET: api/<ReporteController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReporteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReporteController>
        [HttpPost]
        [Route("Reporte")]
        public IActionResult Reporte([FromBody] Reporte jsonParam)
        {
            try
            {

                List<Reporte> LReporte = new List<Reporte>();


                int l_DepartamentoId = int.Parse(jsonParam.DepartamentoId.ToString());
                string l_EstadoId = jsonParam.EstadoId.ToString();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");
                cnn = new SqlConnection(l_Cadena);
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Reporte";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("p_Departamento", l_DepartamentoId));
                cmd.Parameters.Add(new SqlParameter("p_Estado", l_EstadoId));
                dr = cmd.ExecuteReader();



                while (dr.Read())
                {
                    Reporte itemReporte = new Reporte();

                    itemReporte.DPI = (string)dr["DPI"];
                    itemReporte.Nombres = (string)dr["Nombres"];
                    itemReporte.Apellidos = (string)dr["Apellidos"];
                    itemReporte.Sexo = (string)dr["Sexo"];
                    itemReporte.Departamento = (string)dr["Departamento"];
                    itemReporte.Estado = (string)dr["Estado"];
                    itemReporte.EstadoId = (string)dr["EstadoId"];
                    itemReporte.DepartamentoId = (int)dr["DepartamentoId"];

                    LReporte.Add(itemReporte);

                }



                cnn.Close();


                return Ok(LReporte);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }




        }

        // PUT api/<ReporteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReporteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
