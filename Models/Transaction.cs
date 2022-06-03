
using System;

namespace TransactionApp.Models
{
    public class Transaction
    {

        public Transaction() { }

        public Transaction(bool isDeposit, DateTime processingDate, string transactionDescription, string transactionLocation, double transactionAmount)
        {

            this.isDeposit = isDeposit;
            this.processingDate = processingDate;
            this.transactionDescription = transactionDescription;
            this.transactionLocation = transactionLocation;
            this.transactionAmount = transactionAmount;

        }


        public bool isDeposit { get; set; }

        public DateTime processingDate { get; set; }
        public string transactionDescription { get; set; }

        public string transactionLocation { get; set; }

        public double transactionAmount { get; set; }





    }

}