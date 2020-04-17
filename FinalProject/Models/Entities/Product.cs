using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Discription { get; set; }
        public decimal Price { get; set; }
        public decimal Sale { get; set; }

        public Product()
        {

        }
        public Product(string name, string discription, decimal price,decimal sale, string imgUrl)
        {
            Name = name;
            Discription = discription;
            Price = price;
            Sale = sale;
            PictureUrl = imgUrl;
        }

    }
}
