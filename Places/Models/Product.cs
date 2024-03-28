using System.ComponentModel.DataAnnotations.Schema;

namespace Places.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "VARCHAR(MAX)")]
        public string ImageUrl { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }

        public int Stoc {  get; set; }

        public bool Available { get; set; }

        public int CreatedByUserId { get; set; }

        public string ProductCod {  get; set; }
        public string InternalClientCod { get; set; }

      
    }
}
