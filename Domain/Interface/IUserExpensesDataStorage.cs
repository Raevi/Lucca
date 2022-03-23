using Domain.Models;

namespace Domain.Interface
{
    public interface IUserExpensesDataStorage
    {
        Task<bool> CreateNewExpenses(int userId, Expenses expense);

        Task<Users> GetUserById(int userId);
    }
}
