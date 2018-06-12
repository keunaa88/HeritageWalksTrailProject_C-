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


        public List<AdminViewModel> GetAllAdmin()
        {
            List<AdminViewModel> list = new List<AdminViewModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Admin", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdminViewModel()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            adminId = reader["adminId"].ToString(),
                            password = reader["password"].ToString(),
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString(),
                            role = reader["role"].ToString()

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
        /// <param name="id"></param> it can be null
        /// <returns></returns>
        public AdminViewModel GetAdminById(int? id) 
        {

            AdminViewModel admin = new AdminViewModel();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Admin where id="+id, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admin.id = Convert.ToInt32(reader["id"]);
                        admin.adminId = reader["adminId"].ToString();
                        admin.firstName = reader["firstName"].ToString();
                        admin.lastName = reader["lastName"].ToString();
                        admin.role = reader["role"].ToString();
                    }
                }
                cmd.Dispose();
            }
            return admin;
        }

        public String InsertAdmin(AdminViewModel admin)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try{
                    conn.Open();
                    string Query = "INSERT INTO ADMIN (adminId, password, firstName, lastName, role) VALUES" +
                                   "('" + admin.adminId + "','" + admin.password + "','" + admin.firstName + "','"
                                        + admin.lastName + "','" + admin.role + "');";
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
                    Console.WriteLine("Can not open connection ! : " +ex.Message);
                    if(ex.Number == 1062) //duplicated exception
                        return "Duplicated";

                    return "Fail";
                }
            }
        }

        public void UpdateAdminById(int? id, AdminViewModel admin)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string Query = "UPDATE ADMIN SET " +
                                    "password='" + admin.newPassword + "', " +
                                    "firstName='" + admin.firstName + "', " +
                                    "lastName='" + admin.lastName + "', " +
                                    "role='" + admin.role + "' WHERE id=" + admin.id;
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }


        public void DeleteAdminById(int? id)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string Query = "DELETE FROM ADMIN WHERE id=" + id;
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }

        public bool CheckPassword(int id, String password)
        {
            bool IsCorrectPassword = false;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select * FROM ADMIN WHERE ID="+id+" AND PASSWORD='"+password+"'" , conn);


                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        IsCorrectPassword = true;
                    }
                }
                cmd.Dispose();
            }

            return IsCorrectPassword;
        }
    }
}
