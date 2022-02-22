
using System;
using System.Collections.Generic;

namespace Commerce_TransactionApp.Models
{
    public class Transaction
    {

        public int accountId {get; set;}
        public double transactionAmount { get; set; }
        public string transactionType { get; set; }
        public string transactionLocation { get; set; }
        public string transactionDescription { get; set; }
        public DateTime processingDate { get; set; } 
        public double accountBalance { get; set; }

    

    }

}