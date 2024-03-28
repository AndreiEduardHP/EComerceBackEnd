namespace Places.Dto
{
    public class OrderDto
    {
     
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public string CodBSR { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }
}
