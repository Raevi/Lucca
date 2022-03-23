using Domain.Interface;
using Domain.Models;
using Infrastructure.SqlServer;
using Infrastructure.SqlServer.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class UserExpensesDataStorage : IUserExpensesDataStorage
    {
        private readonly ExpenseContext _context;
        private readonly ILogger _logger;

        public UserExpensesDataStorage(ExpenseContext context,
                                       ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger(nameof(UserExpensesDataStorage));
        }

        public async Task<bool> CreateNewExpenses(int userId, Expenses expense)
        {
            try
            {
                _context.Expenses.Add(new SqlServer.Models.ExpenseDao
                {
                    Amount = expense.Amount,
                    Comment = expense.Comment,
                    Currency = expense.Currency,
                    Date = expense.Date,
                    Nature = expense.Nature.ToString(),
                    UserId = userId
                });

                var result = await _context.SaveChangesAsync().ConfigureAwait(false);
                return result != 0;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Users> GetUserById(int userId)
        {
            try
            {
                var userDao = await _context.Users.Include(x => x.Expences)
                                                  .FirstOrDefaultAsync(x => x.UserId == userId)
                                                  .ConfigureAwait(false);
                if (userDao == null)
                {
                    return Users.Null;
                }
                else
                {
                    return userDao.ToUsers();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
