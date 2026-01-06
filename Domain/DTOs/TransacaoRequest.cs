using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class TransacaoRequest
    {
        [Required]
        public TipoTransacao Tipo { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public int ContaId { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }
    }
}