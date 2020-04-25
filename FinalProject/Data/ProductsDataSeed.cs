using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class ApplicationDbContextSeed
    {
        public async static Task SeedAsync(ApplicationDbContext context, int? retry = 0)
        {
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
                new Category("Cups"),
                new Category("Accessories")
            };
        }
        static IEnumerable<Product> GetPreconfiguredItems()
        {
            return new List<Product>()
            {
                new Product("Black Sweatshirt", "Nice black sweatshirt comes in all sizes.", 300, 20, "1.png", 1),
                new Product("Blue Sweatshirt", "Nice blue sweatshirt comes in all sizes.", 300, 30, "6.png", 1),
                new Product("Purple Sweatshirt", "Nice purple sweatshirt comes in all sizes.", 300, 15, "8.png", 1),
                new Product("White Mug", "White nice looking mug.", 100, 0, "2.png", 2),
                new Product("White T-Shirt", "White T-Shirt comes in all sizes.", 200, 30,"3.png", 1),
                new Product("Purple T-Shirt", "Purple T-Shirt comes in all sizes.", 250, 20,"4.png", 1),
                new Product("Red T-Shirt", "Red T-Shirt comes in all sizes.",200, 20,"7.png", 1),
                new Product("Red Pin", "Red Pin", 50, 5,"5.png", 3),
                new Product("White Pin", "White Pin", 50, 5,"11.png", 3),
                new Product("Purple Pin", "Purple Pin", 50, 10,"10.png", 3),
            };
        }
    }
}
