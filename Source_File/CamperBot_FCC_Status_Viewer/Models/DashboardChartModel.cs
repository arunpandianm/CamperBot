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
                string sqlQuery = "SELECT u_date, pts_count, u_count FROM daily_count";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    current_month_update_list temp_data = new current_month_update_list()
                    {
                        u_date = rdr["u_date"].ToString(),
                        pts_count = rdr["pts_count"].ToString(),
                        u_count = rdr["u_count"].ToString()

                    };
                    tempList.Add(temp_data);
                }
                rdr.Close();
                
                //Find the Inactive user and append to same list
                // Find count of Inactive user
                string sqlQuery1 = "select count(*) as i_user from daily_update where points = '0' AND r_date = '2017-02-01'; ";
                MySqlCommand cmd1 = new MySqlCommand(sqlQuery1, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    current_month_update_list temp_data = new current_month_update_list()
                    {
                        u_date = "InactiveUser",
                        pts_count = "InactiveUser",
                        u_count = rdr1["i_user"].ToString()

                    };
                    tempList.Add(temp_data);
                }
                rdr1.Close();
                //end find inactive user
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