using HeritageWalksTrail.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HeritageWalksTrail.Models
{
    public class TrailContext
    {

        public string ConnectionString { get; set; }

        public TrailContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<TrailViewModel> GetAllTrails()
        {
            List<TrailViewModel> list = new List<TrailViewModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Trail", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TrailViewModel()
                        {
                            id          = Convert.ToInt32(reader["id"]),
                            name        = reader["name"].ToString(),
                            description = reader["description"].ToString(),
                            colorCode   = reader["colorCode"].ToString(),
                            photoName   = reader["photoName"].ToString(),
                            distance    = reader["distance"].ToString(),
                            totalTime   = reader["totalTime"].ToString()

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
        public TrailViewModel GetTrailById(int? id, TrailViewModel trail)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Trail where id=" + id, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trail.id = Convert.ToInt32(reader["id"]);
                        trail.name = reader["name"].ToString();
                        trail.description = reader["description"].ToString();
                        trail.colorCode = reader["colorCode"].ToString();
                        trail.photoName = reader["photoName"].ToString();
                        trail.distance = reader["distance"].ToString();
                        trail.totalTime = reader["totalTime"].ToString();
                    }
                }
                cmd.Dispose();
            }
            return trail;
        }

        public String InsertTrail(TrailViewModel trail)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string Query
                        = "INSERT INTO TRAIL (name, description, colorCode, photoName, distance, totalTime)" +
                          " VALUES ('" + trail.name + "','" + trail.description + "','" + trail.colorCode+ "','"
                                       + trail.photoName+ "','" + trail.distance+"','" + trail.totalTime+"')";
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

        public void UpdateTrailById(int? id, TrailViewModel trail)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string Query = "UPDATE TRAIL SET " +
                                    "name='"        + trail.name + "', " +
                                    "description='" + trail.description + "', " +
                                    "colorCode='"   + trail.colorCode + "', " +
                                    "photoName='"   + trail.photoName + "', " +
                                    "distance='"    + trail.distance + "', " +
                                    "totalTime='"   + trail.totalTime + "' " +
                                    "WHERE id="     + trail.id;
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }


        /// <summary>
        /// Case of using 'TRANSACTION'
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTrailById(int? id)
        {
            int returnValue = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                using (MySqlConnection conn1 = GetConnection())
                {
                    try
                    {
                        conn1.Open();
                        string Query = "DELETE FROM STOPS WHERE trail_id=" + id;
                        MySqlCommand cmd1 = new MySqlCommand(Query, conn1);
                        returnValue = cmd1.ExecuteNonQuery();

                        using (MySqlConnection conn2 = GetConnection())
                        {
                            try
                            {
                                conn2.Open();
                                Query = "DELETE FROM TRAIL WHERE id=" + id;
                                MySqlCommand cmd2 = new MySqlCommand(Query, conn2);
                                returnValue = 0;
                                returnValue = cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                // Display information that command2 failed.
                                Console.WriteLine("returnValue for command2: {0}", returnValue);
                                Console.WriteLine("Exception Message2: {0}", ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Display information that command2 failed.
                        Console.WriteLine("returnValue for command1: {0}", returnValue);
                        Console.WriteLine("Exception Message1: {0}", ex.Message);
                    }
                }
                scope.Complete();

            }
            if (returnValue > 0)
            {
                Console.WriteLine("Transaction was committed.");
            }
            else
            {
                // You could write additional business logic here, notify the caller by
                // throwing a TransactionAbortedException, or log the failure.
                Console.WriteLine("Transaction rolled back.");
            }

        }

        public TrailViewModel getColorCodeList(TrailViewModel trail)
        {
            List<SelectListItem> list = trail.colorCodeSelectList;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select DISTINCT(ColorCode) FROM Trail", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SelectListItem item = trail.colorCodeSelectList.Where(x => x.Value == reader["colorCode"].ToString()).Single();
                        if (trail.colorCode != reader["colorCode"].ToString())
                        {
                            list = list.Where(c => c.Value != reader["colorCode"].ToString()).ToList();
                        }
                    }
                }
                cmd.Dispose();
            }
            trail.colorCodeSelectList = list;

            return trail;
        }

    }
}
