using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Configuration;
using System.Data;
using SampleCrudApplication.Models;

namespace SampleCrudApplication.Repository
{
    public class AccountRepository
    {
        private SqlConnection _connection;

        private void Connection()
        {
            string configConnection = ConfigurationManager.ConnectionStrings["GetDatabaseConnection"].ToString();
            _connection = new SqlConnection(configConnection);
        }
        //create service
        public bool AddAccountDetails(Account account)
        {
            Connection();
            SqlCommand command = new SqlCommand("AddAccountDetails", _connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@AccHolder", account.AccHolder);
            command.Parameters.AddWithValue("@AccNumber", account.AccNumber);
            command.Parameters.AddWithValue("@IFSCNumber", account.IFSCNumber);
            command.Parameters.AddWithValue("@Branch", account.Branch);
           
            _connection.Open();
            int i = command.ExecuteNonQuery();
            _connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get all details
        public List<Account> GetAllDetails()
        {
            Connection();
            List<Account> AccountList = new List<Account>();
            SqlCommand command = new SqlCommand("GetAccountDetails", _connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            _connection.Open();
            adapter.Fill(dataTable);
            _connection.Close();
            //binding the data
            foreach (DataRow dataRow in dataTable.Rows)
                AccountList.Add(
                    new Account
                    {
                        id = Convert.ToInt32(dataRow["id"]),
                        AccHolder = Convert.ToString(dataRow["AccHolder"]),
                        AccNumber = Convert.ToString(dataRow["AccNumber"]),
                        IFSCNumber = Convert.ToString(dataRow["IFSCNumber"]),
                        Branch = Convert.ToString(dataRow["Branch"])
                    }
                );
            return AccountList;
        }
        
        //Edit account
        public bool EditAccount(Account account)
        {
            Connection();
            SqlCommand command = new SqlCommand("UpdateAccountDetails", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", account.id);
            command.Parameters.AddWithValue("@AccHolder", account.AccHolder);
            command.Parameters.AddWithValue("@AccNumber", account.AccNumber);
            command.Parameters.AddWithValue("@IFSCNumber", account.IFSCNumber);
            command.Parameters.AddWithValue("@Branch", account.Branch);
            _connection.Open();
            int i = command.ExecuteNonQuery();
            _connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete 
        public bool DeleteDetails(int id)
        {
            Connection();
            SqlCommand command = new SqlCommand("DeleteAccountDetails", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("id", id);
            _connection.Open();
            int i = command.ExecuteNonQuery();
            _connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}