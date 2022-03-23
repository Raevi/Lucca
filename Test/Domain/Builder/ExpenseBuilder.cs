using Domain.Models;
using Domain.Records;
using System;

namespace Test.Domain.Builder
{
    public class ExpenseBuilder
    {
        private DateTime _date;
        private string _comment = string.Empty;
        private string _currency = string.Empty;
        private decimal _amount;

        public static ExpenseBuilder NewExpense => new ExpenseBuilder();

        public ExpenseBuilder FromValues(string currency)
        {
            _date = DateTime.Now;
            _comment = "the comment";
            _currency = currency;
            _amount = 1;
            return this;
        }

        public ExpenseBuilder FromValues(DateTime date, string comment)
        {
            _date = date;
            _comment = comment;
            _currency = "EUR";
            _amount = 1;
            return this;
        }

        public ExpenseBuilder FromValues(DateTime date, decimal amount, string currency, string comment)
        {
            _date = date;
            _comment = comment;
            _currency = currency;
            _amount = amount;
            return this;
        }

        public Expenses Build()
        {
            return new HotelExpenses(_date,
                                     _amount,
                                     _currency,
                                     _comment);
        }

        public ExpensesRecord BuildRecord()
        {
            return new ExpensesRecord(_date, Nature.Restaurant, _amount, _currency, _comment);
        }
    }
}
