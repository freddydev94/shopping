using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class City
    {
        
        public int Id { get; set; }

        [Display(Name = "Ciudad/Municipio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]

        public string Nombre { get; set; }

        [Required]
        public int statesId { get; set; }

        
        public State states { get; set; }

        public ICollection<User> Usuarios { get; set; }

    }
}

