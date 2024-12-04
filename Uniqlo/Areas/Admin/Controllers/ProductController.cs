using Microsoft.AspNetCore.Mvc;
using Uniqlo.DataAccess;
using Uniqlo.Models;
using Uniqlo.ViewModel.Products;
using Uniqlo.ViewModel.Sliders;

namespace Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(UniqloDbContext _sql,IWebHostEnvironment _env) : Controller
    {
        public IActionResult Index()
        {
            var data = _sql.Products.ToList();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = _sql.Categories.Where(x => !x.IsDeleted).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM sm)
        {
            if (sm.CoverFile != null)
            {
                if (!sm.CoverFile.ContentType.StartsWith("image"))
                {
                    ModelState.AddModelError("File", "Image deyil");

                }
                if (sm.CoverFile.Length > 3 * 1024 * 1024)
                {
                    ModelState.AddModelError("File", "Olcusu dogru deyil");
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _sql.Categories.Where(x => !x.IsDeleted).ToList();
                return View();
            }
         
                string FileName = Path.GetRandomFileName() + Path.GetExtension(sm.CoverFile.FileName);

                using (Stream s = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "Product", FileName)))
                {
                    await sm.CoverFile.CopyToAsync(s);
                }

            Product pm = new Product
            {
                Name = sm.Name,
                Description = sm.Description,
                CostPrice = sm.CostPrice,
                SellPrice = sm.SellPrice,
                Discount= sm.Discount,
                Quantity = sm.Quantity,
                CoverFile=FileName,
                CategoryId = sm.CategoryId,
            };

            await _sql.Products.AddAsync(pm);
            await _sql.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Products.FindAsync(id.Value);
            if (data == null) return NotFound();
            _sql.Products.Remove(data);
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Products.FindAsync(id.Value);
            if (data == null) return NotFound();
           data.IsDeleted = false;
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Products.FindAsync(id.Value);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
