namespace Domain.Models
{
    public class MiscExpenses : Expenses
    {
        public MiscExpenses(DateTime date, decimal amount, string currency, string comment) 
            : base(date, Nature.Misc, amount, currency, comment)
        {
        }
    }
}
