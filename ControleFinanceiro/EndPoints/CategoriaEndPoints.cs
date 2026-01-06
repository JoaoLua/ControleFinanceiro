using Application.UseCases.CategoriaUseCases;
using Domain.DTOs;
using System.Security.Claims;


namespace ControleFinanceiro.EndPoints
{
    public static class CategoriaEndPoints
    {
        public static void MapCategoriaEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/categorias")
                           .WithTags("Categorias") 
                           .WithOpenApi()
                           .RequireAuthorization();

            group.MapPost("/", async (ClaimsPrincipal user, CategoriaRequest request, AddCategoriaUseCase useCase) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                request.UserId = userId;
                var response = await useCase.Execute(request);

                return Results.Created($"/categorias/{response.Id}", response);
            })
            .WithName("CriarCategoria");
            group.MapGet("/", async (ClaimsPrincipal user, GetAllCategoriasUseCase useCase) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var categorias = await useCase.Execute(userId);
                return Results.Ok(categorias);
            })
            .RequireAuthorization();

            group.MapDelete("/{categoriaId}", async (ClaimsPrincipal user, int categoriaId, DeleteCategoriaUseCase useCase) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var categorias = await useCase.Execute(userId, categoriaId);
                return Results.Ok(categorias);
            })
            .WithName("DeletarCategoria");
        }
    }
}
