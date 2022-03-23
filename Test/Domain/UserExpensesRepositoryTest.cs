using Domain;
using Domain.Interface;
using Domain.Models;
using Domain.Records;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Test.Domain.Builder;
using Xunit;

namespace Test.Domain
{
    public class UserExpensesRepositoryTest
    {
        [Fact]
        public async Task Should_Give_The_User_From_Data_Storage_Given_An_User_Identifier()
        {
            var expentedUser = UserBuilder.NewUser.JohnWick("EUR").Build();
            var dataStorage = new Mock<IUserExpensesDataStorage>();
            dataStorage.Setup(x => x.GetUserById(1))
                       .ReturnsAsync(expentedUser);

            var repository = new UserExpensesRepository(dataStorage.Object);
            var user = await repository.GetUserById(1).ConfigureAwait(false);

            user.Should().BeEquivalentTo(expentedUser);
            dataStorage.Verify(x => x.GetUserById(1), Times.Once);
        }

        [Fact]
        public async Task Should_Have_An_Valid_User_Identifier()
        {
            var dataStorage = new Mock<IUserExpensesDataStorage>();
            dataStorage.Setup(x => x.GetUserById(0))
                       .ReturnsAsync(Users.Null);

            var repository = new UserExpensesRepository(dataStorage.Object);
            var expenseRecord = ExpenseBuilder.NewExpense.FromValues("EUR").BuildRecord();
        
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () =>
                 await repository.CreateNewExpenses(0, expenseRecord).ConfigureAwait(false));

            Assert.Equal("userId", ex.Message);
        }

        [Fact]
        public async Task Can_Failed_Due_To_Not_Controllable_Storage_Issue()
        {
            var expentedUser = UserBuilder.NewUser.JohnWick("EUR").Build();
            var dataStorage = new Mock<IUserExpensesDataStorage>();
            dataStorage.Setup(x => x.GetUserById(1))
                       .ReturnsAsync(expentedUser);
            dataStorage.Setup(x => x.CreateNewExpenses(1, It.IsAny<Expenses>()))
                       .ReturnsAsync(false);

            var repository = new UserExpensesRepository(dataStorage.Object);
            var expenseRecord = ExpenseBuilder.NewExpense.FromValues("EUR").BuildRecord();

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                 await repository.CreateNewExpenses(1, expenseRecord).ConfigureAwait(false));

            Assert.Equal("Expense has not be stored", ex.Message);
        }

        [Fact]
        public async Task Should_Give_The_Expense_That_Has_Been_Successfully_Saved()
        {
            var expentedUser = UserBuilder.NewUser.JohnWick("EUR").Build();
            var dataStorage = new Mock<IUserExpensesDataStorage>();
            dataStorage.Setup(x => x.GetUserById(1))
                       .ReturnsAsync(expentedUser);
            dataStorage.Setup(x => x.CreateNewExpenses(1, It.IsAny<Expenses>()))
                       .ReturnsAsync(true);

            var repository = new UserExpensesRepository(dataStorage.Object);
            var expenseRecord = ExpenseBuilder.NewExpense.FromValues("EUR").BuildRecord();

            var newExpense = await repository.CreateNewExpenses(1, expenseRecord).ConfigureAwait(false);

            var expected = ExpensesFactory.Create(expenseRecord.Nature, 
                                                  expenseRecord.Date,
                                                  expenseRecord.Amount,
                                                  expenseRecord.Currency,
                                                  expenseRecord.Comment);

            newExpense.Should().BeEquivalentTo(expected);
            dataStorage.Verify(x => x.GetUserById(1), Times.Once);
            dataStorage.Verify(x => x.CreateNewExpenses(1, It.IsAny<Expenses>()), Times.Once);
        }
    }
}
