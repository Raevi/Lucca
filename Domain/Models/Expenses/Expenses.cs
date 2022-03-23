using Domain.Models.Error;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public abstract class Expenses
    {
        public DateTime Date { get; }

        public Nature Nature { get; }

        public decimal Amount { get; }

        public string Currency { get; }

        public string Comment { get; }

        public Expenses(DateTime date, 
                        Nature nature, 
                        decimal amount, 
                        string currency, 
                        string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new ValidationException(AppValidationMessage.EmptyComment);
            }

            Date = date.Date;
            Nature = nature;
            Amount = amount;
            Currency = currency;
            Comment = comment;
        }

        public bool HasValidDate() => DateIsValid(Date);

        public static bool DateIsValid(DateTime date) =>
            DateTime.Now.AddMonths(-3).Date <= date && date <= DateTime.Now.Date;
    }
}
