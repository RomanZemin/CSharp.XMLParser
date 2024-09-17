namespace CST.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public User? User { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
