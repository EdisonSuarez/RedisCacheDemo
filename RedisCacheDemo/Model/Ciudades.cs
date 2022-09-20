using System.ComponentModel.DataAnnotations;

namespace RedisCacheDemo.Model
{
    public class Ciudades
    {
        [Key]
        public int IdCiudad { get; set; }
        public string IdDepartamento { get; set; }
        public string NombreCiudad { get; set; }
    }
}
