using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Uniqlo.Models;

namespace Uniqlo.DataAccess
{
    public class UniqloDbContext :IdentityDbContext<User>
    {
        public DbSet<Slider> Sliders {  get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<ProductImage> ProductImages {  get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-MJLOCD2;Database=UniqloDB;Trusted_Connection=True;TrustServerCertificate=True");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public UniqloDbContext(DbContextOptions opt) : base(opt) { }
    }
}
