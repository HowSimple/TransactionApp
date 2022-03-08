
using System;
using System.Collections.Generic;

namespace Commerce_TransactionApp.Models
{
    public class Transaction
    {
   
        public Transaction() { }

        public Transaction(int transactionID, string transactionType, DateTime processingDate, string transactionDescription, string transactionLocation, double transactionAmount, int accountNumber, double accountBalance)
        {
            this.transactionID = transactionID;
            this.transactionType = transactionType;
            this.processingDate = processingDate;
            this.transactionDescription = transactionDescription;
            this.transactionLocation = transactionLocation;
            this.transactionAmount = transactionAmount;
            this.accountNumber = accountNumber;
            this.accountBalance = accountBalance;
        }

        public int transactionID { get; set; }
        public string transactionType { get; set; }

        public DateTime processingDate { get; set; }
        public string transactionDescription { get; set; }

        public string transactionLocation { get; set; }

        public double transactionAmount { get; set; }

        public int accountNumber { get; set; }
        public double accountBalance { get; set; }




    }

}