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
        private string connectionString;
        private SqlConnection connection;
        private SqlCommand command;


        public TransactionDbService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("database");
        }
        public TransactionDbService(string dbConnectionString)
        {
            connectionString = dbConnectionString;
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

        public int Register(string user, string pass, string location, int accountNumber)
        {
            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE RegisterProcedure @username, @location,  @password";

            const int username_varcharSize = 10;
            const int password_varcharSize = 15;
            const int state_varcharSize = 15;
            //set up parameters
            SqlParameter username = this.command.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, username_varcharSize));
            SqlParameter password = this.command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, password_varcharSize));
            SqlParameter state = this.command.Parameters.Add(new SqlParameter("@location", SqlDbType.VarChar, state_varcharSize));
            //SqlParameter accountNum = this.command.Parameters.Add(new SqlParameter("@account", SqlDbType.VarChar, username_varcharSize));
            //fill in paramaters
            username.Value = user;
            password.Value = pass;
            state.Value = location;
           // accountNum.Value = accountNumber;



            int userId = 0;

            try
            {
                // ExecuteScalar runs the command and returns only a single entry
                var result = this.command.ExecuteScalar();

                if (result != null)
                    userId = int.Parse(result.ToString()); // converts returned ID to int

            }
            catch (SqlException)
            {

                userId = -404;
            }


            this.connection.Close();

            return userId;

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
                string storedProcedureName = "ShowNotification";

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

       
     
        public int DeleteNotification(int userID, int userNotificationID)
        {
            int affectedRows;

            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE DeleteNotification @userID, @userNotificationID;";



            //set up parameters
            SqlParameter _userID = this.command.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int));
            SqlParameter _userNotificationID = this.command.Parameters.Add(new SqlParameter("@userNotificationID", SqlDbType.Int));
            //fill in paramaters
            _userID.Value = userID;
            _userNotificationID.Value = userNotificationID;

            affectedRows = command.ExecuteNonQuery();

            return affectedRows;
        }
      
        // Uses SelectNotification procedure
        public int SelectNotification(int userIDInput, int notificationRuleID, double triggerAmount)
        {
            int affectedRows;
            
            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE SelectNotification @userID, @ruleID,@triggerAmount;";

           

            //set up parameters
            SqlParameter userID = this.command.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int));
            SqlParameter NotificationID = this.command.Parameters.Add(new SqlParameter("@ruleID", SqlDbType.Int));
            SqlParameter _triggerAmount = this.command.Parameters.Add(new SqlParameter("@triggerAmount", SqlDbType.Int));
            //fill in paramaters
            userID.Value = userIDInput;
            NotificationID.Value = notificationRuleID;
            _triggerAmount.Value= triggerAmount;

            affectedRows = command.ExecuteNonQuery();

            return affectedRows;
        }
        // Uses UnselectNotification procedure
        public int UnselectNotification(int userIDInput, int notificationRuleID)
        {
            int affectedRows;

            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE UnselectNotification @userID, @ruleID;";



            //set up parameters
            SqlParameter userID = this.command.Parameters.Add(new SqlParameter("@userID", SqlDbType.Int));
            SqlParameter NotificationID = this.command.Parameters.Add(new SqlParameter("@ruleID", SqlDbType.Int));

            //fill in paramaters
            userID.Value = userIDInput;
            NotificationID.Value = notificationRuleID;

            affectedRows = command.ExecuteNonQuery();

            return affectedRows;

        }

        public int AddNewTransaction(Transaction transaction, int userId)
        {
            this.ConnectDatabase();

            this.connection.Open();
            this.command.CommandText = "EXECUTE TransactionProcedure @userID, @transactionAmount, @transactionLocation, @transactionType, @processingDate, @transactionDescription;";
            this.command.Parameters.AddWithValue("@userID", userId);

            
            this.command.Parameters.AddWithValue("@transactionAmount", transaction.transactionAmount);
            string transactionType;
            if (transaction.isDeposit)
                transactionType = "CR";
            else transactionType = "WD";

            this.command.Parameters.AddWithValue("@transactionType", transactionType);
            this.command.Parameters.AddWithValue("@transactionLocation", transaction.transactionLocation);
            this.command.Parameters.AddWithValue("@transactionDescription", transaction.transactionDescription);
            this.command.Parameters.AddWithValue("@processingDate", transaction.processingDate);


            
            int rows_effected = this.command.ExecuteNonQuery();
            this.connection.Close();

            return rows_effected;

        }


        public void PrintSummary(int userID) {
            DataTable transactionSummary;

            transactionSummary = GetTransactionSummary(userID);

            transactionSummary.WriteXml("wwwroot/Transaction Summary.xml");
            
   

           
        }

    }
}
