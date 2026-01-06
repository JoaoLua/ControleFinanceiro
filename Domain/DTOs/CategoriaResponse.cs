using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? CorHexadecimal { get; set; }
        public CategoriaResponse(int id, string? nome)
        {
            Id = id;
            Nome = nome;
        }
        public CategoriaResponse(int id, string? nome, string? descricao, string? corHexadecimal)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            CorHexadecimal = corHexadecimal;
        }
    }
}
