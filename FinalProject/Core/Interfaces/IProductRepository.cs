using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Core
{
    public interface IProductRepository
    {
        Product GetById(params object[] id);

        IQueryable<Product> GetAll();

        Product Add(Product product);

        bool Update(Product product);

        bool Delete(Product product);
    }
}
