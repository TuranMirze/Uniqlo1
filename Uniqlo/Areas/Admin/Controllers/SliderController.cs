using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo.DataAccess;
using Uniqlo.Models;
using Uniqlo.ViewModel.Sliders;

namespace Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(UniqloDbContext _sql, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var a = await _sql.Sliders.ToListAsync();
            return View(a);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = _sql.Categories.Where(x => !x.IsDeleted).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM sm)
        {
            if (sm.File != null)
            {
                if (!sm.File.ContentType.StartsWith("image"))
                {
                    ModelState.AddModelError("File", "Image deyil");

                }
                if (sm.File.Length > 3 * 1024 * 1024)
                {
                    ModelState.AddModelError("File", "Olcusu dogru deyil");
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _sql.Categories.Where(x => !x.IsDeleted).ToList();
                return View();
            }

            string FileName = Path.GetRandomFileName() + Path.GetExtension(sm.File.FileName);

            using (Stream s = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "Slider", FileName)))
            {
                await sm.File.CopyToAsync(s);
            }

            Slider slider = new Slider
            {
                ImageUrl = FileName,
                Title = sm.Title,
                Subtitle = sm.Subtitle,
                Link = sm.Link
            };


            await _sql.Sliders.AddAsync(slider);
            await _sql.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Sliders.FindAsync(id.Value);
            if(data ==null) return NotFound();
             _sql.Sliders.Remove(data);
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Sliders.FindAsync(id.Value);
            if (data == null) return NotFound();
            data.IsDeleted = false;
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _sql.Sliders.FindAsync(id.Value);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _sql.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
