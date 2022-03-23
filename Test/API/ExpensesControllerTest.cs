using Api.Controllers.Models;
using CleemyTest.Controllers;
using Domain.Interface;
using Domain.Models;
using Domain.Records;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Domain.Builder;
using Xunit;

namespace Test.API
{
    public class ExpensesControllerTest
    {
        [Fact]
        public async Task Should_Return_User_Expenses_From_Repository()
        {
            var expenses = new[]
            {
                ExpenseBuilder.NewExpense.FromValues(DateTime.Now.AddDays(-5), 16.4m, "USD", "Comment").Build(),
                ExpenseBuilder.NewExpense.FromValues(DateTime.Now.AddDays(-1), 11.4m, "USD", "Comment").Build(),
            };
            var user = UserBuilder.NewUser.JohnWick("USD").WithExpenses(expenses).Build();
            var repository = new Mock<IUserExpensesRepository>();
            repository.Setup(x => x.GetUserById(1))
                      .ReturnsAsync(user);
            var controller = new ExpensesController(repository.Object);

            var result = await controller.ListExpensesByUser(1, "date", "desc").ConfigureAwait(false);

            var expected = new[]
            {
                new ExpenseResultItems
                {
                    Amount = 11.4m,
                    Comment = "Comment",
                    Currency = "USD",
                    Nature = "Hotel",
                    Date = DateTime.Now.AddDays(-1).Date,
                    User = "John Wick"
                },
                new ExpenseResultItems
                {
                    Amount = 16.4m,
                    Comment = "Comment",
                    Currency = "USD",
                    Nature = "Hotel",
                    Date = DateTime.Now.AddDays(-5).Date,
                    User = "John Wick"
                }
            };

            var okObjectResult = result as OkObjectResult;
            var resultExpenses = okObjectResult.Value as IEnumerable<ExpenseResultItems>;

            resultExpenses.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
            repository.Verify(x => x.GetUserById(1), Times.Once);
        }

        [Fact]
        public async Task Should_Return_NotFound_When_User_Doesnt_Exists()
        {
            var repository = new Mock<IUserExpensesRepository>();
            repository.Setup(x => x.GetUserById(1))
                      .ReturnsAsync(Users.Null);
            var controller = new ExpensesController(repository.Object);

            var result = await controller.ListExpensesByUser(1, "date", "desc").ConfigureAwait(false);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            repository.Verify(x => x.GetUserById(1), Times.Once);
        }

        [Fact]
        public async Task Should_Created_A_New_Expenses()
        {
            var repository = new Mock<IUserExpensesRepository>();
            repository.Setup(x => x.CreateNewExpenses(1, It.IsAny<ExpensesRecord>()))
                      .ReturnsAsync(ExpenseBuilder.NewExpense.FromValues("USD").Build());
            var controller = new ExpensesController(repository.Object);

            var result = await controller.CreateNewExpenses(1, new ExpenseToCreate 
            { 
                Nature = Api.Controllers.Models.Nature.Restaurant,
                Date = DateTime.Now
            }).ConfigureAwait(false);

            var statusCodeResult = result as StatusCodeResult;

            Assert.Equal(201, statusCodeResult.StatusCode);
            repository.Verify(x => x.CreateNewExpenses(1, It.IsAny<ExpensesRecord>()), Times.Once);
        }
    }
}
