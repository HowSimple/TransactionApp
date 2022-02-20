using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Commerce_TransactionApp.Models
{
    public class TransactionDbService
    {
        private readonly TransactionContext _db;
        public TransactionDbService(TransactionContext db)
        {
            _db = db;
            
        }
       
        public List<Transaction> GetAllTransactions()
        {
            //var accounts = from id in _transactionContext.AccountIds 
            //var transactions = _db.Set<Transaction>();

            using (var context = _db)
            {
                return context.AccountIds.ToList();
            }
        }

    }
}
