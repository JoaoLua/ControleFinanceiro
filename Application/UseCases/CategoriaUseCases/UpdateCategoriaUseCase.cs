using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CategoriaUseCases
{
    public class UpdateCategoriaUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public UpdateCategoriaUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<CategoriaResponse> Execute(string userId, int categoriaId, CategoriaRequest updateCategoria)
        {
            var categoria = new Categoria
            {
                Id = categoriaId,
                Nome = updateCategoria.Nome,
                Descricao = updateCategoria.Descricao,
                CorHexadecimal = updateCategoria.CorHexadecimal
            };

            var result = await _categoriaRepository.UpdateAsync(userId, categoriaId, categoria);
            return new CategoriaResponse(
                result.Id,
                result.Nome,
                result.Descricao,
                result.CorHexadecimal
            );
        }
    }
}
