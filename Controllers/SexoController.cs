using APIRest.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIRest.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class SexoController : ControllerBase
    {
        private IConfiguration _configuration;

        public SexoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection cnn;

        /// <summary>
        /// Metodo para obtener los sexos definidos
        /// </summary>
        /// <returns></returns>
        // GET: api/<SexoController>
        [HttpGet]
        public IActionResult  Get()
        {
            try {

                 List<Sexos> Lsexo = new List<Sexos>();

                string l_Cadena = _configuration.GetValue<string>("ConnectionStrings:Connection");                
                cnn = new SqlConnection(l_Cadena);               
                SqlCommand cmd = cnn.CreateCommand();
                cnn.Open();
                SqlDataReader dr = null;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Sexo_Listar";
                cmd.Parameters.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Sexos sexo = new Sexos();

                    sexo.SexoId = (int)dr["SexoId"];
                    sexo.Descripcion = (string)dr["Descripcion"];
                    Lsexo.Add(sexo);

                }

                cnn.Close();


                return Ok(Lsexo);
            }
            catch (Exception ex) {
                cnn.Close();
                return BadRequest(ex.Message);
            }            
        }
    }
}
