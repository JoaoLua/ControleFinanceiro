using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Relatorios
{
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class ExtratoRelatorio : IDocument
    {
        public class ExtratoModel
        {
            public string NomeUsuario { get; set; } = string.Empty;
            public DateTime DataInicio { get; set; }
            public DateTime DataFim { get; set; }
            public List<TransacaoItem> Transacoes { get; set; } = new();
        }

        public class TransacaoItem
        {
            public DateTime Data { get; set; }
            public string Descricao { get; set; } = string.Empty;
            public string Categoria { get; set; } = string.Empty;
            public decimal Valor { get; set; }
            public string Tipo { get; set; } = string.Empty;
        }

        private readonly ExtratoModel _model;

        public ExtratoRelatorio(ExtratoModel model)
        {
            _model = model;
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Extrato Financeiro").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                    column.Item().Text($"Usuário: {_model.NomeUsuario}");
                    column.Item().Text($"Período: {_model.DataInicio:dd/MM/yyyy} até {_model.DataFim:dd/MM/yyyy}");
                });

                row.ConstantItem(100).AlignRight().Text(DateTime.Now.ToString("g"));
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                column.Item().Table(table =>
                {
                    // Definição das colunas
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(80); // Data
                        columns.RelativeColumn();   // Descrição
                        columns.ConstantColumn(100); // Categoria
                        columns.ConstantColumn(80); // Tipo
                        columns.ConstantColumn(100); // Valor
                    });

                    // Cabeçalho da Tabela
                    table.Header(header =>
                    {
                        header.Cell().Text("Data").Bold();
                        header.Cell().Text("Descrição").Bold();
                        header.Cell().Text("Categoria").Bold();
                        header.Cell().Text("Tipo").Bold();
                        header.Cell().AlignRight().Text("Valor").Bold();
                    });

                    // Linhas
                    foreach (var item in _model.Transacoes)
                    {
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{item.Data:dd/MM}");
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Descricao);
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Categoria);

                        // Cor condicional
                        var corTipo = item.Tipo == "Receita" ? Colors.Green.Medium : Colors.Red.Medium;
                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Tipo).FontColor(corTipo);

                        table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text($"R$ {item.Valor:N2}");
                    }
                });

                // Totalizador no final
                var totalReceitas = _model.Transacoes.Where(t => t.Tipo == "Receita").Sum(t => t.Valor);
                var totalDespesas = _model.Transacoes.Where(t => t.Tipo == "Despesa").Sum(t => t.Valor);
                var saldo = totalReceitas - totalDespesas;

                column.Item().PaddingTop(10).AlignRight().Text($"Saldo do Período: R$ {saldo:N2}").FontSize(14).Bold();
            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    }
}
