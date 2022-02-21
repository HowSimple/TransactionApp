using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Commerce_TransactionApp.Models
{
    public class TransactionDbService
    {
        private readonly TransactionContext _db;
        private readonly IConfiguration _configuration;
        public TransactionDbService(TransactionContext db)
        {
            var connection = _configuration.GetConnectionString("database");
            //_db = new TransactionContext<Transaction>(options =>options.UseSqlServer(connection));
            
        }
        public bool IsDatabaseConnected() {
            var connectionString = _configuration.GetConnectionString("database");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        public List<Transaction> GetAllTransactions()
        {

            if (IsDatabaseConnected())
            {
                using (var context = _db)
                {
                    return context.AccountId.ToList();
                }
            }
            else return null;
            //var accounts = from id in _transactionContext.AccountIds 
            //var transactions = _db.Set<Transaction>();

            
        }

    }
}
