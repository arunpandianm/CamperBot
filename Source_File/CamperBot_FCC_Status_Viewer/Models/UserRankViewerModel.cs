using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class UserRankViewerModel
    {
        public static List<rank_list> GenerateRankList()
        {
            List<rank_list> tempList = new List<rank_list>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=mini;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Select user based on rank order
                string sqlQuery = "SELECT user.uid, user.name, user.uname, user.url, user_rank_list.points FROM user, user_rank_list WHERE user.uid = user_rank_list.uid order by user_rank_list.points DESC;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rank_list temp_data = new rank_list()
                    {
                        uid = rdr["uid"].ToString(),
                        name = rdr["name"].ToString(),
                        uname = rdr["uname"].ToString(),
                        url = rdr["url"].ToString(),
                        //rank = rdr["rank"].ToString(),
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
    public class rank_list
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string uname { get; set; }
        public string url { get; set; }
        //public string rank { get; set; }
        public string points { get; set; }
    }
}
