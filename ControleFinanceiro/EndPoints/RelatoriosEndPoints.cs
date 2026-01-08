using Domain.Interfaces;
using Infraestructure.Relatorios;
using QuestPDF.Fluent;
using System.Security.Claims;

namespace ControleFinanceiro.EndPoints
{
    public static class RelatoriosEndPoints
    {
        public static void MapRelatoriosEndPoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/relatorios")
                           .WithTags("Relatórios")
                           .WithOpenApi()
                           .RequireAuthorization();
            group.MapGet("/relatorio/pdf", async (ClaimsPrincipal user, ITransacaoRepository repo) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                var transacoesDb = await repo.GetAllAsync(userId); 

                var model = new ExtratoRelatorio.ExtratoModel
                {
                    NomeUsuario = user.Identity?.Name ?? "Usuário",
                    DataInicio = DateTime.Now.AddDays(-30),
                    DataFim = DateTime.Now,
                    Transacoes = transacoesDb.Select(t => new ExtratoRelatorio.TransacaoItem
                    {
                        Data = t.Data,
                        Descricao = t.Descricao ?? "-",
                        Categoria = t.Categoria?.Nome ?? "Sem Categoria",
                        Valor = t.Valor,
                        Tipo = ((int)t.Tipo) == 0 ? "Receita" : "Despesa" 
                    }).ToList()
                };

                // 3. Gera o PDF
                var documento = new ExtratoRelatorio(model);
                var pdfBytes = documento.GeneratePdf();

                // 4. Retorna o arquivo
                return Results.File(pdfBytes, "application/pdf");
            })
            .RequireAuthorization();
        }
    }
}
