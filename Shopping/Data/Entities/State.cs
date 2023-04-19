using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Nombre { get; set; }

        
        public int countryId { get; set; }
        public Country country { get; set; }

        public ICollection<City> cities { get; set; }

        [Display(Name = "Ciudades")]
        public int CityCount => cities == null ? 0 : cities.Count;
    }
}
