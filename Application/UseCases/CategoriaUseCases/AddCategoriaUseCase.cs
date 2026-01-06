using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.CategoriaUseCases
{
    public class AddCategoriaUseCase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public AddCategoriaUseCase(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaResponse> Execute(CategoriaRequest request)
        {
            var categoria = new Categoria
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                CorHexadecimal = request.CorHexadecimal ?? "669bbc",
                UserId = request.UserId
            };

            await _categoriaRepository.AddAsync(categoria);
            return new CategoriaResponse(categoria.Id, categoria.Nome!);
        }
    }
}

