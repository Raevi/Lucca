using Domain.Models;
using Infrastructure.SqlServer.Models;

namespace Infrastructure.SqlServer.Mapper
{
    public static class ExpenseMapper
    {
        public static Users ToUsers(this UserDao userDao)
        {
            return new Users(userDao.FirstName,
                             userDao.LastName,
                             userDao.Currency,
                             userDao.Expences?.ToExpenses() ?? Array.Empty<Expenses>());
        }
    
        private static IEnumerable<Expenses> ToExpenses(this IEnumerable<ExpenseDao> expenses)
        {
            return expenses.Select(x => x.ToExpenses());
        }

        private static Expenses ToExpenses(this ExpenseDao expense)
        {
            Enum.TryParse(expense.Nature, out Nature nature);
            return ExpensesFactory.Create(nature,
                                          expense.Date,
                                          expense.Amount,
                                          expense.Currency,
                                          expense.Comment);
        }
    }
}
