﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;

namespace Commerce_TransactionApp.Models
{
    public class TransactionDbService
    {
        //private readonly TransactionContext _db;
        private readonly IConfiguration _configuration;
        private string connectionString;
        public TransactionDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("database");
            //System.Diagnostics.Debug.WriteLine("Test");
            System.Diagnostics.Debug.WriteLine(connectionString);




        }
        public bool IsDatabaseConnected() {
            //var connectionString = _configuration.GetConnectionString("database");
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
        public int AddNewTransaction(Transaction transaction) {
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                string queryStatement = "INSERT INTO dbo.Transactions VALUES (@transactionid,@type, @date, @description, @location,@amount, @accountnumber)";

                using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                {
                    _cmd.Parameters.AddWithValue("@transactionid", transaction.transactionID);
                    _cmd.Parameters.AddWithValue("@balance", transaction.accountBalance);
                    _cmd.Parameters.AddWithValue("@amount", transaction.transactionAmount);
                    _cmd.Parameters.AddWithValue("@type", transaction.transactionType);
                    _cmd.Parameters.AddWithValue("@location", transaction.transactionLocation);
                    _cmd.Parameters.AddWithValue("@description", transaction.transactionDescription);
                    _cmd.Parameters.AddWithValue("@date", transaction.processingDate);
                    _cmd.Parameters.AddWithValue("@accountnumber", transaction.accountNumber);


                    _con.Open();
                    int rows_effected = _cmd.ExecuteNonQuery();
                    _con.Close();

                    return rows_effected;

                }
            }
        }
        public DataTable GetAllTransactions()
        {

          
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "SELECT * FROM dbo.Transactions";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {
                    System.Data.DataTable customerTable = new System.Data.DataTable("Accounts");

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                        _con.Open();
                        _dap.Fill(customerTable);
                        _con.Close();
                     
                        return customerTable;

                    }
                }
           
           

            
        }

    }
}
