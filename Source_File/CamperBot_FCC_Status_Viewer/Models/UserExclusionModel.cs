using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class UserExclusionModel
    {
        //Add user to excluder list
        public static void AddUserExclusion(string userName)
        {
            // Database Connection
            string connectionString = "server=localhost;user=root;database=camperbot;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Update the excluder column to 'Y'
                string sqlQuery = String.Format("UPDATE user SET excluder = 'Y', exc_date = NOW() WHERE uname = '{0}';", userName.Trim());
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                //Just execute the query no need to generate list
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            connection.Close();
        }

        //Remove user from excluder list
        public static void RemoveUserExclusion(String userName)
        {
            // Database Connection
            string connectionString = "server=localhost;user=root;database=camperbot;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Update the excluder column to 'Y'
                string sqlQuery = String.Format("UPDATE user SET excluder = 'N', exc_date = NOW() WHERE uname = '{0}';", userName.Trim());
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                //Just execute the query no need to generate list
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            connection.Close();
        }
    }
}