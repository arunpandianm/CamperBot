using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class DashboardChartModel
    {
        public static List<current_month_update_list> FetchCurrentMonthStatus()
        {
            List<current_month_update_list> tempList = new List<current_month_update_list>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=mini;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                // Select Total points from daily update table
                string sqlQuery = "SELECT u_date, pts_count, u_count FROM daily_count WHERE u_date LIKE '2016-10-%' ";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    current_month_update_list temp_data = new current_month_update_list()
                    {
                        u_date = rdr["u_date"].ToString(),
                        pts_count = rdr["pts_count"].ToString(),
                        u_count = rdr["u_count"].ToString(),

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
    public class current_month_update_list
    {
        public string u_date { get; set; }
        public string pts_count { get; set; }
        public string u_count { get; set; }

    }
}