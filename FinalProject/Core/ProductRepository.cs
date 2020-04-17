using FinalProject.Data;
using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Core
{
    public class ProductRepository : Repository<Product, ApplicationDbContext>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
