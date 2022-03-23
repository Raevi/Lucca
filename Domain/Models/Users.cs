using Domain.Models.Error;
using System.Collections.ObjectModel;

namespace Domain.Models
{
    public class Users
    {
        public string FirstName { get; }

        public string LastName { get; }

        public string Currency { get; }

        private List<Expenses> _expenses { get; } = new List<Expenses>();

        public ReadOnlyCollection<Expenses> Expenses => _expenses.AsReadOnly();

        private Users() : this(string.Empty, 
                               string.Empty, 
                               string.Empty,
                               Array.Empty<Expenses>()) { }

        public Users(string firstName, 
                     string lastName, 
                     string currency,
                     IEnumerable<Expenses> expenses)
        {
            FirstName = firstName;
            LastName = lastName;
            Currency = currency;
            _expenses = new List<Expenses>(expenses);
        }

        public Expenses CreateNewExpenses(DateTime date,
                                          Nature nature,
                                          decimal amount,
                                          string currency,
                                          string comment)
        {
            if(!Currency.Equals(currency, StringComparison.Ordinal))
            {
                throw new AppValidationException(AppValidationMessage.Currency);
            }

            if (_expenses.Any(x => x.Amount == amount && x.Date == date.Date))
            {
                throw new AppValidationException(AppValidationMessage.SameExpense);
            }

            var expense = ExpensesFactory.Create(nature, date, amount, currency, comment);
            if (expense.HasValidDate())
            {
                _expenses.Add(expense);
                return expense;
            }
            else
            {
                throw new AppValidationException(AppValidationMessage.Date);
            }
        }

        public static Users Null = new Users();
    }
}
