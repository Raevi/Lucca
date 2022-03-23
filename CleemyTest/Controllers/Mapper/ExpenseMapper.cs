using Api.Controllers.Models;
using Domain.Models;
using Domain.Records;

namespace Api.Controllers.Mapper
{
    public static class ExpenseMapper
    {
        public static ExpensesRecord ToExpensesRecord(this ExpenseToCreate expense)
        {
            if (!Enum.TryParse(expense.Nature.ToString(), out Domain.Models.Nature domainNature))
            {
                throw new ArgumentException("Invalid Nature");
            }

            return new ExpensesRecord(expense.Date.Value,
                                      domainNature,
                                      expense.Amount,
                                      expense.Currency,
                                      expense.Comment);
        }

        public static IEnumerable<ExpenseResultItems> ToExpenseResultItems(this Users user)
        {
            string names = $"{user.FirstName} {user.LastName}";
            return user.Expenses.Select(x => x.ToExpenses(names));
        }

        public static ExpenseResultItems ToExpenses(this Expenses expenses, string names)
        {
            return new ExpenseResultItems
            {
                Comment = expenses.Comment,
                Currency = expenses.Currency,
                Date = expenses.Date,
                Nature = expenses.Nature.ToString(),
                User = names,
                Amount = expenses.Amount
            };
        }
    }
}
