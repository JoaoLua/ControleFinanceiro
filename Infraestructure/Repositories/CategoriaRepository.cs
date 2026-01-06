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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Categoria>> GetAllAsync(string userId)
        { 
            return await _context.Categorias
                .Where(c => c.UserId == userId || c.UserId == null)
                .ToListAsync();
        }
        public async Task<Categoria?> GetByIdAsync(int id, string userId)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id && (c.UserId == userId || c.UserId == null));
        }
        public async Task<IEnumerable<Categoria>> DeleteAsync(string userId, int categoriaId)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == categoriaId && c.UserId == userId);
            if (categoria == null)
                throw new KeyNotFoundException("Categoria not found or does not belong to the user.");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        
            return await GetAllAsync(userId);
        }
        public async Task<Categoria?> UpdateAsync(string userId, int categoriaId, Categoria updateCategoria)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == categoriaId && c.UserId == userId);
            if (categoria == null)
                throw new KeyNotFoundException("Categoria não coincide com usuario");

            categoria.Nome = updateCategoria.Nome;
            categoria.Descricao = updateCategoria.Descricao;
            categoria.CorHexadecimal = updateCategoria.CorHexadecimal;

            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }
    }
}
