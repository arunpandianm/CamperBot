using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamperBot_FCC_Status_Viewer.Models
{
    public class YearlyChartModel
    {
        public static List<yearly_update_list> DatabaseConnection()
        {
            List<yearly_update_list> monthlyUpdateList = new List<yearly_update_list>();

            // Database Connection
            string connectionString = "server=localhost;user=root;database=camperbot;port=3306;password=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                // Select Total points from daily update table
                string sqlQuery = "SELECT u_date, pts_count, u_count FROM daily_count WHERE u_date LIKE '%12-31' ";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);

                // Generate the list with fetched data
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    yearly_update_list temp_data = new yearly_update_list()
                    {
                        u_date = rdr["u_date"].ToString(),
                        pts_count = rdr["pts_count"].ToString(),
                        u_count = rdr["u_count"].ToString(),

                    };
                    monthlyUpdateList.Add(temp_data);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
            }
            connection.Close();
            return (monthlyUpdateList);
        }
    }

    // Template for list
    public class yearly_update_list
    {
        public string u_date { get; set; }
        public string pts_count { get; set; }
        public string u_count { get; set; }

    }

}
