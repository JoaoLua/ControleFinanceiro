using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ContaRequest
    {
        [Required(ErrorMessage = "O nome da conta é obrigatório.")]
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
    }
}
