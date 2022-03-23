using Domain.Models;
using System;
using System.Collections.Generic;

namespace Test.Domain.Builder
{
    public class UserBuilder
    {
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _currency = string.Empty;
        private IEnumerable<Expenses> _expenses = Array.Empty<Expenses>();

        public static UserBuilder NewUser => new UserBuilder();

        public UserBuilder JohnWick(string currency)
        {
            _firstName = "John";
            _lastName = "Wick";
            _currency = currency;
            return this;
        }

        public UserBuilder WithExpenses(IEnumerable<Expenses> expenses)
        {
            _expenses = expenses;
            return this;
        }

        public Users Build()
        {
            return new Users(_firstName, 
                             _lastName,
                             _currency,
                             _expenses);
        }
    }
}
