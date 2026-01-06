using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CategoriaRequest
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? CorHexadecimal { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
