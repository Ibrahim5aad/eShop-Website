﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string BuyerId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CheckoutDate { get; set; }

        public virtual List<OrderItem> Items { get; set; }
    }
}