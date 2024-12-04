using Microsoft.AspNetCore.Mvc;
using Uniqlo.DataAccess;
using Uniqlo.Models;
using Uniqlo.ViewModel.Category;

namespace Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(UniqloDbContext _sql) : Controller
    {
        public IActionResult Index()
        {
            var data = _sql.Categories.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryCreateVM cm)
        {
            if (ModelState.IsValid) return BadRequest();
            Category c = new Category
            {
                Name = cm.Name,
            };
            _sql.Categories.Add(c);
            _sql.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Categories.FindAsync(id.Value);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Categories.FindAsync(id.Value);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _sql.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Categories.FindAsync(id.Value);
            if (data == null) return NotFound();
            _sql.Categories.Remove(data);
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

//[HttpPost]
//public IActionResult Update(CategoryCreateVM cm)
//{
//    if (ModelState.IsValid) return BadRequest();
//    Category c = new Category
//    {
//        Name = cm.Name,
//    };
//    _sql.Categories.Add(c);
//    _sql.SaveChanges();
//    return View();
//}

