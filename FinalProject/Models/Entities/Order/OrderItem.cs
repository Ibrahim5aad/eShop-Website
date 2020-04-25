using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public float Price { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public OrderItem(int productId, float price, int quantity = 1)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public float TotalPrice()
        {
            return Price * Quantity;
        }

    }
}
