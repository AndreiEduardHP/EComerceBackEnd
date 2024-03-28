namespace Places.Dto
{
    public class OrderInfoDto
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public UserProfileDto Customer { get; set; }
        public AddressDto Address { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
