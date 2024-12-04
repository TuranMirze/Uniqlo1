using Uniqlo.ViewModel.Products;
using Uniqlo.ViewModel.Sliders;

namespace Uniqlo.ViewModel.Common
{
    public class HomeVM
    {
        public IEnumerable<SliderItemVM> Sliders { get; set; }
        public IEnumerable<ProductItemVM> Products { get; set; }

    }
}
