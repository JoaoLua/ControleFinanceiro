using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task AddAsync(Categoria categoria);
        Task<IEnumerable<Categoria>> GetAllAsync(string userId);
        Task<Categoria?> GetByIdAsync(int id, string userId);
        Task<IEnumerable<Categoria>> DeleteAsync(string userId, int categoriaId);
        Task<Categoria> UpdateAsync(string userId, int categoriaId, Categoria updateCategoria);
    }
}
