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
        private readonly IConfiguration _configuration;
        private string connectionString;
        public TransactionDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("database");
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



        public int Login(User user)
        {
            const int username_varcharSize = 10;
            const int password_varcharSize = 15;

      
            using (SqlConnection _con = new SqlConnection(connectionString))
            using (SqlCommand _cmd = new SqlCommand(null, _con))
            {
                _con.Open();
                _cmd.CommandText = "EXECUTE LoginProcedure @username, @password;";
                _cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, username_varcharSize));
                _cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar,password_varcharSize));
                //_cmd.Parameters.AddWithValue("@username", user.username);
                //_cmd.Parameters.AddWithValue("@password", user.password);

                //_cmd.Prepare();
                // _cmd.Parameters["@username"].Value = user.username;
                //_cmd.Parameters["@password"].Value = user.password;



                int userId = 0;
                var result = _cmd.ExecuteScalar();
                if (result != null)
                    userId = int.Parse(result.ToString());
                _con.Close();

                return userId;

            }
        }

        public int AddNewTransaction(Transaction transaction)
        {
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
