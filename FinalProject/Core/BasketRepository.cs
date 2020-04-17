using FinalProject.Data;
using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Core
{
    public class BasketRepository : Repository<Basket, ApplicationDbContext>, IBasketRepository
    {

        public BasketRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
