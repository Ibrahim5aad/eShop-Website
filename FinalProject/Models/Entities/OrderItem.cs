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
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public OrderItem(int productId, decimal price, int quantity = 1)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetNewQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
