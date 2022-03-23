namespace Domain.Models
{
    public class RestaurantExpenses : Expenses
    {
        public RestaurantExpenses(DateTime date, decimal amount, string currency, string comment) 
            : base(date, Nature.Restaurant, amount, currency, comment)
        {
        }
    }
}
