using ChallengeING.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeING.Data
{
    public class SqlDBContext : DbContext
    {
        public SqlDBContext(DbContextOptions<SqlDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }

}
