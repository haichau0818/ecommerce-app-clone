using Azure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
                users.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
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
        public Response _placeOrder(Users user, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("p_placeOrder", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", user.ID);
 
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

        public Response _userOrderList(Users user, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_userOrderList", sqlConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Type", user.Type);
            da.SelectCommand.Parameters.AddWithValue("@ID", user.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            List<Orders> lstOrder = new List<Orders>();
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Orders orders = new Orders();
                    orders.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    orders.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                    orders.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                    orders.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                    lstOrder.Add(orders);

                }
                if (lstOrder.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "add success!";
                    response.listOrders = lstOrder;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "add fail!";
                    response.listOrders = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "add fail!";
                response.listOrders = null;
            }
            return response;
        }

        public Response _updateMedicine(Medicine medicine, SqlConnection sqlConnection)
        {
            SqlCommand cmd = new SqlCommand("p_updateMedicine", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", medicine.Name);
            cmd.Parameters.AddWithValue("@Manufaturer", medicine.Manufaturer);
            cmd.Parameters.AddWithValue("@UnitPrice", medicine.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", medicine.Discount);
            cmd.Parameters.AddWithValue("@Quantity", medicine.Quantity);
            cmd.Parameters.AddWithValue("@ExpDate", medicine.ExpDate);
            cmd.Parameters.AddWithValue("@ImageUrl", medicine.ImageUrl);
            cmd.Parameters.AddWithValue("@Status", medicine.Status);
            cmd.Parameters.AddWithValue("@Type", medicine.Type);
            sqlConnection.Open();
            int i = cmd.ExecuteNonQuery();
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

        public Response _userList(Users users, SqlConnection sqlConnection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_userList", sqlConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
        
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            List<Users> lstUser = new List<Users>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Users user = new Users();
                    user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    user.Password = Convert.ToString(dt.Rows[i]["Password"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                    user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                    user.CreateOn = Convert.ToDateTime(dt.Rows[i]["CreateOn"]);
                    lstUser.Add(user);

                }
                if (lstUser.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "add success!";
                    response.listUser = lstUser;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "add fail!";
                    response.listUser = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "add fail!";
                response.listUser = null;
            }
            return response;
            

        }

    }
}
