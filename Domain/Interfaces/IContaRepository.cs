using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IContaRepository
    {
        Task<bool> UserPossuiContaAsync(string userId);
        Task<Conta> AddAsync(Conta conta);
        Task<IEnumerable<Conta>> GetAllAsync(string userId);
        Task<Conta> GetByIdAsync(int id, string userId);
        Task UpdateAsync(Conta conta);
        Task<decimal> GetSaldoTotalAsync(string userId);
        Task<Conta?> UpdateAsync(string userId, int contaId, Conta updateConta);
    }
}
