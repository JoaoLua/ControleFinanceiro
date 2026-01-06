using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ResumoFinanceiroResponse
    {
        public decimal ReceitaMensal { get; set; }
        public decimal DespesaMensal { get; set; }
        public decimal SaldoTotal { get; set; }
        public decimal SaldoMensal { get; set; }
    }
}
