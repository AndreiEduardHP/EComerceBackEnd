namespace Places.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }    
        public DateTime OrderDate { get; set; }
        public string CodBSR { get; set; }
        public int UserId { get; set; }
        public UserProfile User { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
