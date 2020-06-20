using System.ComponentModel.DataAnnotations;
using WebAPI.Zurich.Util;

namespace WebAPI.Zurich.Models
{
    public class Segurado
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do segurado é obrigatório !")]
        [MinLength(6),MaxLength(50)]

        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF do segurado é obrigatório !")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        [MinLength(11),MaxLength(11)]
        public string CPF { get; set; }

        ///[MinLength(1), MaxLength(3)]
        [Required(ErrorMessage = "A idade do segurado é obrigatório !")]
        public int Idade { get; set; }
    }
}