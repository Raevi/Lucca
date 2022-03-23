namespace Domain.Models.Error
{
    public static class AppValidationMessage
    {
        public const string SameExpense = "The same expense already exist at this date";
        public const string Currency = "The expense currecy shall be the same that user currency";
        public const string Date = "The expense cannot be older than 3 months or earlier than today";
        public const string StorageFailed = "Expense has not be stored";
        public const string EmptyComment = "The comment cannot be empty";
    }
}
