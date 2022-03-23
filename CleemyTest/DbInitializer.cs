using Infrastructure.SqlServer;
using Infrastructure.SqlServer.Models;

namespace Api
{
    public static class DbInitializer
    {
        public static void Initialize(ExpenseContext context)
        {
            if (context.Users.Any())
            {
                return;  
            }

            var users = new UserDao[]
            {
                new UserDao{FirstName = "Anthony", LastName = "Stark", Currency = "USD"},
                new UserDao{FirstName = "Natasha", LastName = "Romanova", Currency = "RUB"}
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
