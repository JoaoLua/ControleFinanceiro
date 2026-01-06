using Application.UseCases.DashBoardUseCases;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleFinanceiro.EndPoints
{
    public static class DashBoardEndPoints
    {
        public static void MapDashBoardEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/dashboard")
                           .WithTags("DashBoard")
                           .WithOpenApi()
                           .RequireAuthorization();

            group.MapGet("/resumo", async (
                 ClaimsPrincipal user,
                 GetResumoFinanceiroUseCase useCase) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Results.Unauthorized();

                var resumo = await useCase.Execute(userId);

                return Results.Ok(resumo);
            });
            group.MapGet("/ultimas", async (
                ClaimsPrincipal user,
                GetUltimasTransacoesUseCase useCase,
                [FromQuery] int quantidade = 5) => 
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Results.Unauthorized();
                var lista = await useCase.Execute(userId, quantidade);
                return Results.Ok(lista);
            });
        }
    }
}
