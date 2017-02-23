using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class ActiveUserModel
    {
        public static List<active_user_list> GenerateActiveUserList_today()
        {
            List<active_user_list> tempList = new List<active_user_list>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=camperbot;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Select user based on rank order
                string current_date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                string sqlQuery = String.Format("SELECT user.uid, user.name, user.uname, user.url, daily_update.points FROM user, daily_update WHERE user.uid = daily_update.uid AND daily_update.r_date = '{0}';", current_date);
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    active_user_list temp_data = new active_user_list()
                    {
                        uid = rdr["uid"].ToString(),
                        name = rdr["name"].ToString(),
                        uname = rdr["uname"].ToString(),
                        url = rdr["url"].ToString(),
                        points = rdr["points"].ToString()
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

        public static List<active_user_list1> GenerateActiveUserList_month()
        {
            List<active_user_list1> tempList = new List<active_user_list1>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=camperbot;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Select user based on rank order
                string current_month = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM") + "-%";
                //string sqlQuery = "SELECT distinct(user.uid), user.name, user.uname, user.url FROM user, daily_update WHERE user.uid = daily_update.uid AND(daily_update.r_date like '" + current_month + "' AND daily_update.points != 0);";
                string sqlQuery = String.Format("SELECT distinct(user.uid), user.name, user.uname, user.url FROM user, daily_update WHERE user.uid = daily_update.uid AND (daily_update.r_date like '{0}' AND daily_update.points != 0);", current_month);
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    active_user_list1 temp_data = new active_user_list1()
                    {
                        uid = rdr["uid"].ToString(),
                        name = rdr["name"].ToString(),
                        uname = rdr["uname"].ToString(),
                        url = rdr["url"].ToString(),
                       // points = rdr["points"].ToString()
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
    public class active_user_list
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string uname { get; set; }
        public string url { get; set; }
        public string points { get; set; }
    }
    public class active_user_list1
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string uname { get; set; }
        public string url { get; set; }
        //public string points { get; set; }
    }
}
