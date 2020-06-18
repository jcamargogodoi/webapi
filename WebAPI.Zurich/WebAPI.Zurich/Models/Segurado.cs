using System.ComponentModel.DataAnnotations;

namespace WebAPI.Zurich.Models
{
    public class Segurado
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do segurado é obrigatório !")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF do segurado é obrigatório !")]
        public double CPF { get; set; }

        [Required(ErrorMessage = "A idade do segurado é obrigatório !")]
        public int Idade { get; set; }
    }
}