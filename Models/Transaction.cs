
using System;
using System.Collections.Generic;

namespace Commerce_TransactionApp.Models
{
    public class Transaction
    {
        public Transaction(int accountId, double transactionAmount, string transactionType, string transactionLocation, string transactionDescription, DateTime processingDate, double accountBalance)
        {
            this.accountId = accountId;
            this.transactionAmount = transactionAmount;
            this.transactionType = transactionType;
            this.transactionLocation = transactionLocation;
            this.transactionDescription = transactionDescription;
            this.processingDate = processingDate;
            this.accountBalance = accountBalance;
        }

        public int accountId {get; set;}
        public double transactionAmount { get; set; }
        public string transactionType { get; set; }
        public string transactionLocation { get; set; }
        public string transactionDescription { get; set; }
        public DateTime processingDate { get; set; } 
        public double accountBalance { get; set; }
        
        
    

    }

}