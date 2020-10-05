using ChallengeING.Data;
using ChallengeING.Data.Repositories;
using ChallengeING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeING.Tests
{
    [TestClass]
    public class AccountsRepositoryTests
    {
        [TestMethod]
        public async Task GetMonthlyReportForAccount_TestAsync()
        {
            var id = new Guid("17b33123-99ea-4bac-b1d1-c3fc0b7579c3");
            var options = new DbContextOptionsBuilder<SqlDBContext>()
                        .UseInMemoryDatabase(databaseName: "Test")
                        .Options;

            using (var context = new SqlDBContext(options))
            {
                context.Accounts.Add(new Account
                {
                    ResourceId = new Guid("17b33123-99ea-4bac-b1d1-c3fc0b7579c3"),
                    Product = new Product
                    {
                        Id = new Guid(),
                        Name = "some product"
                    },
                    IBAN = "32141sadf",
                    Name = "Jon",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Amount = 20.0M,
                            CategoryId = Models.Enums.Category.Travel,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 1
                        },
                        new Transaction
                        {
                            Amount = 120.0M,
                            CategoryId = Models.Enums.Category.Travel,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        },
                        new Transaction
                        {
                            Amount = 40.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 2
                        },
                        new Transaction
                        {
                            Amount = 160.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 5
                        }
                    },
                    Currency = Models.Enums.Currency.RON
                });
                context.Accounts.Add(new Account
                {
                    ResourceId = new Guid("e2287c78-0454-402a-86bd-b9e6f5cb73be"),
                    Product = new Product
                    {
                        Id = new Guid(),
                        Name = "some product"
                    },
                    IBAN = "32141sadf",
                    Name = "Jon",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Amount = 20.0M,
                            CategoryId = Models.Enums.Category.MedicalExpenses,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,10,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 15
                        },
                        new Transaction
                        {
                            Amount = 120.0M,
                            CategoryId = Models.Enums.Category.MedicalExpenses,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 42
                        },
                        new Transaction
                        {
                            Amount = 40.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 45
                        },
                        new Transaction
                        {
                            Amount = 55.0M,
                            CategoryId = Models.Enums.Category.Entertainment,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 46
                        }
                    },
                    Currency = Models.Enums.Currency.EUR
                });
                context.Accounts.Add(new Account
                {
                    ResourceId = new Guid("e2417c78-4254-402a-86bd-b9d1f5cb73be"),
                    Product = new Product
                    {
                        Id = new Guid(),
                        Name = "some product"
                    },
                    IBAN = "32141sadf",
                    Name = "Joffen",
                    Transactions = new List<Transaction>() {
                        new Transaction
                        {
                            Amount = 220.0M,
                            CategoryId = Models.Enums.Category.Clothing,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020, 9, 10, 13, 0, 0, TimeSpan.Zero),
                            TransactionId = 113
                        },
                        new Transaction
                        {
                            Amount = 420.0M,
                            CategoryId = Models.Enums.Category.Clothing,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020, 9, 10, 13, 0, 0, TimeSpan.Zero),
                            TransactionId = 415
                        },
                        new Transaction
                        {
                            Amount = 20.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020, 9, 10, 13, 0, 0, TimeSpan.Zero),
                            TransactionId = 462
                        }
                    },
                    Currency = Models.Enums.Currency.GBP
                });
                context.SaveChanges();
            }

            var repo = new AccountRepository(new SqlDBContext(options));
            var result = await repo.GetMonthlyReportForAccount(id);

            var travel = result.Where(x => x.CategoryName == ChallengeING.Models.Enums.Category.Travel.ToString()).Select(y => y.TotalAmount);
            var food = result.Where(x => x.CategoryName == ChallengeING.Models.Enums.Category.Travel.ToString()).Select(y => y.TotalAmount);

            Assert.AreEqual(160.0M, travel.FirstOrDefault());
            Assert.AreEqual(200.0M, food.FirstOrDefault());
        }

        private List<Account> Data()
        {
            List<Account> accounts = new List<Account>()
            {
                new Account
                {
                    ResourceId = new Guid("17b33123-99ea-4bac-b1d1-c3fc0b7579c3"),
                    Product = new Product
                    {
                        Id = new Guid(),
                        Name = "some product"
                    },
                    IBAN = "32141sadf",
                    Name = "Jon",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Amount = 20.0M,
                            CategoryId = Models.Enums.Category.Travel,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 1
                        },
                        new Transaction
                        {
                            Amount = 120.0M,
                            CategoryId = Models.Enums.Category.Travel,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        },
                        new Transaction
                        {
                            Amount = 40.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        },
                        new Transaction
                        {
                            Amount = 160.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        }
                    },
                    Currency = Models.Enums.Currency.RON
                },
                new Account
                {
                    ResourceId = new Guid("e2287c78-0454-402a-86bd-b9e6f5cb73be"),
                    Product = new Product
                    {
                        Id = new Guid(),
                        Name = "some product"
                    },
                    IBAN = "32141sadf",
                    Name = "Jon",
                    Transactions = new List<Transaction>()
                    {
                        new Transaction
                        {
                            Amount = 20.0M,
                            CategoryId = Models.Enums.Category.MedicalExpenses,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,10,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 1
                        },
                        new Transaction
                        {
                            Amount = 120.0M,
                            CategoryId = Models.Enums.Category.MedicalExpenses,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        },
                        new Transaction
                        {
                            Amount = 40.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        },
                        new Transaction
                        {
                            Amount = 55.0M,
                            CategoryId = Models.Enums.Category.Entertainment,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        }
                    },
                    Currency = Models.Enums.Currency.EUR
                },
                new Account
                {
                    ResourceId = new Guid("e2417c78-4254-402a-86bd-b9d1f5cb73be"),
                    Product = new Product
                    {
                        Id = new Guid(),
                        Name = "some product"
                    },
                    IBAN = "32141sadf",
                    Name = "Joffen",
                    Transactions = new List<Transaction>(){
                        new Transaction
                        {
                            Amount = 220.0M,
                            CategoryId = Models.Enums.Category.Clothing,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 1
                        },
                        new Transaction
                        {
                            Amount = 420.0M,
                            CategoryId = Models.Enums.Category.Clothing,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        },
                        new Transaction
                        {
                            Amount = 20.0M,
                            CategoryId = Models.Enums.Category.Food,
                            IBAN = "dsafdsafafdas",
                            TransactionDate = new DateTimeOffset(2020,9,10,13,0,0, TimeSpan.Zero),
                            TransactionId = 4
                        }
                    },
                    Currency = Models.Enums.Currency.GBP
                }
            };

            return accounts;
        }
    }
}
