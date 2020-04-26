using FinalProject.Models.Entities;
using FinalProject.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class ApplicationDbContextSeed
    {
        private static ApplicationDbContext context;
        public async static Task SeedAsync(ApplicationDbContext _context, int? retry = 0)
        {
            context = _context;

            int retryForAvailability = retry.Value;
            try
            {
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        GetPreconfiguredCategories());

                    await context.SaveChangesAsync();
                }


                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        GetPreconfiguredItems());

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    await SeedAsync(context, retryForAvailability);
                }
                throw;
            }
        }
        static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category("Clothes"),
                new Category("Kitchen Items"),
                new Category("Accessories"),
                new Category("Tablets"),
                new Category("Mobile Phones"),
                new Category("Laptops"),
            };
        }
        static IEnumerable<Product> GetPreconfiguredItems()
        {
            var vendor1 = context.Users.FirstOrDefault(u => u.UserName == "vendor1@gmail.com");
            var vendor2 = context.Users.FirstOrDefault(u => u.UserName == "vendor2@gmail.com");
            return new List<Product>()
            {
                new Product("Huawei Media Pad T5", "0.1 inch, 16G, 2 GB Ram, 4G LTE - Black", 3600, 10,"13.png", 4, vendor1.Id),
                new Product("Lenovo Tab 7 TB-7304i", "7 Inch, 16GB, 1GB RAM, 3G, Slate Black", 1500, 0,"14.png", 4, vendor1.Id),
                new Product("Realme 6 Dual SIM", "128GB, 8GB RAM, 4G LTE - Comet Blue", 4500, 0,"15.png", 5, vendor1.Id),
                new Product("Asus G731GT-AU058T", "17.3-inch Full HD, Intel core i7-9750H, 1TB HDD and 256GB SSD, 16 GB RAM, NVIDIA GeForce GTX 1650 4 GB GDDR5, Windows - Black", 24777, 5,"16.png", 6, vendor1.Id),
                new Product("Black Sweatshirt", "Nice black sweatshirt comes in all sizes.", 300, 20, "1.png", 1, vendor2.Id),
                new Product("Blue Sweatshirt", "Nice blue sweatshirt comes in all sizes.", 300, 30, "6.png", 1, vendor2.Id),
                new Product("Purple Sweatshirt", "Nice purple sweatshirt comes in all sizes.", 300, 0, "8.png", 1, vendor2.Id),
                new Product("White Mug", "White nice looking mug.", 100, 0, "2.png", 2, vendor2.Id),
                new Product("White T-Shirt", "White T-Shirt comes in all sizes.", 200, 30,"3.png", 1, vendor2.Id),
                new Product("Purple T-Shirt", "Purple T-Shirt comes in all sizes.", 250, 20,"4.png", 1, vendor2.Id),
                new Product("Red T-Shirt", "Red T-Shirt comes in all sizes.",200, 20,"7.png", 1, vendor2.Id),
                new Product("Red Pin", "Red Pin", 50, 5,"5.png", 3, vendor2.Id),
                new Product("White Pin", "White Pin", 50, 0,"11.png", 3, vendor2.Id),
                new Product("Purple Pin", "Purple Pin", 50, 10,"10.png", 3, vendor2.Id),
            };
        }
    }
}
