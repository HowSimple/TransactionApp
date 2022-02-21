﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Commerce_TransactionApp.Models
{
    public class TransactionDbService
    {
        //private readonly TransactionContext _db;
        private readonly IConfiguration _configuration;
        private string connectionString;
        public TransactionDbService(IConfiguration configuration)
        {
            _configuration = _configuration;
            connectionString = _configuration.GetConnectionString("database");
            
            //_db = new TransactionContext<Transaction>(options =>options.UseSqlServer(connection));

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
        public DataTable GetAllTransactions()
        {

          
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "SELECT * FROM dbo.Transactions e ORDER BY account_id";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {
                        DataTable customerTable = new DataTable("Accounts");

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