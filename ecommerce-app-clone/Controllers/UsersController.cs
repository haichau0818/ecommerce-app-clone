using ecommerce_app_clone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
//using System.Data.SqlClient;
namespace ecommerce_app_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        [HttpPost]
        [Route("registration")]
        public Response register(Users users)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection =new SqlConnection(_configuration.GetConnectionString("ECOMMERCE"));
            response = dal._register(users, connection);
            return response;
        }
        [HttpPost]
        [Route("login")]
        public Response login(Users users) { 
            DAL aL= new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString(""));
            Response response= aL._login(users, connection);
            return response;

        }
        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(Users users)
        {
            DAL aL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString(""));
            Response response = aL._viewUser(users, connection);
            return response;

        }

        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users users)
        {
            DAL aL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString(""));
            Response response = aL._updateProfile(users, connection);
            return response;

        }
        [HttpPost]
        [Route("addToCarts")]
        public Response addToCarts(Carts cart)
        {
            DAL aL = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString(""));
            Response response = aL._addToCart(cart, connection);
            return response;

        }

    }
}
