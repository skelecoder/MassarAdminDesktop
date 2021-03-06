﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MassarAdminDesktop
{
    public class DBConnect
    {
        public static MySqlConnection connection;

        //Constructor
        public DBConnect()
        {
            Initialize();

        }

        private void Initialize()
        {
            connection = new MySqlConnection(Option.stringConnection);
            
            OpenConnection();
        }

        private static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static bool checkConnection()
        {
            if (connection.State.ToString() == "Open")
                return true;
            CloseConnection();
            OpenConnection();
            if (connection.State.ToString() == "Open")
                return true;
            return false;





        }

        public static bool Post(string query)
        {
            
            MySqlCommand cmd = new MySqlCommand(query, connection);
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch  
            {
                CloseConnection();
                OpenConnection();
                cmd.ExecuteNonQuery();
                return true;
            }
            return true;
        }
        public static MySqlDataReader Gets(string query)
        {
            
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                return cmd.ExecuteReader();
            }
            catch 
            {
                CloseConnection();
                OpenConnection();
                return cmd.ExecuteReader();
            }
        }

        public static string Get(string query)
        {
            string rt="";
            MySqlDataReader r = Gets(query);
            if(r.Read())
                 rt = r[0].ToString();
            r.Close();
            return rt;
        }

    }

}
