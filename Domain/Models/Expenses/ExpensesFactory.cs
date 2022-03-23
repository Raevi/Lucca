namespace Domain.Models
{
    public static class ExpensesFactory
    {
        public static Expenses Create(Nature nature,
                                      DateTime date, 
                                      decimal amount, 
                                      string currency, 
                                      string comment)
        {
            return nature switch
            {
                Nature.Restaurant => new RestaurantExpenses(date, amount, currency, comment),
                Nature.Hotel => new HotelExpenses(date, amount, currency, comment),
                _ => new MiscExpenses(date, amount, currency, comment)
            };
        }
    }
}
