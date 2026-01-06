using Application.UseCases.ContaUseCases;
using Domain.DTOs;
using System.Security.Claims;

namespace ControleFinanceiro.EndPoints
{
    public static class ContaEndPoints
    {
        public static void MapContaEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/contas")
                           .WithTags("Contas")
                           .WithOpenApi()
                           .RequireAuthorization();

            group.MapPost("/", async (ClaimsPrincipal user, ContaRequest request, AddContaUseCase useCase) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Results.Unauthorized();
                var novaConta = await useCase.Execute(request, userId);

                return Results.Created($"/contas/{novaConta.Id}", novaConta);
            });

            group.MapGet("/", async (ClaimsPrincipal user, GetAllContasUseCase useCase) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var contas = await useCase.Execute(userId);
                return Results.Ok(contas);
            })
            .RequireAuthorization();
        }
    }
}
