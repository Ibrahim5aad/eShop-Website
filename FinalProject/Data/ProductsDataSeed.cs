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
        static IEnumerable<Product> GetPreconfiguredItems()
        {
            return new List<Product>()
            {
                new Product("Blue Sweatshirt", "Small Blue Sweatshirt", 200, 0.6m, "img/products/product-grey-1.jpg"),
                new Product("Red Sweatshirt", "Red Sweatshirt", 200, 0.6m, "img/products/product-grey-1.jpg"),
                new Product("Blue Shirt", "Blue Shirt", 150, 0.6m,"img/products/product-grey-1.jpg"),
                new Product("White Shirt", "Medium Shirt", 300, 0.6m,"img/products/product-grey-1.jpg"),
                new Product("Heels", "Heels", 400, 0.6m,"img/products/product-grey-1.jpg"),
                new Product("Training Suit", "Extra Large Training Suit", 350, 0.6m,"img/products/product-grey-1.jpg"),
            };
        }
    }
}
