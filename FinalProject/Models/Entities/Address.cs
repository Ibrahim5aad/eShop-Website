using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class Address
    {
        public int Id { get; set; }
        
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string HouseNumber { get; set; }

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }


        public override string ToString()
        {
            return $"{HouseNumber}, {Street}, {City}, {Country}";
        }
    }
}
