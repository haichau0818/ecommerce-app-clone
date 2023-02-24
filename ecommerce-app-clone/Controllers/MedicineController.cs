using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ecommerce_app_clone.Models;
using Microsoft.Data.SqlClient;

namespace ecommerce_app_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MedicineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Carts carts)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECOMMERCE").ToString());
            response = dal._addToCart(carts, connection);
            return response;

        }
        [HttpPost]
        [Route("placeOrder")]
        public Response placeOrder(Users user)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECOMMERCE").ToString());
            response = dal._placeOrder(user, connection);
            return response;

        }
        [HttpGet]
        [Route("userOrderList")]
        public Response userOrderList(Users user)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECOMMERCE").ToString());
            response = dal._userOrderList(user, connection);
            return response;

        }
    }
}
