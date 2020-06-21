using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Zurich.Models
{
    public class SeguroMediaDtos
    {   
        public int CodigoSegurado { get; set; }
        public string Segurado { get; set; }
        public double MediaSeguro { get; set; }
        public int QtdeSeguro { get; set; }
    }
}