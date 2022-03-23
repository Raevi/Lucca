using Domain.Models;
using Domain.Models.Error;
using FluentAssertions;
using System;
using Test.Domain.Builder;
using Xunit;

namespace Test.Domain
{
    public class UsersTest
    {
        [Fact]
        public void New_Expense_Should_Have_Same_Currency_Then_User()
        {
            var user = UserBuilder.NewUser.JohnWick("EUR").Build();

            var ex = Assert.Throws<AppValidationException>(() =>
                user.CreateNewExpenses(DateTime.Now, 
                                       Nature.Restaurant,
                                       1,
                                       "USD",
                                       "comment"));

            Assert.Equal("The expense currecy shall be the same that user currency", ex.Message);
        }

        [Fact]
        public void New_Expense_Should_Not_Exist_With_Same_Amount_And_Date()
        {
            var user = UserBuilder.NewUser.JohnWick("EUR").Build();
            user.CreateNewExpenses(DateTime.Now.AddDays(-5), Nature.Hotel, 16, "EUR", "comment");

            var ex = Assert.Throws<AppValidationException>(() =>
                user.CreateNewExpenses(DateTime.Now.AddDays(-5),
                                       Nature.Restaurant,
                                       16,
                                       "EUR",
                                       "comment"));

            Assert.Equal("The same expense already exist at this date", ex.Message);
        }

        [Fact]
        public void New_Expense_Should_Added_To_The_User_Expenses_List()
        {
            var expected = new[] 
            { 
                ExpenseBuilder.NewExpense.FromValues(DateTime.Now.AddDays(-5), 16, "EUR", "comment").Build()
            };
            var user = UserBuilder.NewUser.JohnWick("EUR").Build();

            user.CreateNewExpenses(DateTime.Now.AddDays(-5), Nature.Hotel, 16, "EUR", "comment");

            user.Expenses.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public void Should_Not_Have_Date_Older_Then_3_Month()
        {
            var user = UserBuilder.NewUser.JohnWick("EUR").Build();

            var ex = Assert.Throws<AppValidationException>(() =>
                user.CreateNewExpenses(DateTime.Now.AddMonths(-3).AddDays(-1),
                                       Nature.Restaurant,
                                       16,
                                       "EUR",
                                       "comment"));

            Assert.Equal("The expense cannot be older than 3 months or earlier than today", ex.Message);
        }

        [Fact]
        public void Can_Have_Date_Exactly_3_Month_Old()
        {
            var expected = new[]
            {
                ExpenseBuilder.NewExpense.FromValues(DateTime.Now.AddMonths(-3), 16, "EUR", "comment").Build()
            };
            var user = UserBuilder.NewUser.JohnWick("EUR").Build();

            user.CreateNewExpenses(DateTime.Now.AddMonths(-3), Nature.Hotel, 16, "EUR", "comment");

            user.Expenses.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Can_Have_Date_Equal_Today()
        {
            var expected = new[]
            {
                ExpenseBuilder.NewExpense.FromValues(DateTime.Now, 16, "EUR", "comment").Build()
            };
            var user = UserBuilder.NewUser.JohnWick("EUR").Build();

            user.CreateNewExpenses(DateTime.Now, Nature.Hotel, 16, "EUR", "comment");

            user.Expenses.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_Not_Have_Date_Greater_Than_Today()
        {
            var user = UserBuilder.NewUser.JohnWick("EUR").Build();

            var ex = Assert.Throws<AppValidationException>(() =>
                user.CreateNewExpenses(DateTime.Now.AddDays(1),
                                       Nature.Restaurant,
                                       16,
                                       "EUR",
                                       "comment"));

            Assert.Equal("The expense cannot be older than 3 months or earlier than today", ex.Message);
        }

    }
}
