using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        [Required]
        [ForeignKey("Category")]
        [Display(Name = "Category")]

        public int FK_Category_Id { get; set; }

        public Category Category { get; set; }

        [Required]
        public float Price { get; set; }

        [Range(0, 100)]
        [Display(Name = "Sale Percentage")]
        public float Sale { get; set; }

        [NotMapped]
        [DisplayName("Upload Product image")]
        public IFormFile ImageFile { get; set; }
        [MaxLength(100)]
        public string ImageName { get; set; }
        public Product()
        {

        }
        public Product(string name, string discription, float price, float sale, string imgName, int catId)
        {
            Name = name;
            Details = discription;
            Price = price;
            Sale = sale;
            ImageName = imgName;
            FK_Category_Id = catId;
        }

    }
}
