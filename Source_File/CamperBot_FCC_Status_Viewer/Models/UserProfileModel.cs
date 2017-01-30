using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class UserProfileModel
    {
        public static List<user_profile_details> FetchUserProfileDetails(string userId)
        {
            List<user_profile_details> tempList = new List<user_profile_details>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=mini;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Select complete user information along with the points on each date using JOINS
                string sqlQuery = String.Format("SELECT user.uid, user.name, user.doj, user.uname, user.url, user.excluder, user.exc_date, daily_update.r_date, daily_update.points from user, daily_update WHERE user.uid= daily_update.uid AND user.uid = '{0}';", userId.Trim());
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user_profile_details temp_data = new user_profile_details()
                    {
                        uid = rdr["uid"].ToString(),
                        name = rdr["name"].ToString(),
                        doj = rdr["doj"].ToString(),
                        uname = rdr["uname"].ToString(),
                        url = rdr["url"].ToString(),
                        excluder = rdr["excluder"].ToString(),
                        exc_date = rdr["exc_date"].ToString(),
                        r_date = rdr["r_date"].ToString(),
                        points = rdr["points"].ToString(),
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
    public class user_profile_details
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string doj { get; set; }
        public string uname { get; set; }
        public string url { get; set; }
        public string excluder { get; set; }
        public string exc_date { get; set; }
        public string r_date { get; set; }
        public string points { get; set; }
    }
}