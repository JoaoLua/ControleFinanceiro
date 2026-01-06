using Application.UseCases.TransacaoUseCases;
using Domain.DTOs;
using Domain.Enums;
using System.Security.Claims;

namespace ControleFinanceiro.EndPoints
{
    public static class TransacaoEndPoints
    {
        public static void MapTransacaoEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/transacoes")
                           .WithTags("Transacoes")
                           .WithOpenApi()
                           .RequireAuthorization();

            group.MapPost("/", async (
                TransacaoRequest request,
                ClaimsPrincipal user,
                AddTransacaoUseCase useCase) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Results.Unauthorized();

                try
                {
                    var response = await useCase.Execute(request, userId);
                    if (response == null)
                        return Results.BadRequest(new { error = "Conta não encontrada ou não pertence ao usuário." });
                    return Results.Created($"/transacoes/{response.Id}", response);
                }
                catch (Exception ex)
                {
                    var erroReal = ex;
                    while (erroReal.InnerException != null)
                    {
                        erroReal = erroReal.InnerException;
                    }

                    return Results.BadRequest(new
                    {
                        erro = "ERRO DE BANCO DE DADOS",
                        detalhe = erroReal.Message
                    });
                }
            });
            group.MapGet("/", async (
                ClaimsPrincipal user,
                GetAllTransacoesUseCase useCase,
                TipoTransacao? tipo,
                DateTime? data,
                int? contaId,
                int? categoriaId) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Results.Unauthorized();
                var resultado = await useCase.Execute(userId, tipo, data, contaId, categoriaId);
                return Results.Ok(resultado);
            });
        }
    }
}
