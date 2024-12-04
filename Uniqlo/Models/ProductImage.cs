namespace Uniqlo.Models
{
    public class ProductImage : BaseEntitiy
    {
        public string FileUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
