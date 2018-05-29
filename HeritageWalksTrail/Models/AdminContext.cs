using HeritageWalksTrail.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeritageWalksTrail.Models
{
    public class AdminContext
    {

        public string ConnectionString { get; set; }

        public AdminContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }



        public List<Admin> GetAllAdmin()
        {
            List<Admin> list = new List<Admin>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Admin where id < 10", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Admin()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString()

                        });
                    }
                }
            }
            return list;
        }
    }
}
