using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CategoriaUseCases
{
    public class GetAllCategoriasUseCase
    {
        private readonly ICategoriaRepository _repository;
        public GetAllCategoriasUseCase(ICategoriaRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Categoria>> Execute(string userId)
        {

            return await _repository.GetAllAsync(userId);
        }
    }
}
