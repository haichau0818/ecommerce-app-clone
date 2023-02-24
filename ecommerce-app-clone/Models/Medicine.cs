using System;

namespace ecommerce_app_clone.Models
{
    public class Medicine
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Manufaturer { get; set; }

        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpDate { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }

    }
}
