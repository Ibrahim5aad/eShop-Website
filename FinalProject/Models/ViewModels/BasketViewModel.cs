using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string BuyerId { get; set; }

        public float Total()
        {
            return (float)Math.Round(Items.Sum(x => x.Price * x.Quantity), 2);
        }
    }
}
