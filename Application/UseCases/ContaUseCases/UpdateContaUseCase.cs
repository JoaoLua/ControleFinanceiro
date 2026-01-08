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
    public class UpdateContaUseCase
    {
        private readonly IContaRepository _contaRepository;

        public UpdateContaUseCase(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }
        public async Task<ContaResponse?> Execute(string userId, int contaId, ContaRequest request)
        {
            var contaUpdate = new Conta
            {
                Id = contaId,
                Nome = request.Nome,
                Saldo = request.Saldo
            };

            var result = await _contaRepository.UpdateAsync(userId, contaId, contaUpdate);

            if (result is null)
                return null;

            return new ContaResponse
            {
                Id = result.Id,
                Nome = result.Nome,
                Saldo = result.Saldo
            };
        }
    }
}
