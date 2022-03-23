namespace Api.Controllers.Models
{
    public record ExpenseResultItems
    {
        public DateTime Date { get; init; }

        public string? Nature { get; init; }

        public decimal Amount { get; init; }

        public string? Currency { get; init; }

        public string? Comment { get; init; }

        public string? User { get; init; }
    }
}
