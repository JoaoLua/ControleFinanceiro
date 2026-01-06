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
    public class GetAllContasUseCase
    {
        private readonly IContaRepository _repository;
        
        public GetAllContasUseCase(IContaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ContaResponse>> Execute(string userId)
        {
            var possuiContas = await _repository.UserPossuiContaAsync(userId);
            if (!possuiContas)
            {
                var carteira = new Conta
                {
                    Nome = "Carteira",
                    Saldo = 0,
                    UserId = userId
                };

                await _repository.AddAsync(carteira);
            }
            var contas = await _repository.GetAllAsync(userId);
            return contas.Select(c => new ContaResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Saldo = c.Saldo
            });
        }
    }
}
