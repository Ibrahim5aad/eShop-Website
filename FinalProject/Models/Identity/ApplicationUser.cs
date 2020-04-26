using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

        [MaxLength(100)]
        public string ImageName { get; set; }
    }
}
