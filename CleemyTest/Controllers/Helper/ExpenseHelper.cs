using Api.Controllers.Models;

namespace Api.Controllers.Helper
{
    public static class ExpenseHelper
    {
        private const string Asc = "asc";
        private const string Desc = "desc";

        public static IEnumerable<ExpenseResultItems> ApplySort(this IEnumerable<ExpenseResultItems> expenses,
                                                                string? sortKey,
                                                                string? sortDirection)
        {
            if (nameof(ExpenseResultItems.Amount).Equals(sortKey, StringComparison.OrdinalIgnoreCase))
            {
                return expenses.SortBy(x => x.Amount, sortDirection);
            }
            if (nameof(ExpenseResultItems.Date).Equals(sortKey, StringComparison.OrdinalIgnoreCase))
            {
                return expenses.SortBy(x => x.Date, sortDirection);
            }
            return expenses;
        }

        private static IEnumerable<ExpenseResultItems> SortBy<TKey>(this IEnumerable<ExpenseResultItems> expenses, 
                                                                    Func<ExpenseResultItems, TKey> keySelector, 
                                                                    string? sortDirection)
        {
            if (Asc.Equals(sortDirection, StringComparison.OrdinalIgnoreCase))
            {
                return expenses.OrderBy(keySelector);
            }
            if (Desc.Equals(sortDirection, StringComparison.OrdinalIgnoreCase))
            {
                return expenses.OrderByDescending(keySelector);
            }
            return expenses;
        }
    }
}
