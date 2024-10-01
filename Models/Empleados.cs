using APIRest.Models;
using System.Runtime.Intrinsics.Arm;

namespace APIRest.Models
{
    public class Empleados
    {
        public string? DPI { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? SexoId { get; set; }
        public string? Sexo {  get; set; }
        public DateTime? Fecha_Ingreso { get; set; }
        public int? Edad { get; set; }
        public string? Direccion { get; set; }
        public string? NIT { get; set; }
        public int? DepartamentoId { get; set; }
        public string? Departamento { get; set; }
        public string? Estado { get; set; }
        public DateTime? Fecha_Cambio_Estado { get; set; }
    }
}
