using Domain.Models;

namespace Domain.Records
{
    public record class ExpensesRecord(DateTime Date,
                                       Nature Nature,
                                       decimal Amount,
                                       string Currency,
                                       string Comment)
    {
    }
}
