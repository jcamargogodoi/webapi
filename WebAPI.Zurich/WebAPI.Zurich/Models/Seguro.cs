using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Zurich.Models
{
    public class Seguro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Valor do veiculo é obrigatório !")]
        public double ValorSeguro { get; set; }

        public double TaxaRisco { get; set; }
        public double PremioRisco { get; set; }
        public double PremioPuro { get; set; }
        public double PremioComercial { get; set; }

        [Required(ErrorMessage = "Id do veículo é obrigatório !")]
        [ForeignKey("Veiculo")]

        public int VeiculoRefId { get; set; }
        public Veiculo Veiculo { get; set; }

        [Required(ErrorMessage = "Id do segurado é obrigatório !")]
        [ForeignKey("Segurado")]
        public int SeguradoRefId { get; set; }
        public Segurado Segurado{get; set;}
    }
}