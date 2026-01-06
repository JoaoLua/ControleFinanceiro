using Domain.Entities;
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
    public class ContaRepository : IContaRepository
    {
        private readonly AppDbContext _context;

        public ContaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserPossuiContaAsync(string userId)
        {
            return await _context.Contas.AnyAsync(c => c.UserId == userId);
        }
        public async Task<Conta> AddAsync(Conta conta)
        {
            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();
            return conta;
        }
        public async Task<IEnumerable<Conta>> GetAllAsync(string userId)
        {
            return await _context.Contas
                .Where(c => c.UserId == userId || c.UserId == null)
                .ToListAsync();
        }
        public async Task<Conta> GetByIdAsync(int id, string userId)
        {
            return await _context.Contas
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }
        public async Task UpdateAsync(Conta conta)
        {
            _context.Contas.Update(conta);
            await _context.SaveChangesAsync();
        }
        public async Task<decimal> GetSaldoTotalAsync(string userId)
        {
            return await _context.Contas
                .Where(c => c.UserId == userId)
                .SumAsync(c => c.Saldo);
             
        }
    }
}
