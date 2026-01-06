using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CategoriaUseCases
{
    public class DeleteCategoriaUseCase
    {
        private readonly ICategoriaRepository _repository;
        public DeleteCategoriaUseCase(ICategoriaRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Categoria>> Execute(string userId, int categoriaId)
        {
            return await _repository.DeleteAsync(userId, categoriaId);
        }
    }
}
