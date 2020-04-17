using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class Basket
    {
        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Basket(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void AddItem(int ProductId, decimal Price, int quantity = 1)
        {
            if (!Items.Any(i => i.ProductId == ProductId))
            {
                _items.Add(new OrderItem(ProductId, Price, quantity));
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.ProductId == ProductId);
            existingItem.AddQuantity(quantity);
        }

        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.Quantity == 0);
        }

        public void SetNewBuyerId(string buyerId)
        {
            BuyerId = buyerId;
        }
    }
}
