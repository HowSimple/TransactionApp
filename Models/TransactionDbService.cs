using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;

namespace Commerce_TransactionApp.Models
{
    public class TransactionDbService
    {
        private readonly IConfiguration configuration;
        private string connectionString;
        private SqlConnection connection;
        private SqlCommand command;


        public TransactionDbService(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration.GetConnectionString("database");
        }

        public bool ConnectDatabase()
        {
            try
            {
                this.connection = new SqlConnection(connectionString);
                this.command = new SqlCommand(null, connection);
                return true;
            }
            catch(SqlException)
            {
                return false;
            }
        }
        public bool IsDatabaseConnected() {
            
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

        
        // Uses LoginProcedure
        public int Login(string user, string pass)
        {
                this.ConnectDatabase();
           
                this.connection.Open();
                this.command.CommandText = "EXECUTE LoginProcedure @username, @password;";

                const int username_varcharSize = 10;
                const int password_varcharSize = 15;

                //set up parameters
                SqlParameter username = this.command.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, username_varcharSize));
                SqlParameter password = this.command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar,password_varcharSize));
                //fill in paramaters
                username.Value = user;
                password.Value = pass;
                


                int userId = 0;
                
                try
                {
                // ExecuteScalar runs the command and returns only a single entry
                    var result = this.command.ExecuteScalar();
                      
                    if (result != null)
                        userId = int.Parse(result.ToString()); // converts returned ID to int

                }
                catch(SqlException) 
                {

                    userId = -404;
                }
                
                
                this.connection.Close();

                return userId;

            
        }

        // Uses ShowNotification
        public DataTable GetUserNotifications(int userIdInput)
        {

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                string storedProcedureName = "ShowNotificationProcedure";

                using (SqlCommand _cmd = new SqlCommand(storedProcedureName, _con))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userIdInput;

                    System.Data.DataTable notificationTable = new System.Data.DataTable("Notifications");
                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(notificationTable);
                    _con.Close();

                    return notificationTable;

                }
            }
        }
        // Uses NotificationProcedure
        public DataTable GetAllNotifications(int userIdInput)
        {
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                string storedProcedureName = "NotificationProcedure";

                using (SqlCommand _cmd = new SqlCommand(storedProcedureName, _con))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userIdInput;

                    System.Data.DataTable notificationTable = new System.Data.DataTable("Notifications");
                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(notificationTable);
                    _con.Close();

                    return notificationTable;

                }
            }
        }
        // Uses TransactionSummaryProcedure 
        public DataTable GetTransactionSummary(int userIdInput)
        {
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                string storedProcedureName = "TransactionSummaryProcedure";

                using (SqlCommand _cmd = new SqlCommand(storedProcedureName, _con))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userIdInput;

                    System.Data.DataTable customerTable = new System.Data.DataTable("Accounts");
                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(customerTable);
                    _con.Close();

                    return customerTable;

                }
            }
        }
        // GetAllTransactions may be a good reference for GetTransactionSummary

        public DataTable GetAllTransactions()
        {

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                string queryStatement = "SELECT * FROM dbo.UserTransactions";

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
            
        // Uses SelectNotification procedure
        public int SelectNotification(int userIDInput, int notificationIDInput)
        {
            int affectedRows;
            
            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE SelectNotification @userID, @userNotificationID;";

           

            //set up parameters
            SqlParameter userID = this.command.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int));
            SqlParameter userNotificationID = this.command.Parameters.Add(new SqlParameter("@userNotificationID", SqlDbType.Int));
            //fill in paramaters
            userID.Value = userIDInput;
            userNotificationID.Value = notificationIDInput;

            affectedRows = command.ExecuteNonQuery();

            return affectedRows;
        }
        // Uses UnselectNotification procedure
        public int UnselectNotification(int userIDInput, int notificationIDInput)
        {
            int affectedRows;

            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE UnselectNotification @userID, @userNotificationID;";



            //set up parameters
            SqlParameter userID = this.command.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int));
            SqlParameter userNotificationID = this.command.Parameters.Add(new SqlParameter("@userNotificationID", SqlDbType.Int));
            //fill in paramaters
            userID.Value = userIDInput;
            userNotificationID.Value = notificationIDInput;

            affectedRows = command.ExecuteNonQuery();

            return affectedRows;

        }

        public int AddNewTransaction(Transaction transaction)
        {
            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE TransactionProcedure @userID, @transactionAmount, @transactionLocation, @transactionType, @processingDate, transactionDescription;";

            //_cmd.Parameters.AddWithValue("@transactionid", transaction.transactionID);
            this.command.Parameters.AddWithValue("@balance", transaction.accountBalance);
            this.command.Parameters.AddWithValue("@amount", transaction.transactionAmount);
            this.command.Parameters.AddWithValue("@type", transaction.transactionType);
            this.command.Parameters.AddWithValue("@location", transaction.transactionLocation);
            this.command.Parameters.AddWithValue("@description", transaction.transactionDescription);
            this.command.Parameters.AddWithValue("@date", transaction.processingDate);
            this.command.Parameters.AddWithValue("@accountnumber", transaction.accountNumber);

            
            int rows_effected = this.command.ExecuteNonQuery();
            this.connection.Close();

            return rows_effected;

        }

    }
}
