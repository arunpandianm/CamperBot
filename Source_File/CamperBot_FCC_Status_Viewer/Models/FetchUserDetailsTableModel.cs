using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class FetchUserDetailsTableModel
    {
        public static List<database_list> FetchUserDetailsTable()
        {
            List<database_list> tempList = new List<database_list>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=camperbot;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                // Select required user details from user table
                string sqlQuery = "SELECT uid, name, doj, uname, url, excluder, exc_date FROM user";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    database_list temp_data = new database_list()
                    {
                        uid = rdr["uid"].ToString(),
                        name = rdr["name"].ToString(),
                        doj = rdr["doj"].ToString(),
                        uname = rdr["uname"].ToString(),
                        url = rdr["url"].ToString(),
                        excluder = rdr["excluder"].ToString(),
                        exc_date = rdr["exc_date"].ToString()
                    };
                    tempList.Add(temp_data);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
            }
            connection.Close();
            return (tempList);
        }
    }

    // Template for list
    public class database_list
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string doj { get; set; }
        public string uname { get; set; }
        public string url { get; set; }
        public string excluder { get; set; }
        public string exc_date { get; set; }
    }
}
