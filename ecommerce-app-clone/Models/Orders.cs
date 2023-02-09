namespace ecommerce_app_clone.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int OrderNo { get; set; }
        public int OrderTotal { get; set; }
        public int OrderStatus { get; set; }
    }
}
