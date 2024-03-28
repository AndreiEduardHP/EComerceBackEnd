namespace Places.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Details { get; set; }
        public string ContactPhoneNumber { get; set; }
        public bool IsDisabled { get; set; }

        public int UserId { get; set; }
        public UserProfile User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
