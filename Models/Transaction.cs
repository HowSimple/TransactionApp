using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Commerce_TransactionApp.Models
{
    public class Transaction
    {

        public int AccountId {get; set;}

    }

public class TransactionContext : DbContext {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
        }

        public DbSet<Transaction> AccountIds { get; set; }
       
    }

   

}