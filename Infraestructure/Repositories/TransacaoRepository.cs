using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;
        public TransacaoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Transacao transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Transacao>> GetAllAsync(
            string userId,
            TipoTransacao? tipo = null,
            DateTime? data = null,
            int? contaId = null,
            int? categoriaId = null)
        {
            var query = _context.Transacoes
                                 .AsNoTracking()
                                 .Include(t => t.Categoria)
                                 .Where(t => t.UserId == userId);

            if (tipo.HasValue)
            {
                query = query.Where(t => t.Tipo == tipo.Value);
            }

            if (data.HasValue)
            {
                query = query.Where(t => t.Data.Date == data.Value.Date);
            }

            if (contaId.HasValue)
            {
                query = query.Where(t => t.ContaId == contaId.Value);
            }

            if (categoriaId.HasValue)
            {
                query = query.Where(t => t.CategoriaId == categoriaId.Value);
            }

            return await query.OrderByDescending(t => t.Data).ToListAsync();
        }

        public async Task<decimal> GetSomaPorPeriodoAsync(
            string userId,
            TipoTransacao tipo,
            DateTime dataInicio,
            DateTime dataFim)
        {

            var dataFimAjustada = dataFim.Date.AddDays(1).AddTicks(-1);
            return await _context.Transacoes
                                    .Where(t => t.UserId == userId &&
                                        t.Tipo == tipo &&
                                        t.Data >= dataInicio &&
                                        t.Data <= dataFimAjustada)
                                        .SumAsync(t => t.Valor);
        }

        public async Task<IEnumerable<Transacao>> GetRecentesAsync(
            string userId,
            int quantidade)
        {
            return await _context.Transacoes
                                 .AsNoTracking()
                                 .Include(t => t.Categoria)
                                 .Where(t => t.UserId == userId)
                                 .OrderByDescending(t => t.Data)
                                 .Take(quantidade)
                                 .ToListAsync();
        }
    }
}
