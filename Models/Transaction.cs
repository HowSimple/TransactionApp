
using System;
using System.Collections.Generic;

namespace Commerce_TransactionApp.Models
{
    public class Transaction
    {
   
        public Transaction() { }

        public Transaction( string transactionType, DateTime processingDate, string transactionDescription, string transactionLocation, double transactionAmount)
        {
          
            this.transactionType = transactionType;
            this.processingDate = processingDate;
            this.transactionDescription = transactionDescription;
            this.transactionLocation = transactionLocation;
            this.transactionAmount = transactionAmount;
          
        }

       
        public string transactionType { get; set; }

        public DateTime processingDate { get; set; }
        public string transactionDescription { get; set; }

        public string transactionLocation { get; set; }

        public double transactionAmount { get; set; }

     



    }

}