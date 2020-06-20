using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Zurich.Models
{
    
    public class SeguroDtos
    {
        public int VeiculoRefId { get; set; }
        public int SeguradoRefId { get; set; }
    }
}