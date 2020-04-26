using FinalProject.Data;
using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Core
{
    public class ProductRepository : IProductRepository
    {
        private readonly IWebHostEnvironment host;
        private ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment host)
        {
            this.host = host;
            this.context = context;
        }

        public Product Add(Product product)
        {
            //saves the image to wwwroot/Images
            string wwwRootPath = host.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            product.ImageName = fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Products/" + fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                product.ImageFile.CopyTo(fileStream);
            }

            context.Set<Product>().Add(product);
            return context.SaveChanges() > 0 ? product : null;
        }


        public bool Delete(Product product)
        {
            // deleting image from wwwroot
            var ImagePath = Path.Combine(host.WebRootPath, "Products", product.ImageName);

            if (System.IO.File.Exists(ImagePath))
            {
                System.IO.File.Delete(ImagePath);
            }

            context.Set<Product>().Remove(product);
            return context.SaveChanges() > 0;
        }

        public IQueryable<Product> GetAll()
        {
            return context.Set<Product>().Include(e => e.Category);
        }

        public Product GetById(params object[] id)
        {
            return context.Set<Product>().Find(id);
        }

        public bool Update(Product product)
        {
            if (product.ImageFile != null)
            {

                string wwwRootPath = host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.ImageName = fileName = fileName + DateTime.Now.ToString("yymmddssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/" + fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);

                }
            }
            else
            {
                var ImagePath = Path.Combine(host.WebRootPath, "Products", product.ImageName);

                if (System.IO.File.Exists(ImagePath))
                {
                    using (var fileStream = new FileStream(ImagePath, FileMode.Open))
                    {

                        product.ImageFile = new FormFile(fileStream, 0, fileStream.Length, null, Path.GetFileName(fileStream.Name))
                        {
                            Headers = new HeaderDictionary(),
                            ContentType = "image/png"
                        };
                    }
                }
            }
            context.Set<Product>().Update(product);
            return context.SaveChanges() > 0;
        }
    }
}
