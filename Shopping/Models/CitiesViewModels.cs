using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CitiesViewModels
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad/Municipio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Nombre { get; set; }


        public int statesId { get; set; }
    }
}
