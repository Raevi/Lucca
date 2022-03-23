using Domain.Interface;
using Domain.Models;
using Domain.Models.Error;
using Domain.Records;

namespace Domain
{
    public class UserExpensesRepository : IUserExpensesRepository
    {
        private readonly IUserExpensesDataStorage _dataStorage;

        public UserExpensesRepository(IUserExpensesDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task<Expenses> CreateNewExpenses(int userId, ExpensesRecord expensesProperties)
        {
            var user = await _dataStorage.GetUserById(userId).ConfigureAwait(false);
            if (user == Users.Null)
            {
                throw new ArgumentException(nameof(userId));
            }

            var newExpense = user.CreateNewExpenses(expensesProperties.Date,
                                                    expensesProperties.Nature,
                                                    expensesProperties.Amount,
                                                    expensesProperties.Currency,
                                                    expensesProperties.Comment);

            var result = await _dataStorage.CreateNewExpenses(userId, newExpense).ConfigureAwait(false);
            if(result == false)
            {
                throw new InvalidOperationException(AppValidationMessage.StorageFailed);
            }
            else
            {
                return newExpense;
            }
        }

        public async Task<Users> GetUserById(int userId)
        {
            return await _dataStorage.GetUserById(userId).ConfigureAwait(false);
        }
    }
}
