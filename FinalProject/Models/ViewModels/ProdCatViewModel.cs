using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class ProdCatViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }

        public List<Product> Products { get; set; }
    }
}
