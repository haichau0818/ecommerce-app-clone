namespace ecommerce_app_clone.Models
{
    public class Carts
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string MedicineID { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
