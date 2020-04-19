using FinalProject.Data;
using FinalProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Core
{
    public class OrderRepository : Repository<Order, ApplicationDbContext>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
