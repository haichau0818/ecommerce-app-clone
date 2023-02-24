using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ecommerce_app_clone.Models;
using Microsoft.Data.SqlClient;

namespace ecommerce_app_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("addUpdateMedicine")]
        public Response addUpdateMedicine(Medicine medicine)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECOMMERCE").ToString());
            response = dal._updateMedicine(medicine, connection);
            return response;

        }
        [HttpGet]
        [Route("userList")]
        public Response userList(Users user)
        {
            Response response = new Response();
            DAL dal = new DAL();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("ECOMMERCE").ToString());
            response = dal._userList(user, connection);
            return response;

        }
    }
}
