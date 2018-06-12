using HeritageWalksTrail.Controllers;
using HeritageWalksTrail.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeritageWalksTrail.Models
{
    public class StopsContext
    {

        public string ConnectionString { get; set; }

        public StopsContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<StopsViewModel> GetAllStops()
        {
            List<StopsViewModel> list = new List<StopsViewModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Stops", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StopsViewModel()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name        = reader["name"].ToString(),
                            description = reader["description"].ToString(),
                            photoName   = reader["photoName"].ToString(),
                            timeSpent   = reader["timeSpent"].ToString(),
                            latitude    = reader["latitude"].ToString(),
                            longitude   = reader["longitude"].ToString(),
                            audioName   = reader["audioName"].ToString(),
                            trail_id    = Convert.ToInt32(reader["trail_id"]),
                            admin_id    = Convert.ToInt32(reader["admin_id"])

                        });
                    }
                }
                cmd.Dispose();
            }

            return list;
        }

        /// <summary>
        /// To get data to edit 
        /// </summary>
        /// <param name = "id" ></ param > it can be null
        /// <returns></returns>
        public StopsViewModel GetStopById(int? id)
        {

            StopsViewModel stop = new StopsViewModel();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Stops where id=" + id, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stop.id = Convert.ToInt32(reader["id"]);
                        stop.name = reader["name"].ToString();
                        stop.description = reader["description"].ToString();
                        stop.photoName = reader["photoName"].ToString();
                        stop.timeSpent = reader["timeSpent"].ToString();
                        stop.latitude = reader["latitude"].ToString();
                        stop.longitude = reader["longitude"].ToString();
                        stop.audioName = reader["audioName"].ToString();
                        stop.trail_id = Convert.ToInt32(reader["trail_id"]);
                        stop.admin_id = Convert.ToInt32(reader["admin_id"]);
                    }
                }
                cmd.Dispose();
            }
            return stop;
        }

        public String InsertStop(StopsViewModel stop)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string Query
                        = "INSERT INTO STOPS (name, description, photoName, timeSpent, latitude, longitude, audioName, trail_id, admin_id)" +
                          " VALUES ('" + stop.name + "','" + stop.description + "','" + stop.photoName + "','"
                               + stop.timeSpent + "','" + stop.latitude + "','" + stop.longitude + "','"
                               + stop.audioName + "'," + stop.trail_id + "," + stop.admin_id + ")";
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    int result = cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    // Check Error
                    if (result < 0)
                        return "Fail";
                    else
                        return "Insert";
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Can not open connection ! : " + ex.Message);
                    return "Fail";
                }
            }
        }

        public void UpdateStopById(int? id, StopsViewModel stop)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string Query = "UPDATE STOPS SET " +
                                    "name='"        + stop.name + "', " +
                                    "description='" + stop.description + "', " +
                                    "photoName='"   + stop.photoName + "', " +
                                    "timeSpent='"   + stop.timeSpent + "', "+
                                    "latitude='"    + stop.latitude + "', " +
                                    "longitude='"   + stop.longitude + "', " +
                                    "audioName='"   + stop.audioName + "' " +
                                    "WHERE id="     + stop.id;
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }


        public void DeleteStopById(int? id)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string Query = "DELETE FROM STOPS WHERE id=" + id;
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }

        public List<SelectListItem> GetAllTrailsSelectListItem()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Trail", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SelectListItem()
                        {
                            Value = reader["id"].ToString(),
                            Text = reader["name"].ToString()
                        });
                    }
                }
                cmd.Dispose();
            }
            return list;
        }
    }
}
