using Domain.Models;
using Domain.Records;

namespace Domain.Interface
{
    public interface IUserExpensesRepository
    {
        Task<Expenses> CreateNewExpenses(int userId, ExpensesRecord expensesProperties);

        Task<Users> GetUserById(int userId);
    }
}
