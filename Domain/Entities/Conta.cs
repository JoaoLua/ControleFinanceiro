using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Conta
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal Saldo { get; set; } = 0;
        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }
        public virtual ICollection<Transacao>? Transacoes { get; set; }

    }
}
