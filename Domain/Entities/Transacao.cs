using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transacao
    {
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
        public string? Descricao { get; set; }
        public int ContaId { get; set; }
        public virtual Conta? Conta { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual IdentityUser? User { get; set; }
    }
}
