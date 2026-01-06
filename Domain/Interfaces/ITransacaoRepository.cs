using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task AddAsync(Transacao transacao);
        Task<IEnumerable<Transacao>> GetAllAsync(
                    string userId,
                    TipoTransacao? tipo = null,
                    DateTime? data = null,
                    int? contaId = null,
                    int? categoriaId = null);

        Task<decimal> GetSomaPorPeriodoAsync(
            string userId,
            TipoTransacao tipo,
            DateTime dataInicio,
            DateTime dataFim);

        Task<IEnumerable<Transacao>> GetRecentesAsync(
            string userId,
            int quantidade);
    }
}
