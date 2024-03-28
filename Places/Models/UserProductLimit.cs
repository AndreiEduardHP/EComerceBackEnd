using System.ComponentModel.DataAnnotations.Schema;

namespace Places.Models
{
    public class UserProductLimit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }

        [ForeignKey("UserId")]
        public UserProfile User { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
