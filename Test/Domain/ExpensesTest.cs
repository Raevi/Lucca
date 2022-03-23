using System;
using System.ComponentModel.DataAnnotations;
using Test.Domain.Builder;
using Xunit;

namespace Test.Domain
{
    public class ExpensesTest
    {
        [Fact]
        public void Should_Store_A_Date_Whitout_Times()
        {
            var date = DateTime.Now.AddDays(-2);
            var expectedDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            var expense = ExpenseBuilder.NewExpense
                                        .FromValues(date, "the comment")
                                        .Build();

            Assert.Equal(expectedDate, expense.Date);
        }

        [Fact]
        public void Should_Have_A_Comment_Not_Empty()
        {
            var ex = Assert.Throws<ValidationException>(() => 
                ExpenseBuilder.NewExpense
                              .FromValues(DateTime.Now.AddDays(-1), string.Empty)
                              .Build());

            Assert.Equal("The comment cannot be empty", ex.Message);
        }

    }
}
