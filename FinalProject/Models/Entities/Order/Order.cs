using FinalProject.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public virtual ApplicationUser Buyer { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CheckoutDate { get; set; }

        public virtual Address Address { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public virtual List<OrderItem> Items { get; set; }

        [NotMapped]
        public float TotalPrice
        {
            get
            {
                float total = 0;
                if(Items != null)
                {
                    foreach (var item in Items)
                    {
                        total += item.TotalPrice();
                    }
                }
                return total;
            }
        }
    }
}
