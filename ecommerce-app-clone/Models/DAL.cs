using Azure;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ecommerce_app_clone.Models
{
    public class DAL
    {
        public Response _register(Users users, SqlConnection sqlConnect)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", sqlConnect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Type", "Pending");
            sqlConnect.Open();
            int i = cmd.ExecuteNonQuery();
            sqlConnect.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User registered successfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User registration fail";
            }
            return response;
        }

        public Response _login(Users users, SqlConnection sqlConnection)
        {

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("sp_login", sqlConnection);

            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Password", users.Password);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                users.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                users.FirstName = Convert.ToString(dt.Rows[0]["FristName"]);
                users.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                users.Email = Convert.ToString(dt.Rows[0]["Email"]);
                users.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is valid";
                response.users = users;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is invalid";
                response.users = null;
            }
            return response;
        }

        public Response _viewUser(Users users,SqlConnection sqlConnection)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("p_viewUser",sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ID",users.ID);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            Response response= new Response();  
            if (dt.Rows.Count > 0)
            {
                users.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                users.FirstName = Convert.ToString(dt.Rows[0]["FristName"]);
                users.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                users.Email = Convert.ToString(dt.Rows[0]["Email"]);
                users.Type = Convert.ToString(dt.Rows[0]["Type"]);
                users.Fund = Convert.ToDouble(dt.Rows[0]["Fund"]);
                users.CreateOn = Convert.ToDateTime(dt.Rows[0]["CreateOn"]);
                users.Password = Convert.ToString(dt.Rows[0]["Password"]);
                response.StatusCode = 200;
                response.StatusMessage = "User exists";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exists";
                response.users = users;
            }
            return response;

        }
        public Response _updateProfile(Users users, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("p_updateProfile", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            sqlConnection.Open();
            int i =cmd.ExecuteNonQuery();
            sqlConnection.Close();

         
            Response response = new Response();
            if (i > 0)
            {
              
                response.StatusCode = 200;
                response.StatusMessage = "Update success";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Update fail";
            }
            return response;

        }

        public Response _addToCart(Carts cart, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("p_addCarts", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MedicineID", cart.MedicineID);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice); 
            cmd.Parameters.AddWithValue("@UserID", cart.UserID);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            sqlConnection.Open();
            int i = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            Response response = new Response();
            if (i > 0)
            {

                response.StatusCode = 200;
                response.StatusMessage = "Add success";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Add fail";
            }
            return response;


        }
    }
}
