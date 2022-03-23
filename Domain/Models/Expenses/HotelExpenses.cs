namespace Domain.Models
{
    public class HotelExpenses : Expenses
    {
        public HotelExpenses(DateTime date, decimal amount, string currency, string comment) 
            : base(date, Nature.Hotel, amount, currency, comment)
        {
        }
    }
}
