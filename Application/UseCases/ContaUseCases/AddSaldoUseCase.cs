using Domain.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ContaUseCases
{
    public class AddSaldoUseCase
    {
        private readonly IContaRepository _repository;
        public AddSaldoUseCase(IContaRepository repository)
        {
            _repository = repository;
        }
        public async Task<ContaResponse?> Execute(int contaId, AdicionarSaldoRequest request, string userId)
        {
            var conta = await _repository.GetByIdAsync(contaId, userId);

            if (conta== null) 
                return null;

            conta.Saldo += request.Valor;

            return new ContaResponse
            {
                Id = conta.Id,
                Nome = conta.Nome,
                Saldo = conta.Saldo
            };

        }
    }
}
