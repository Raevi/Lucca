using Domain.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Domain
{
    public class ExpensesFactoryTest
    {
        [Fact]
        public void Can_Obtains_A_HotelExpenses_Instance()
        {
            var expenses = ExpensesFactory.Create(Nature.Hotel, DateTime.Now, 1, "EUR", "the comment");

            expenses.Should().BeAssignableTo<HotelExpenses>();
        }

        [Fact]
        public void Can_Obtains_A_RestaurantExpenses_Instance()
        {
            var expenses = ExpensesFactory.Create(Nature.Restaurant, DateTime.Now, 1, "EUR", "the comment");

            expenses.Should().BeAssignableTo<RestaurantExpenses>();
        }

        [Fact]
        public void Can_Obtains_A_MiscExpenses_Instance()
        {
            var expenses = ExpensesFactory.Create(Nature.Misc, DateTime.Now, 1, "EUR", "the comment");

            expenses.Should().BeAssignableTo<MiscExpenses>();
        }
    }
}
