using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.TransacaoUseCases
{
    public class AddTransacaoUseCase
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaRepository _contaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        public AddTransacaoUseCase(
                ITransacaoRepository transacaoRepository,
                IContaRepository contaRepository,
                ICategoriaRepository categoriaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<TransacaoResponse?> Execute(TransacaoRequest request, string userId)
        {
            var conta = await _contaRepository.GetByIdAsync(request.ContaId, userId);
            if (conta == null)
                throw new Exception("Conta inválida.");

            var categoria = await _categoriaRepository.GetByIdAsync(request.CategoriaId, userId);
            if (categoria == null)
                throw new Exception("Categoria inválida ou não pertence ao usuário.");

            var transacaoEntity = new Transacao
            {
                Tipo = request.Tipo, 
                Valor = request.Valor,
                Descricao = request.Descricao,
                Data = DateTime.Now,
                ContaId = request.ContaId,
                CategoriaId = request.CategoriaId,
                UserId = userId
            };

            if (transacaoEntity.Tipo == TipoTransacao.Receita)
            {
                conta.Saldo += transacaoEntity.Valor;
            }
            else
            {
                conta.Saldo -= transacaoEntity.Valor;
            }

            await _transacaoRepository.AddAsync(transacaoEntity);
            await _contaRepository.UpdateAsync(conta);

            return new TransacaoResponse
            {
                Id = transacaoEntity.Id, 
                Tipo = transacaoEntity.Tipo.ToString(), 
                Valor = transacaoEntity.Valor,
                Descricao = transacaoEntity.Descricao ?? string.Empty,
                Data = transacaoEntity.Data,
                ContaId = transacaoEntity.ContaId,
                CategoriaId = transacaoEntity.CategoriaId,
                CategoriaNome = categoria.Nome 
            };
        }
    }
}
