using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models.Entities;
using FinalProject.Core;
using Microsoft.AspNetCore.Identity;
using FinalProject.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Models.ViewModels;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrdersController(IBasketRepository basketRepository, IOrderRepository orderRepository, SignInManager<ApplicationUser> signInManager)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _signInManager = signInManager;
        }

        // GET: Orders
        [Authorize]
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll()
                .Where(order => order.BuyerId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                 .Include(order => order.Items)
                 .Include(order => order.Address);
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = _orderRepository.GetAll()
                                        .Include(order => order.Items)
                                        .ThenInclude(orderItem => orderItem.Product)
                                        .FirstOrDefault(m => m.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddOrder(OrderViewModel orderViewModel, Dictionary<string, int> items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var basket = _basketRepository.GetAll()
                    .Include(basket => basket.Items)
                    .ThenInclude(item => item.Product)
                    .FirstOrDefault(basket => basket.BuyerId == orderViewModel.Order.BuyerId);

            var OrderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                if (items.TryGetValue(item.ProductId.ToString(), out var quantity))
                {
                    OrderItems.Add(new OrderItem(item.Product.Id, item.Price, quantity));
                }
            }
            orderViewModel.Order.Items = OrderItems;
            _orderRepository.Add(orderViewModel.Order);
            _basketRepository.Delete(basket);
            return View();
        }


        [Authorize]
        public IActionResult CheckoutBasket(string BuyerId, Dictionary<string, int> items)
        {
            if (BuyerId == null) RedirectToPage("Basket");
            if (ModelState.IsValid)
            {
                var basket = _basketRepository.GetAll()
                    .Include(basket => basket.Items)
                    .ThenInclude(item => item.Product)
                    .FirstOrDefault(basket => basket.BuyerId == BuyerId);

                var OrderItems = new List<OrderItem>();

                foreach (var item in basket.Items)
                {
                    if (items.TryGetValue(item.Id.ToString(), out var quantity))
                    {
                        OrderItems.Add(new OrderItem(item.Product.Id, item.Price, quantity));
                    }
                }
                OrderViewModel ovm = new OrderViewModel()
                {
                    Order = new Order()
                    {
                        BuyerId = BuyerId,
                        Status = OrderStatus.Pending,
                        CheckoutDate = DateTime.Now
                    },
                    OrderItems = OrderItems

                };

                return View(ovm);
            }
            return BadRequest();
        }


        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();// order);
        }


        private bool OrderExists(int id)
        {
            return true;// _context.Orders.Any(e => e.Id == id);
        }
    }
}
