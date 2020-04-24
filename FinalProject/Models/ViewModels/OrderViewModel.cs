using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
