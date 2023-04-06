using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class Country
    {
        [Key]

        public int Id { get; set; }

        [Display(Name ="Pais")]
        [Required (ErrorMessage ="El campo {0} es obligatorio" )]
        [MaxLength(40, ErrorMessage ="El campo {} debe tener maximo {1} caracteres")]
        public string Nombre { get; set; }
    }
}
