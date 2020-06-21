using System.ComponentModel.DataAnnotations;

namespace WebAPI.Zurich.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Marca e modelo é obrigatório !")]
        public string MarcaModelo { get; set; }

        [Required(ErrorMessage = "Valor do veículo é obrigatório !")]
        public double  valor { get; set; }
    }
}