namespace ecommerce_app_clone.Models
{
    public class OrderItems
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int MedicineID { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public int Quatity { get; set; }
        public double TotalPrice { get; set; }
    }
}
