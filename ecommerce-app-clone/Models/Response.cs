using System.Collections.Generic;

namespace ecommerce_app_clone.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public List<Users> listUser { get; set; } 

        public Users users { get; set; }

        public List<Medicine> listMedicine { get; set; }

        public Medicine medicines { get; set; }

        public List<Orders> listOrders{ get; set; }

        public Orders orders { get; set; }
        public List<Carts> listCarts { get; set; }

        public Orders carts { get; set; }
        public List<OrderItems> listOrderItems { get; set; }

        public Orders orderItems { get; set; }



        }
}
