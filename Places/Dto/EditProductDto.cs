using System.ComponentModel.DataAnnotations.Schema;

namespace Places.Dto
{
    public class EditProductDto
    {
   

        public string Name { get; set; }

       
        public string ImageUrl { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }

        public int Stoc { get; set; }

        public bool Available { get; set; }

       

        public string ProductCod { get; set; }
        public string InternalClientCod { get; set; }

    }
}
