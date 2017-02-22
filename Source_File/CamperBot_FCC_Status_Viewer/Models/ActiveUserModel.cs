using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class ActiveUserModel
    {
        public static List<active_user_list> GenerateActiveUserList()
        {
            List<active_user_list> tempList = new List<active_user_list>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=camperbot;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Select user based on rank order
                string sqlQuery = "SELECT user.uid, user.name, user.uname, user.url, daily_update.points FROM user, daily_update WHERE user.uid = daily_update.uid AND daily_update.r_date = '2017-02-17';";
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
}
