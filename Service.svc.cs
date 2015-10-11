using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;             
using System.ServiceModel.Activation;
using System.Net;

namespace UserService
{
     
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)] 
    public class Service : IUserService
    {
        
        String connectionString = "Server=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|MyDB.mdf;Database=MyDBa;Trusted_Connection=Yes;User Instance=true;Integrated Security=true;";
           
        public User[] GetAllUsers()
        {   
            
            List<User> UserList =  new List<User>();   

            SqlDataReader data = null;
             using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {    
                    con.Open();
                    SqlCommand query = new SqlCommand("SELECT * FROM Users", con);     
                    data = query.ExecuteReader();
                    while (data.Read())
                    {
                        User user = new User();

                        user.fullname = data.GetString(1);
                        user.status = data.GetString(2);
                        user.id = data.GetSqlInt32(0).ToString();
                        UserList.Add(user);
                    }
                    data.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return new List<User>().ToArray();
                }                  

                
                   
               
            }                      
            return UserList.ToArray();
        }

        public string UpdateUser(User user)
        {


             
            SqlDataReader data = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string cmdStr = @"UPDATE Users SET fullname= @fullname, status= @status WHERE id = @id" ;   
                    SqlCommand command = new SqlCommand(cmdStr, con);
                    command.Parameters.AddWithValue("@fullname", user.fullname);
                    command.Parameters.AddWithValue("@status", user.status);
                    command.Parameters.AddWithValue("@id", user.id);
                    data = command.ExecuteReader();
                    if (data.RecordsAffected == 1)
                    {
                        data.Close();
                        return "success";
                    }
                    data.Close();
                    return "not affected";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return "error"; 
                }

                


            }             
           
        }


        public string DeleteUser(string id)
        {



            SqlDataReader data = null;

            using (SqlConnection con = new SqlConnection("Server=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|MyDB.mdf;Database=MyDBa;Trusted_Connection=Yes;User Instance=true;Integrated Security=true;"))
            {
                try
                {
                    con.Open();
                    string cmdStr = @"DELETE FROM Users WHERE id = @id";
                    SqlCommand command = new SqlCommand(cmdStr, con);       
                    command.Parameters.AddWithValue("@id", id);
                    data = command.ExecuteReader();
                    if (data.RecordsAffected == 1)
                    {
                        data.Close();
                        return "success";
                    }
                    data.Close();
                    return "not affected";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return "error";
                }
                                   


            }

        }


        public string AddUser(User user)
        {
            
            SqlDataReader data = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string cmdStr = "INSERT INTO Users (\"fullname\",\"status\") VALUES (@fullname,@status)";
                    SqlCommand command = new SqlCommand(cmdStr, con);
                    command.Parameters.AddWithValue("@fullname", user.fullname);
                    command.Parameters.AddWithValue("@status", user.status);  
                    data = command.ExecuteReader();
                    if (data.RecordsAffected == 1)
                    {
                        data.Close();
                        return "success";
                    }
                    data.Close();
                    return "not affected";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return "error";
                }               
            }

        }
    }
}
