using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Uniqlo.DataAccess;
using Uniqlo.Models;
using Uniqlo.ViewModel.Common;
using Uniqlo.ViewModel.Products;
using Uniqlo.ViewModel.Sliders;

namespace Uniqlo.Controllers
{
    public class HomeController(UniqloDbContext _sql) : Controller
    {
        public async Task<IActionResult> Index()
        {
            HomeVM hm = new ();
            hm.Sliders = await _sql.Sliders
               .Where(x => !x.IsDeleted)
               .Select(x => new SliderItemVM
               {
                Title = x.Title,
                Subtitle = x.Subtitle,
                Link = x.Link,
                ImageUrl = x.ImageUrl,
            }).ToListAsync();

            hm.Products = await _sql.Products
               .Where(x => !x.IsDeleted)
               .Select(x => new ProductItemVM
               {
                Name = x.Name,
                Discount = x.Discount,
                Price = x.SellPrice,
                IsInStock=x.Quantity > 0,
               ImageURL=x.CoverFile,
            }).ToListAsync();
            return View(hm);
            
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
