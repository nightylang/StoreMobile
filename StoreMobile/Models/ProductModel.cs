using System.ComponentModel.DataAnnotations;

namespace StoreMobileV2.Models
{
    public class ProductModel
    {
        [Key]
        public int ID_product { get; set; }
        public string id_name { get; set; } = string.Empty;
        public string product_name { get; set; } = string.Empty;
        public string product_description { get; set; } = string.Empty;
        public decimal product_price { get; set; } = 0;
        public int qty { get; set; }
        public string id_category { get; set; }
        public string id_model { get; set; }
        public string? id_image { get; set; } = "";
    }
}
