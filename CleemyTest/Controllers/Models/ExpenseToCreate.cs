using Api.Controllers.Validation;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Models
{
    public record ExpenseToCreate
    {
        [Required]
        [ValidExpenseDate]
        public DateTime? Date { get; init; }
        [Required]
        public Nature? Nature { get; init; }
        public decimal Amount { get; init; }
        [Required]
        [MinLength(1)]
        public string? Currency { get; init; }
        [Required]
        [MinLength(1)]
        public string? Comment { get; init; }
    }
}
