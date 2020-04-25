    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public float Price { get; set; }

        public virtual Basket Basket { get; set; }
        public BasketItem()
        {

        }
        public BasketItem(int productId, float price, int quantity = 1)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetNewQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public float TotalPrice()
        {
            return Price * Quantity;
        }
    }
}
