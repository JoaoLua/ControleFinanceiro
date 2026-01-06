using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.TransacaoUseCases
{
    public class GetAllTransacoesUseCase
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public GetAllTransacoesUseCase(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<IEnumerable<TransacaoResponse>> Execute(
            string userId,
            TipoTransacao? tipo = null,
            DateTime? data = null,
            int? contaId = null,
            int? categoriaId = null)
        {
            var transacoes = await _transacaoRepository.GetAllAsync(userId, tipo, data, contaId, categoriaId);
            return transacoes.Select(t => new TransacaoResponse
            {
                Id = t.Id,
                Tipo = t.Tipo.ToString(), 
                Valor = t.Valor,
                Descricao = t.Descricao ?? string.Empty,
                Data = t.Data,
                ContaId = t.ContaId,
                CategoriaId = t.CategoriaId,
                CategoriaNome = t.Categoria != null ? t.Categoria.Nome : "Sem Categoria"
            });
        }
    }
}
