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

        // GET: api/<SexoController>
        [HttpGet]
        public IActionResult  Get()
        {
            try {

                 List<Sexo> Lsexo = new List<Sexo>();

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
                    Sexo sexo = new Sexo();

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

        // GET api/<SexoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SexoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SexoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SexoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
       


    }
}
