using Entity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace DALC

{
    public class DALC
    {
        string connectionString = @"Data Source=DESKTOP-7SH3LGT\SQLS1;Database=todo;user Id=sa;password=sqls1";

        public User Get_User_By_Email_Address(string email, string password)

        {
            User u = new User();

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                _con.Open();


                using (SqlCommand _cmd = new SqlCommand("UPG_GET_USER_BY_EMAIL", _con))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("@P__EMAIL_ADDRESS", email);

                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            u.user_id = reader.GetInt32("user_id");
                            u.email = reader.GetString("email"); ;
                            u.password = reader.GetString("password"); ;
                        }
                    }
                    else
                    {
                        reader.Close();
                        _con.Close();
                        AddNewUser(email, password, u);
                    }
                }
            }
            return u;
        }
        private void AddNewUser(string email, string password, User u)
        {
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                _con.Open();

                using (SqlCommand _cmd = new SqlCommand("INSERT INTO [todo].[dbo].[User] ([email],[password])  OUTPUT INSERTED.user_id VALUES (@Email,@password)", _con))
                {
                    _cmd.Parameters.AddWithValue("@Email", email); //password
                    _cmd.Parameters.AddWithValue("@password", password);
                    u.user_id = (int)_cmd.ExecuteScalar();
                    u.email = email;
                    u.password = password;


                }
            }
        }



        public List<todo> Get_Tasks_By_User_Id(int user_id)
        {
            List<todo> todos = new List<todo>();

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                _con.Open();

            using ( SqlCommand _cmd = new SqlCommand("GET_TASKS", _con))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("@PUSERID", user_id);
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read()) { 
                    todo t = new Entity.todo();
                        t.todo_id = reader.GetInt32("todo_id"); 
                        t.prefix = reader.GetString("prefix"); 
                        t.title = reader.GetString("title");
                        t.duedate = reader.GetDateTime("duedate");
                        t.category = reader.GetString("category");
                        t.estimate = reader.GetInt32("estimate"); 
                        t.EstimateUnit = reader.GetString("EstimateUnit");
                        t.importance = reader.GetString("importance");
                        todos.Add(t);
                    }
                    reader.Close();



                }
            _con.Close();


            }
            return todos;
        }

        public void Edit_Task(int todo_id,string prefix, string title, DateTime duedate,string category , int estimate, string EstimateUnit, string importance)
        {
        
        using(SqlConnection _cn = new SqlConnection(connectionString))
            {
                _cn.Open(); 


        using(SqlCommand cmd = new SqlCommand("EDIT_TODO", _cn))
                {
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@PID", todo_id);
                   cmd.Parameters.AddWithValue("@PPREFIX", prefix);
                   cmd.Parameters.AddWithValue("@PTITLE", title);
                   cmd.Parameters.AddWithValue("@PDUEDATE", duedate);
                   cmd.Parameters.AddWithValue("@PCATEGORY", category);
                   cmd.Parameters.AddWithValue("@PESTIMATE", estimate);
                   cmd.Parameters.AddWithValue("@PESTIMATEUNIT", EstimateUnit);
                   cmd.Parameters.AddWithValue("@IMPORTANCE", importance);
                    cmd.ExecuteNonQuery();
                }

            }

        }


        public void Add_Task(string prefix, string title, DateTime duedate, string category, int estimate, string EstimateUnit, string importance, int user_id)
        {
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                _con.Open();

                using (SqlCommand cmd = new SqlCommand("ADD_TODO", _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PPREFIX", prefix);
                    cmd.Parameters.AddWithValue("@PTITLE", title);
                    cmd.Parameters.AddWithValue("@PDUEDATE", duedate);
                    cmd.Parameters.AddWithValue("@PCATEGORY", category);
                    cmd.Parameters.AddWithValue("@PESTIMATE", estimate);
                    cmd.Parameters.AddWithValue("@PESTIMATEUNIT", EstimateUnit);
                    cmd.Parameters.AddWithValue("@IMPORTANCE", importance);
                    cmd.Parameters.AddWithValue("@User_id", user_id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

       

        public void Change_Status(int todo_id, string prefix)
        {

            using (SqlConnection _cn = new SqlConnection(connectionString))
            {
                _cn.Open();


                using (SqlCommand cmd = new SqlCommand("CHANGE_STATUS", _cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PID", todo_id);
                    cmd.Parameters.AddWithValue("@PPREFIX", prefix);
                   
                    cmd.ExecuteNonQuery();
                }

            }

        }


    }
}
