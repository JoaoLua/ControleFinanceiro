using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DashBoardUseCases
{
    public class GetResumoFinanceiroUseCase
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaRepository _contaRepository;
        public GetResumoFinanceiroUseCase(ITransacaoRepository transacaoRepository, IContaRepository contaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
        }
        public async Task<ResumoFinanceiroResponse> Execute(string userId)
        {
            var dataAtual = DateTime.Now; ;
            var inicioMes = new DateTime(dataAtual.Year, dataAtual.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            var receita = await _transacaoRepository.GetSomaPorPeriodoAsync(userId, TipoTransacao.Receita, inicioMes, fimMes); 
            var despesa = await _transacaoRepository.GetSomaPorPeriodoAsync(userId, TipoTransacao.Despesa, inicioMes, fimMes);
            var saldoTotal = await _contaRepository.GetSaldoTotalAsync(userId);

            return new ResumoFinanceiroResponse
            {
                ReceitaMensal = receita,
                DespesaMensal = despesa,
                SaldoMensal = receita - despesa,
                SaldoTotal = saldoTotal
            };
        }
    }
}
