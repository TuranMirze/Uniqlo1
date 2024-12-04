using System.ComponentModel.DataAnnotations.Schema;
using Uniqlo.ViewModel.Products;

namespace Uniqlo.Models
{
    public class Product : BaseEntitiy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }   
        public int Discount { get; set; }
        public string CoverFile { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<ProductImage> Image { get; set; }

        //public static implicit operator Product(ProductCreateVM vm)
        //{

        //    return new Product
        //    {
        //        Name = vm.Name,
        //        Description = vm.Description,
        //        CostPrice = vm.CostPrice,
        //        SellPrice = vm.SellPrice,
        //        Quantity = vm.Quantity,
        //        Discount = vm.Discount,
        //        CoverFile = vm.CoverFile,
        //        //CategoryId = vm.CategortId

        //    };


        //}
    }
}
