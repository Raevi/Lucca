using Api.Controllers.Helper;
using Api.Controllers.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Test.Domain.Builder;
using Xunit;

namespace Test.API
{
    public class ExpenseHelperTest
    {
        [Theory]
        [MemberData(nameof(GetRecordSet))]
        public void Should_Sort_Items_Given_A_Key_And_A_Direction(IEnumerable<ExpenseResultItems> items,
                                                                  string? sortKey,
                                                                  string? sortDirection,
                                                                  IEnumerable<ExpenseResultItems> expected)
        {
            var result = items.ApplySort(sortKey, sortDirection);

            result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

        public static IEnumerable<object[]> GetRecordSet =>
        new List<object[]>
        {
            new object[] 
            {
                new[]
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
                },
                "date",
                "desc",
                new[]
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
                }
            },
            new object[]
            {
                new[]
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
                },
                "date",
                "asc",
                new[]
                {
                    new ExpenseResultItems
                    {
                        Amount = 16.4m,
                        Comment = "Comment",
                        Currency = "USD",
                        Nature = "Hotel",
                        Date = DateTime.Now.AddDays(-5).Date,
                        User = "John Wick"
                    },
                    new ExpenseResultItems
                    {
                        Amount = 11.4m,
                        Comment = "Comment",
                        Currency = "USD",
                        Nature = "Hotel",
                        Date = DateTime.Now.AddDays(-1).Date,
                        User = "John Wick"
                    }
                }
            },
            new object[]
            {
                new[]
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
                },
                "amount",
                "asc",
                new[]
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
                }
            },
            new object[]
            {
                new[]
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
                },
                "amount",
                "desc",
                new[]
                {
                    new ExpenseResultItems
                    {
                        Amount = 16.4m,
                        Comment = "Comment",
                        Currency = "USD",
                        Nature = "Hotel",
                        Date = DateTime.Now.AddDays(-5).Date,
                        User = "John Wick"
                    },
                    new ExpenseResultItems
                    {
                        Amount = 11.4m,
                        Comment = "Comment",
                        Currency = "USD",
                        Nature = "Hotel",
                        Date = DateTime.Now.AddDays(-1).Date,
                        User = "John Wick"
                    }
                }
            },
        };
    }
}
