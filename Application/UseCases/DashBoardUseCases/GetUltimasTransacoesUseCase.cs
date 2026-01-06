using Domain.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DashBoardUseCases
{
    public class GetUltimasTransacoesUseCase
    {
        private readonly ITransacaoRepository _repository;
        public GetUltimasTransacoesUseCase(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TransacaoResponse>> Execute(string userID, int quantidade)
        {
            var transacoes = await _repository.GetRecentesAsync(userID, quantidade);

            return transacoes.Select(t => new TransacaoResponse
            {
                Id = t.Id,
                Tipo = t.Tipo.ToString(),
                Valor = t.Valor,
                Descricao = t.Descricao ?? string.Empty,
                Data = t.Data,
                ContaId = t.ContaId,
                CategoriaId = t.CategoriaId,
                CategoriaNome = t.Categoria?.Nome ?? "Sem Categoria"
            });
        }
    }
}
