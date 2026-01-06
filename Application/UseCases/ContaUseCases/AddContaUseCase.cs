using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ContaUseCases
{
    public class AddContaUseCase 
    {
        private readonly IContaRepository _repository;

        public AddContaUseCase(IContaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContaResponse> Execute(ContaRequest request, string userId)
        {
            var conta = new Conta
            {
                Nome = request.Nome,
                Saldo = request.Saldo,
                UserId = userId,
                Transacoes = new List<Transacao>()

            };
            var createdConta =  await _repository.AddAsync(conta);
            return new ContaResponse
            {
                Id = createdConta.Id,
                Nome = createdConta.Nome,
                Saldo = createdConta.Saldo
            };

        }
    }
}
